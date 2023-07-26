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
                    RightGroups_Description = "Test",
                    RightGroups_Id = Guid.Parse("E9FE8BFA-D3AA-40E4-9A5B-305458BAA8BB"),
                    RightGroups_Name = "External user for channel partners and customers"
                },
                new St4.RightGroup
                {
                    RightGroups_Description = "Test",
                    RightGroups_Id = Guid.Parse("C75B7CCA-95FA-4816-A3A7-58AF77006347"),
                    RightGroups_Name = "System Admins"
                },
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