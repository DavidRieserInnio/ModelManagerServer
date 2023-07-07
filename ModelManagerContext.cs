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
        public virtual DbSet<EnumVariant> EnumVariants { get; set; }
        public virtual DbSet<EnumVariantProperty> EnumVariantProperties { get; set; }

        public ModelManagerContext(DbContextOptions<ModelManagerContext> options, IConfiguration configuration)
            : base(options)
        {
            _Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnumVariant>()
                .HasMany(v => v.EnumVariantProperties)
                .WithMany(p => p.EnumVariants)
                .UsingEntity<RefEnumVariantProperty>();

            modelBuilder.Entity<Model>()
                .HasMany(m => m.Parts)
                .WithMany(p => p.Models)
                .UsingEntity<RefModelPart>();

            modelBuilder.Entity<Model>()
                .HasMany(m => m.TemplateValues)
                .WithMany(t => t.Models)
                .UsingEntity<RefModelTemplateValue>();

            modelBuilder.Entity<Model>()
                .HasMany(m => m.Rules)
                .WithMany(r => r.Models)
                .UsingEntity<RefModelRule>();

            modelBuilder.Entity<Part>()
                .HasMany(p => p.PartProperties)
                .WithMany(p => p.Parts)
                .UsingEntity<RefPartProperty>();

            modelBuilder.Entity<Part>()
                .HasMany(p => p.PartPermissions)
                .WithMany(p => p.Parts)
                .UsingEntity<RefPartPermission>();
        }
    }
}
