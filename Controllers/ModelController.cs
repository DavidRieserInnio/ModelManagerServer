using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;
using ModelManagerServer.Service;
using ModelManagerServer.St4.Enums;
using ModelManagerServer.ViewModels;

namespace ModelManagerServer.Controllers
{
    public class ModelController : Controller
    {
        private readonly ModelRepository _modelRepository;

        private static Guid USER_ID { get => Guid.Parse("90530C69-9F4D-4F4B-9ED7-4B31092E605D"); }

        public ModelController(ModelRepository modelRepository)
        {
            this._modelRepository = modelRepository;
        }

        #region View Actions
        public IActionResult Index(Guid? modelId = null)
        {
            IEnumerable<IGrouping<Guid, Model?>>? models;
            if (modelId is not null)
                models = this._modelRepository.FindModelWithVersionsOrdered(modelId.Value)?.GroupBy(m => m.Id);
            else
                models = this._modelRepository.FindAllModelsGroupedByVersion();
            return View(models);
        }

        public IActionResult CreateModel()
        {
            return View("ModelBuilder");
        }

        public IActionResult EditModel()
        {
            return View("ModelBuilder");
        }
        #endregion

        #region API Actions
        public IActionResult FindModel(Guid modelId, int modelVersion)
        {
            var model = this._modelRepository.FindModel(modelId, modelVersion);
            var success = model is not null;
            return Json(new { success, model });
        }

        [HttpPost]
        public IActionResult Create(Model model)
        {
            var res = this._modelRepository.CreateModel(model, USER_ID);

            if (res.IsNone)
                return Json(new { success = true });
            return Json(new { success = false, error = res.Get() });
        }

        [HttpGet]
        public IActionResult ConvertModel(Guid modelId, int modelVersion)
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is null) return RedirectToAction(nameof(Index));

            var expressions = ConversionService.FindExpressions(model).Select(k => (k, "")).ToDictionary();
            var values = model.TemplateValues.Select(t => (t.Name, t.Value));
            foreach (var (Name, Value) in values) expressions[Name] = Value;
            
            expressions.Remove(nameof(modelId));
            expressions.Remove(nameof(modelVersion));
            expressions.Remove("startPosition");

            return View("EditValues", new EditValuesViewModel(modelId, modelVersion, expressions.ToList()));
        }

        /* TODO: Create Preview Endpoint using ProjectConfigurator Code? */
        [HttpPost]
        public IActionResult ConvertModel(
            Guid modelId,
            int modelVersion,
            int startPosition,
            IFormCollection kvps
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is not null)
            {
                Dictionary<string, string> userValues = kvps.ToDictionary();
                var parts = ConvertModel(model, userValues, startPosition);
                // TODO: Store Parts in St4 Database
            }
            return Json(new { success = model is not null });
        }

        private static List<St4.Part> ConvertModel(
            Model model, Dictionary<string, string> userValues, int startPosition
        )
        {
            var parts = ConversionService.ConvertModel(model, userValues, 0);
            for (var i = 0; i < parts.Count; i++) 
                parts[i].Parts_Position = startPosition + i;
            return parts;
        }

        [HttpPost]
        public IActionResult SetModelState(Guid modelId, int modelVersion, St4PartState state)
        {
            var success = Enum.IsDefined(state) && 
                          this._modelRepository.ChangeModelState(modelId, modelVersion, state);
            return Json(new { success });
        }
        #endregion
    }
}
