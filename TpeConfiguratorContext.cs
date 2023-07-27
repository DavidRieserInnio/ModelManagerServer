using Microsoft.EntityFrameworkCore;
using ModelManagerServer.Entities;
using ModelManagerServer.St4;

namespace ModelManagerServer
{
    public class TpeConfiguratorContext : DbContext
    {
        private readonly IConfiguration _Configuration;

        public virtual DbSet<St4.Part> Parts { get; set; }
        public virtual DbSet<St4.RuleMethod> Rules { get; set; }
        public virtual DbSet<St4.ConfigVersion> ConfigVersions { get; set; }
        public virtual DbSet<St4.Localization> LocalizationTexts { get; set; }
        public virtual DbSet<St4.Milestone> Milestones { get; set; }
        public virtual DbSet<St4.PartPermission> PartPermissions { get; set; }
        public virtual DbSet<St4.Property> Properties { get; set; }
        public virtual DbSet<St4.RightGroup> RightGroups { get; set; }

        public virtual DbSet<St4.RefModelPart> RefModelParts { get; set; }
        public virtual DbSet<St4.RefModelRule> RefModelRules { get; set; }


        public TpeConfiguratorContext(DbContextOptions<TpeConfiguratorContext> options, IConfiguration configuration)
            : base(options)
        {
            _Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<St4.Part>().HasKey(p => new { p.Parts_Id, p.Parts_Version });
            modelBuilder.Entity<Property>().HasKey(p => p.Properties_Id);
            modelBuilder.Entity<Localization>().HasKey(l => l.Localization_TextId);
            modelBuilder.Entity<St4.RefModelPart>().HasKey(p => new { p.Parts_Id, p.Parts_Version, p.ConfigVersions_Id });
            modelBuilder.Entity<St4.RefModelRule>().HasKey(p => new { p.RuleMethods_Id, p.RuleMethods_Version, p.ConfigVersions_Id });


            modelBuilder.Entity<Milestone>(entity =>
            {
                entity.HasKey(e => e.Milestones_Id);
            });
            modelBuilder.Entity<RuleMethod>(entity =>
            {
                entity.HasKey(e => new { e.RuleMethods_Id, e.RuleMethods_Version })
                     .HasName("RuleMethod$PrimaryKey");
                entity.Ignore(e => e.RuleMethods_Position);
                entity.Ignore(e => e.RuleMethods_SaveBy);
                entity.Ignore(e => e.RuleMethods_CreatedBy);
                entity.Ignore(e => e.RuleMethods_LockedBy);
            });
            modelBuilder.Entity<RightGroup>(entity =>
            {
                entity.HasKey(e => e.RightGroups_Id);
            });
            modelBuilder.Entity<St4.PartPermission>(entity =>
            {
                entity.HasKey(e => new { e.Parts_Id, e.Parts_Version, e.RightGroups_Id })
                     .HasName("PartPermission$PrimaryKey");
            });
            modelBuilder.Entity<ConfigVersion>(entity =>
            {
                entity.HasKey(e => e.ConfigVersions_Id);
            });
        }
    }
}
