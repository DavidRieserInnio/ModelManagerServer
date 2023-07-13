using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Models;
using ModelManagerServer.Repositories;
using ModelManagerServer.Service;

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
            DictionarySubstitutionProvider userValues = kvps.ToDictionary();
            IList<St4.Part>? parts = model?.Substitute(userValues);
            // TODO
            return Json(new { });
        }
    }
}