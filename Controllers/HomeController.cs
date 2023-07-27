using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.St4;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.Controllers
{
    public class HomeController : Controller
    {
        private St4PartsRepository _partsRepository;

        public HomeController(St4PartsRepository partsRepository)
        {
            this._partsRepository = partsRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(ModelController.Index), "Model");
        }

        [HttpGet]
        public List<St4.RightGroup> GetRightGroups()
        {
            return this._partsRepository.GetRightGroups().ToList();
        }

        [HttpGet]
        public List<St4.Milestone> GetMilestones()
        {
            return this._partsRepository.GetMilestones().ToList();
        }

        [HttpGet]
        public IEnumerable<ConfigVersion> GetConfigVersions()
        {
            return this._partsRepository.GetAllConfigVersions().ToList();
        }

        [HttpGet]
        public IEnumerable<string> GetModelStates()
        {
            return Enum.GetNames<St4PartState>();
        }
    }
}