using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;
using ModelManagerServer.Service;
using ModelManagerServer.St4.Enums;

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
            model.Version = this._modelRepository.GetNextModelVersion(model);
            var res = this._modelRepository.CreateModel(model, USER_ID);

            if (res.IsNone)
                return Json(new { success = true });
            return Json(new { success = false, error = res.Get() });
        }

        /* TODO: Create Preview Endpoint using ProjectConfigurator Code? */
        [HttpPost]
        public IActionResult ConvertModel(
            Guid modelId,
            int modelVersion,
            IFormCollection kvps
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is not null)
            {
                // TODO: Get Position on where to insert Parts
                Dictionary<string, string> userValues = kvps.ToDictionary();
                IList<St4.Part>? parts = ConversionService.ConvertModel(model, userValues, 0);
            }
            // TODO
            return Json(new { success = model is not null });
        }

        public IActionResult GetMissingValues(
            Guid modelId,
            int modelVersion
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);

            if (model is null) return NotFound();

            // TODO: Get missing Values;

            return Ok(new List<string>());
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
