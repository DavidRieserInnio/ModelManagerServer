using Microsoft.EntityFrameworkCore;
using ModelManagerServer.Entities;

namespace ModelManagerServer
{
    public class ModelManagerContext : DbContext
    {
        private readonly IConfiguration _Configuration;

        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<Entities.PartEnum> Enums { get; set; }
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
            modelBuilder.Entity<Model>().HasKey(m => new { m.Id, m.Version });
            modelBuilder.Entity<Part>().HasKey(p => new { p.Id, p.Version });
            modelBuilder.Entity<PartProperty>().HasKey(p => p.Id);
            modelBuilder.Entity<PartPermission>().HasKey(p => p.Id);
            modelBuilder.Entity<Entities.PartEnum>().HasKey(e => new { e.Id, e.Version });
            modelBuilder.Entity<EnumProperty>().HasKey(p => p.Id);
            modelBuilder.Entity<TemplateValue>().HasKey(t => t.Id);
            modelBuilder.Entity<Rule>().HasKey(r => r.Id);
            modelBuilder.Entity<RefModelPart>().HasKey(r => new
            {
                r.Model_Id,
                r.Model_Version,
                r.Part_Id,
                r.Part_Version,
            });

            modelBuilder.Entity<Model>()
                .HasMany(m => m.Parts)
                .WithMany(p => p.Models)
                .UsingEntity<RefModelPart>(
                    l => l.HasOne(r => r.Part)
                        .WithMany(p => p.RefModelsParts)
                        .HasForeignKey(r => new { r.Part_Id, r.Part_Version }),
                    r => r.HasOne(r => r.Model)
                        .WithMany(m => m.RefModelsParts)
                        .HasForeignKey(r => new { r.Model_Id, r.Model_Version })
                );

            modelBuilder.Entity<Model>()
                .HasOne(m => m.Rule)
                .WithOne(r => r.Model)
                .HasForeignKey<Rule>(r => new { r.Model_Id, r.Model_Version })
                .IsRequired(false);

            modelBuilder.Entity<Model>()
                .HasMany(m => m.TemplateValues)
                .WithOne(t => t.Model)
                .HasForeignKey(t => new { t.Model_Id, t.Model_Version });

            modelBuilder.Entity<Part>()
                .HasMany(p => p.PartProperties)
                .WithOne(p => p.Part)
                .HasForeignKey(p => new { p.Part_Id, p.Part_Version });

            modelBuilder.Entity<Part>()
                .HasMany(p => p.PartPermissions)
                .WithOne(p => p.Part)
                .HasForeignKey(p => new { p.Part_Id, p.Part_Version });

            modelBuilder.Entity<Part>()
                .HasOne(p => p.Rule)
                .WithMany(p => p.Parts)
                .HasForeignKey(p => p.Rule_Id);

            modelBuilder.Entity<Part>()
                .HasOne(p => p.Enum)
                .WithOne(p => p.Part)
                .HasForeignKey<Part>(p => new { p.Enum_Id, p.Enum_Version });

            modelBuilder.Entity<Entities.PartEnum>()
                .HasMany(e => e.Properties)
                .WithOne(p => p.Enum)
                .HasForeignKey(p => new { p.Enum_Id, p.Enum_Version });
        }
    }
}
