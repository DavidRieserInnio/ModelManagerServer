using Microsoft.EntityFrameworkCore;
using ModelManagerServer.Entitites;

namespace ModelManagerServer
{
    public class ModelManagerContext : DbContext
    {
        private readonly IConfiguration _Configuration;

        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<Entitites.Enum> Enums { get; set; }
        public virtual DbSet<EnumProperty> EnumVariants { get; set; }
        public virtual DbSet<PartPermission> PartPermission { get; set; }
        public virtual DbSet<PartProperty> PartProperty { get; set; }
        public virtual DbSet<TemplateValue> TemplateValues { get; set; }

        public ModelManagerContext(DbContextOptions<ModelManagerContext> options, IConfiguration configuration)
            : base(options)
        {
            _Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Model>()
                .HasMany(m => m.Parts)
                .WithMany(p => p.Models)
                .UsingEntity<RefModelPart>();
            */
        }
    }
}
