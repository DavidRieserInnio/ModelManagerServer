using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.Controllers
{
    public class ModelController : Controller
    {
        private readonly ModelRepository _modelRepository;

        public ModelController(ModelRepository modelRepository)
        {
            this._modelRepository = modelRepository;
        }

        public IActionResult Index()
        {
            var models = this._modelRepository.FindAllModels();
            return View(models);
        }

        [HttpPost]
        public IActionResult Create(Model model)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                model.Version = this._modelRepository.GetNextModelVersion(model);
                success = this._modelRepository.CreateModel(model, /* TODO: Get User Id */ Guid.NewGuid());
            }

            return Json(new { success });
        }

        public IActionResult SetModelState(Guid modelId, int modelVersion, St4ConfigState state)
        {
            var success = ModelState.IsValid && 
                          this._modelRepository.ChangeModelState(modelId, modelVersion, state);
            return Json(new { success });
        }
    }
}
