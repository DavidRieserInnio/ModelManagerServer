using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Repositories;

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
            Model model = new()
            {
                Name = "Pump",
                Version = 1,
                CreatedByUserId = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                Id = Guid.NewGuid(),
                State = 5,
            };

            model.Rule = new Rule()
            {
                Id = Guid.NewGuid(),
                Model = model,
                Name = "Rule 1",
                Content = "Testing"
            };

            model.Parts = new List<Part>()
            {
                new Part()
                {
                    Id = Guid.NewGuid(),
                    ElementText = "Part 1",
                    Enum = null,
                    Name = "Name",
                    PartPermissions = Array.Empty<PartPermission>(),
                    Version = 1,
                    Type = 10,
                    Rule = model.Rule,
                    PartProperties =  Array.Empty<PartProperty>(),
                },
                new Part()
                {
                    Id = Guid.NewGuid(),
                    ElementText = "Part 2",
                    Enum = new Entities.Enum()
                    {
                        Version = 1,
                        Id = Guid.NewGuid(),
                        Name = "Enum 1",
                        Properties = Array.Empty<EnumProperty>(),
                    },
                    Name = "Name",
                    PartPermissions = Array.Empty<PartPermission>(),
                    Version = 1,
                    Type = 10,
                    Rule = model.Rule,
                    PartProperties =  Array.Empty<PartProperty>(),
                },
                new Part()
                {
                    Id = Guid.NewGuid(),
                    ElementText = "Part 1",
                    Enum = null,
                    Name = "Name",
                    PartPermissions = Array.Empty<PartPermission>(),
                    Version = 1,
                    Type = 10,
                    Rule = model.Rule,
                    PartProperties =  Array.Empty<PartProperty>(),
                },
            };

            model.TemplateValues = new List<TemplateValue>()
            {
                new TemplateValue()
                {
                    Id = Guid.NewGuid(),
                    Model = model,
                    ApplyToParts = true,
                    Name = "TestValue",
                    Value = "Value"
                },
                new TemplateValue()
                {
                    Id = Guid.NewGuid(),
                    Model = model,
                    ApplyToParts = false,
                    Name = "TestValue2",
                    Value = "Value"
                }
            };

            model.CreateReferences();
            this.modelRepository.CreateModel(model);

            return View();
        }
    }
}