using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
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

        [HttpGet]
        public IActionResult CreateModel()
        {
            return View("CreateModel");
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

            /*
                $.ajax({
                    method: "POST",
                    url: "/Home/ConvertModel?" + $.param({
                        "modelId": "bb96aba7-316a-4201-9b96-c877a574051b",
                        "modelVersion": 1
                    }),
                    data: {
                         "Test": [ "Value", "Value2" ] 
                    },
                    success: function(d) {
                        console.log(d);
                    }
                })
             */

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is not null)
            {
                // TODO: Get Position on where to insert Parts
                Dictionary<string, string> userValues = kvps.ToDictionary();
                IList<St4.Part>? parts = ConversionService.ConvertModel(model, userValues, 0);
            }
            // TODO
            return Json(new { success = model is not null});
        }

        [HttpGet]
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

        [HttpGet]
        public List<St4.RightGroup> GetRightGroups()
        {
            return new List<St4.RightGroup> { new St4.RightGroup
                {
                    RightGroups_Id = Guid.NewGuid(),
                    RightGroups_Name = "Test"
                } 
            };
        }
     
        [HttpGet]
        public List<St4.Milestone> GetMilestones()
        {
            return new List<St4.Milestone> { new St4.Milestone
                {
                    Milestones_Id = Guid.NewGuid(),
                    Milestones_Name = "Test",
                }
            };
        }
    }
}