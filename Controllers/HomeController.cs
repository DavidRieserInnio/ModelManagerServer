using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction(nameof(ModelController.Index), "Model");
        }

        [HttpGet]
        public List<St4.RightGroup> GetRightGroups()
        {
            return new List<St4.RightGroup> { new St4.RightGroup
                {
                    RightGroups_Id = Guid.NewGuid(),
                    RightGroups_Name = "Test",
                    RightGroups_Description = "Test",
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

        [HttpGet]
        public IEnumerable<string> GetModelStates()
        {
            return Enum.GetNames<St4PartState>();
        }
    }
}