using Microsoft.EntityFrameworkCore;

namespace ModelManagerServer.St4
{
    public class St4PartsRepository
    {
        private TpeConfiguratorContext _context;

        public St4PartsRepository(TpeConfiguratorContext tpeConfiguratorContext) {
            this._context = tpeConfiguratorContext;
        }

        public IEnumerable<ConfigVersion> GetAllConfigVersions()
        {
            return this._context.ConfigVersions;
        }

        public IEnumerable<Milestone> GetMilestones()
        {
            return this._context.Milestones;
        }

        public IEnumerable<RightGroup> GetRightGroups()
        {
            return this._context.RightGroups;
        }

        private static readonly string RULE_NAME = "ModelManagerRule";

        public RuleMethod? GetRuleMethod(Guid configVersionId) 
        {
            var references = this._context.RefModelRules.Where(r => r.ConfigVersions_Id == configVersionId).Select(r => new { r.RuleMethods_Id, r.RuleMethods_Version }).ToList();
            var rules = this._context.Rules.Where(r => r.RuleMethods_Name == RULE_NAME).Where(r => references.Contains(new { r.RuleMethods_Id, r.RuleMethods_Version }));
            return rules.OrderByDescending(r => r.RuleMethods_Version).FirstOrDefault();
        }

        public void AddRuleMethod(Guid configVersion, RuleMethod ruleMethod)
        {
            this._context.Rules.Add(ruleMethod);
            this._context.RefModelRules.Add(new RefModelRule()
            {
                ConfigVersions_Id = configVersion,
                RuleMethods_Id = ruleMethod.RuleMethods_Id,
                RuleMethods_Position = ruleMethod.RuleMethods_Position,
                RuleMethods_Version = ruleMethod.RuleMethods_Version,
            });
        }

        public void AddParts(Guid configVersionId, List<Part> part)
        {
            part.ForEach(part => this.AddPart(configVersionId ,part));
        }

        public void AddPart(Guid configVersionId, Part part)
        {
            this._context.Parts.Add(part);
            this._context.RefModelParts.Add(new RefModelPart()
            {
                ConfigVersions_Id = configVersionId,
                Parts_Id = part.Parts_Id,
                Parts_Version = part.Parts_Version,
                Parts_Position = part.Parts_Position,
            });
            part.Properties.ForEach(prop => {
                this._context.Properties.Add(prop);
                foreach (var lc in prop.Properties_TranslationTexts) {
                    this._context.LocalizationTexts.Add(lc.Value);
                }
            });
            part.PartPermissions.ForEach(perm => this._context.PartPermissions.Add(perm));
        }

        public void MoveParts(Guid configVersion, int startPosition, int amount)
        {
            var refs = this._context.RefModelParts.Where(r => r.ConfigVersions_Id == configVersion && r.Parts_Position >= startPosition).ToList();
            foreach (var rf in refs)
                rf.Parts_Position += amount;
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }
    }
}
