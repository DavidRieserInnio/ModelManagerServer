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

            foreach (var variant in e.Variants)
            {
                this._ctx.Add(variant);
                foreach (var property in variant.EnumVariantProperties)
                {
                    this._ctx.EnumVariantProperties.Add(property);
                }
            }

            this._ctx.SaveChanges();
        }
    }
}
