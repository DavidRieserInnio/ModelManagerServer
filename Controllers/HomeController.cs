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
            return View();
        }
        
        public IActionResult CreateModel() { 
            Model model = new()
            {
                Name = "Test Model",
                Version = 1,
                Model_Id = Guid.NewGuid(),
                Type = 1,
                CreatedByUserId = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                Rules = new List<Rule>(),
                TemplateValues = new List<TemplateValue>(),
                Parts = new List<Part>()
            {
                new Part()
                {
                    Name = "Part 1",
                    Version = 1,
                    ElementText = "Combo Box 1",
                    Type = 1,
                    Part_Id = Guid.NewGuid(),
                    Rule = null,
                    Enum = null,
                    PartPermissions = new List<PartPermission>(),
                    PartProperties = new List<PartProperty>()
                },
                new Part()
                {
                    Name = "Part 1",
                    Version = 1,
                    ElementText = "Combo Box 1",
                    Type = 1,
                    Part_Id = Guid.NewGuid(),
                    Rule = null,
                    PartPermissions = new List<PartPermission>()
                    {
                        new PartPermission()
                        {
                            RightGroupId = Guid.NewGuid(),
                            Type = St4Permission.Write,
                        },
                        new PartPermission()
                        {
                            RightGroupId = Guid.NewGuid(),
                            Type = St4Permission.Write,
                        }
                    },
                    PartProperties = new List<PartProperty>()
                    {
                        new PartProperty()
                        {
                            Name = "Property1",
                            PartProperty_Id = Guid.NewGuid(),
                            Value = "Property Value 1"
                        },
                        new PartProperty()
                        {
                            Name = "Property1",
                            PartProperty_Id = Guid.NewGuid(),
                            Value = "Property Value 1"
                        }
                    },
                    Enum = new Entitites.Enum()
                    {
                        Enum_Id = Guid.NewGuid(),
                        Name = "Test Enum",
                        Variants = new List<EnumVariant>()
                        {
                            new EnumVariant()
                            {
                                ArticleCode = "Variant1",
                                ItemText = "Item Text",
                                EnumVariantProperties = new List<EnumVariantProperty>()
                                {
                                    new EnumVariantProperty()
                                    {
                                        Name = "Property1",
                                        EnumVariantProperty_Id = Guid.NewGuid(),
                                        Value = "Value1"
                                    },
                                    new EnumVariantProperty()
                                    {
                                        Name = "Property2",
                                        EnumVariantProperty_Id = Guid.NewGuid(),
                                        Value = "Value2"
                                    },
                                }
                            }
                        }
                    }
                }
            }
            };

            this.modelRepository.CreateModel(model);

            return View();
        }
    }
}