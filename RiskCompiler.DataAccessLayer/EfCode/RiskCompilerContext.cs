using RiskCompiler.DataAccessLayer.EfClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskCompiler.DataAccessLayer.EfCode
{
    public class RiskCompilerContext : DbContext
    {
        public DbSet<RiskAssessment> RiskAssessments { get; set; } = null!;
        
        public RiskCompilerContext(DbContextOptions<RiskCompilerContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseSerialColumns();

            //modelBuilder.Entity<RiskAssessment>()
            //    .Property(ra => ra.RiskAssessmentId)
            //    .ValueGeneratedOnAdd();

            // 
            // Many-to-many: RiskAssessmentRelation
            //

            modelBuilder.Entity<RiskAssessmentRelation>()
                .HasKey(raRel => new { raRel.DescendantId, raRel.AncestorId });

            modelBuilder.Entity<RiskAssessmentRelation>()
                .HasOne(raRel => raRel.Ancestor)
                .WithMany(ra => ra.Descendants)
                .HasForeignKey(raRel => raRel.AncestorId);

            modelBuilder.Entity<RiskAssessmentRelation>()
                .HasOne(raRel => raRel.Descendant)
                .WithMany(ra => ra.Ancestors)
                .HasForeignKey(raRel => raRel.DescendantId);

            //modelBuilder.Entity<LineItem>()
            //    .HasOne(p => p.ChosenBook)
            //    .WithMany()
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Book>()
            //    .HasQueryFilter(p => !p.SoftDeleted);

            modelBuilder.Entity<RiskAssessment>().HasData(
                new RiskAssessment
                {
                    RiskAssessmentId = -1,
                    RiskAssessmentGuid = Guid.NewGuid(),
                    Title = "Root",
                    Description = "Holds customer wide entities",
                    IsCoreRiskAssessment = true
                });
        }
    }
}
