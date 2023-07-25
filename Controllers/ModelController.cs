using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;
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

        public IActionResult Index(Guid? modelId = null)
        {
            IEnumerable<IGrouping<Guid, Model?>>? models;
            if (modelId is not null)
                models = this._modelRepository.FindModelWithVersions(modelId.Value)?.GroupBy(m => m.Id);
            else
                models = this._modelRepository.FindAllModelsGroupedByVersion();
            return View(models);
        }

        [HttpPost]
        public IActionResult Create(Model model)
        {
            model.Version = this._modelRepository.GetNextModelVersion(model);
            var res = this._modelRepository.CreateModel(model, USER_ID);

            if (res.IsSome)
                return Json(new { success = false, error = res.Get() });
            return Json(new { success = true });
        }

        public IActionResult SetModelState(Guid modelId, int modelVersion, St4PartState state)
        {
            var success = ModelState.IsValid && 
                          this._modelRepository.ChangeModelState(modelId, modelVersion, state);
            return Json(new { success });
        }
    }
}
