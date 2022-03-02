using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Code_Test.Domain.DataModels;

namespace Code_Test.Domain
{
    public class FactorDBContext : DbContext, IFactorDBContext
    {
        public FactorDBContext(DbContextOptions<FactorDBContext> options) : base(options)
        {
        }
        public DbSet<Occupation> Occupations { get; set; }
        public DbSet<OccupationRating> OccupationRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Occupation>()
                .HasKey(o => new { o.Name });
            modelBuilder.Entity<OccupationRating>()
                .HasKey(o => new { o.Rating });
        }

        public void PopulateData()
        {
            if (this.Occupations.Count() > 0)
            {
                // for testing purposes only populate once
                return;
            }

            OccupationRating pro = new()
            {
                Rating = "professional",
                Factor = 1.0
            };
            this.OccupationRatings.Add(pro);
            OccupationRating white = new()
            {
                Rating = "white collar",
                Factor = 1.25
            };
            this.OccupationRatings.Add(white);
            OccupationRating light = new()
            {
                Rating = "light manual",
                Factor = 1.5
            };
            this.OccupationRatings.Add(light);
            OccupationRating heavy = new()
            {
                Rating = "heavy manual",
                Factor = 1.75
            };
            this.OccupationRatings.Add(heavy);

            this.Occupations.Add(new Occupation() { Name = "cleaner", Rating = light.Rating });
            this.Occupations.Add(new Occupation() { Name = "doctor", Rating = pro.Rating });
            this.Occupations.Add(new Occupation() { Name = "author", Rating = white.Rating });
            this.Occupations.Add(new Occupation() { Name = "farmer", Rating = heavy.Rating });
            this.Occupations.Add(new Occupation() { Name = "mechanic", Rating = heavy.Rating });
            this.Occupations.Add(new Occupation() { Name = "florist", Rating = light.Rating });

            this.SaveChanges();
        }
    }
}
