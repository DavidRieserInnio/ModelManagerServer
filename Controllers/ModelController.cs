using Microsoft.AspNetCore.Mvc;
using ModelManagerServer.Entities;
using ModelManagerServer.Models;
using ModelManagerServer.Repositories;
using ModelManagerServer.Service;
using ModelManagerServer.St4;
using ModelManagerServer.St4.Enums;
using ModelManagerServer.ViewModels;

namespace ModelManagerServer.Controllers
{
    public class ModelController : Controller
    {
        private readonly ModelRepository _modelRepository;
        private readonly St4PartsRepository _st4PartsRepository;

        private static Guid USER_ID { get => Guid.Parse("90530C69-9F4D-4F4B-9ED7-4B31092E605D"); }

        public ModelController(ModelRepository modelRepository, St4PartsRepository st4PartsRepository)
        {
            this._modelRepository = modelRepository;
            this._st4PartsRepository = st4PartsRepository;
        }

        #region View Actions
        public IActionResult Index(Guid? modelId = null)
        {
            IEnumerable<IGrouping<Guid, Model?>>? models;
            if (modelId is not null)
                models = this._modelRepository.FindModelWithVersionsOrdered(modelId.Value)?.GroupBy(m => m.Id);
            else
                models = this._modelRepository.FindAllModelsGroupedByVersion();
            return View(models);
        }

        public IActionResult CreateModel()
        {
            return View("ModelBuilder");
        }

        public IActionResult EditModel()
        {
            return View("ModelBuilder");
        }
        #endregion

        #region API Actions
        public IActionResult FindModel(Guid modelId, int modelVersion)
        {
            var model = this._modelRepository.FindModel(modelId, modelVersion);
            var success = model is not null;
            return Json(new { success, model });
        }

        [HttpPost]
        public IActionResult Create(Model model)
        {
            var res = this._modelRepository.CreateModel(model, USER_ID);

            if (res.IsNone)
                return Json(new { success = true });
            return Json(new { success = false, error = res.Get() });
        }

        [HttpGet]
        public IActionResult ConvertModel(Guid modelId, int modelVersion)
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is null) return RedirectToAction(nameof(Index));

            var expressions = ConversionService.FindExpressions(model).Select(k => (k, "")).ToDictionary();
            var values = model.TemplateValues.Select(t => (t.Name, t.Value));
            foreach (var (Name, Value) in values) expressions[Name] = Value;
            
            expressions.Remove(nameof(modelId));
            expressions.Remove(nameof(modelVersion));
            expressions.Remove("startPosition");

            return View("EditValues", new EditValuesViewModel(modelId, modelVersion, expressions.ToList()));
        }

        /* TODO: Create Preview Endpoint using ProjectConfigurator Code? */
        [HttpPost]
        public IActionResult ConvertModel(
            Guid modelId,
            int modelVersion,
            Guid configVersionId,
            int startPosition,
            IFormCollection kvps
        )
        {
            if (!ModelState.IsValid) return BadRequest();

            Model? model = this._modelRepository.FindModel(modelId, modelVersion);
            if (model is not null)
            {
                Dictionary<string, string> userValues = kvps.ToDictionary();
                var conv = this.ConvertModel(model, userValues, startPosition);
                this.SaveConvertedModel(configVersionId, conv);
            }
            return Json(new { success = model is not null });
        }

        private ConvertedModel ConvertModel(
            Model model, Dictionary<string, string> userValues, int startPosition
        )
        {
            return ConversionService.ConvertModel(model, userValues, startPosition);
        }

        private void SaveConvertedModel(Guid configVersionId, ConvertedModel convertedModel)
        {
            var parts = convertedModel.Parts;

            int start = parts.Min(x => x.Parts_Position);
            int end = parts.Max(x => x.Parts_Position);

            this._st4PartsRepository.MoveParts(configVersionId, start, end - start);
            this._st4PartsRepository.AddParts(configVersionId, parts);

            if (convertedModel.ModelRuleContent is not null) {
                var rule = this._st4PartsRepository.GetRuleMethod(configVersionId);
                if (rule is null)
                {
                    this._st4PartsRepository.AddRuleMethod(configVersionId, new RuleMethod()
                    {
                        Editable = true,
                        RuleMethods_Content = convertedModel.ModelRuleContent,
                        RuleMethods_CreatedBy_Users_Id = USER_ID,
                        RuleMethods_CreationTime = DateTime.Now,
                        RuleMethods_Description = "Generated by Model Manager",
                        RuleMethods_Id = Guid.NewGuid(),
                        RuleMethods_Type = MethodType.Executable,
                        RuleMethods_LockedBy_Users_Id = null,
                        RuleMethods_Comments = "",
                        RuleMethods_Version = 1,
                        RuleMethods_State = RuleMethodState.Working,
                        RuleMethods_Position = 0,
                        RuleMethods_Name = convertedModel.ModelRuleName!
                    });
                } else
                {
                    rule.RuleMethods_Content += convertedModel.ModelRuleContent;
                }
            }

            this._st4PartsRepository.SaveChanges();
        }

        [HttpPost]
        public IActionResult SetModelState(Guid modelId, int modelVersion, St4PartState state)
        {
            var success = Enum.IsDefined(state) && 
                          this._modelRepository.ChangeModelState(modelId, modelVersion, state);
            return Json(new { success });
        }
        #endregion
    }
}
