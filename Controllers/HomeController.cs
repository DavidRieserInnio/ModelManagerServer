using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;

namespace ModelManagerServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelRepository _modelRepository;

        public HomeController(ModelRepository modelRepository)
        {
            this._modelRepository = modelRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateModel(Model model)
        {
            // TODO
            return Json(new { });
        }

        [HttpPost]
        public IActionResult ConvertModel(
            Guid modelId,
            int modelVersion,
            IFormCollection kvps
        )
        {
            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            IList<St4.Part>? parts = model?.Substitute(null!);
            // TODO
            return Json(new { });
        }
    }
}