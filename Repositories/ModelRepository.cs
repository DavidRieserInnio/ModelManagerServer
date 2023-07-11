using ModelManagerServer.Entitites;

namespace ModelManagerServer.Repositories
{
    public class ModelRepository
    {
        private readonly ModelManagerContext _ctx;

        public ModelRepository(ModelManagerContext ctx)
        {
            this._ctx = ctx;
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

        public void CreateEnum(Entitites.Enum e)
        {
            this._ctx.Enums.Add(e);
            this._ctx.AddRange(e.Properties);

            this._ctx.SaveChanges();
        }
    }
}
