using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entitites;
using ModelManagerServer.Repositories;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelRepository modelRepository;

        public HomeController(ModelRepository modelRepository)
        {
            this.modelRepository = modelRepository;
        }

        public IActionResult Index()
        {
            Model model = new Model()
            {
                Name = "Pump",
                Version = 1,
                Rule = null,
                CreatedByUserId = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                Model_Id = Guid.NewGuid(),
                Parts = new List<Part>(),
                State = 5,
                TemplateValues = new List<TemplateValue>(),
            };

            model.CreateReferences();
            this.modelRepository.CreateModel(model);

            return View();
        }
    }
}