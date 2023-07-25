using Microsoft.EntityFrameworkCore;
using ModelManagerServer.Entities;
using ModelManagerServer.Models;
using ModelManagerServer.Service;
using ModelManagerServer.St4.Enums;

namespace ModelManagerServer.Repositories
{
    public class ModelRepository
    {
        private readonly ModelManagerContext _ctx;

        public ModelRepository(ModelManagerContext ctx)
        {
            this._ctx = ctx;
        }

        public Model? FindModel(Guid id, int version)
        {
            return this._ctx.Models.FirstOrDefault(m => m.Id == id && m.Version == version);
        }

        public List<Model>? FindModelWithVersions(Guid id)
        {
            return this._ctx.Models.Where(m => m.Id == id)
                .OrderBy(m => m.Version)
                .CheckEmpty()?
                .ToList();
        }

        public List<List<Model>> FindAllModelsGroupedByVersion()
        {
            return this._ctx.Models.GroupBy(m => m.Id)
                .Select(g => g.OrderBy(m => m.Version).ToList())
                .ToList();
        }

        public List<Model> FindAllModels()
        {
            return this._ctx.Models.ToList();
        }

        public Option<DbUpdateException> CreateModel(Model model, Guid userId)
        {
            // TODO: Check neccessary Things are set!
            model.CreateReferences(userId);
            this._ctx.Models.Add(model);
            try
            {
                this._ctx.SaveChanges();
                return Option<DbUpdateException>.None;
            } 
            catch (DbUpdateException exp)
            {
                return exp;
            }
        }

        public List<Model> GetModelHistory(Model model) => this.GetModelHistory(model.Id);
        public List<Model> GetModelHistory(Guid modelId)
        {
            return this._ctx.Models
                .Where(m => m.Id == modelId)
                .OrderBy(m => m.Version)
                .ToList();
        }

        public int GetNextModelVersion(Model model) => this.GetNextModelVersion(model.Id);
        public int GetNextModelVersion(Guid modelId)
        {
            var highestVersion = this.GetModelHistory(modelId)
                .CheckEmpty()?
                .Max(m => m.Version) ?? 0;
            return highestVersion + 1;
        }

        public bool ChangeModelState(Guid modelId, int modelVersion, St4PartState state)
        {
            var model = this.FindModel(modelId, modelVersion);

            if (model is not null)
            {
                model.State = state;
                try
                {
                    this._ctx.SaveChanges();
                    return true;
                } catch { /* Fall trough and return false. */ }
            }
            return false;
        }
    }
}
