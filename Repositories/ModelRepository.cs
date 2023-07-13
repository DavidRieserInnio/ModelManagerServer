using ModelManagerServer.Entities;
using ModelManagerServer.Service;
using System;

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
                .CheckEmpty()?
                .OrderBy(m => m.Version)
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

        public void CreateModel(Model model)
        {
            model.CreateReferences();
            this._ctx.Models.Add(model);
            this._ctx.SaveChanges();
        }

        public void CreateEnum(Entities.Enum e)
        {
            this._ctx.Enums.Add(e);
            this._ctx.AddRange(e.Properties);

            this._ctx.SaveChanges();
        }
    }
}
