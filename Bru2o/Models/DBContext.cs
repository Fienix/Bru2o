using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;
using Bru2o.Models.Mapping;

namespace Bru2o.Models
{
    public class Bru2oDBContext : DbContext
    {
        static Bru2oDBContext()
        {
            Database.SetInitializer<Bru2oDBContext>(null);
        }

        public Bru2oDBContext()
            : base("Name=Bru2oContext")
        { }

        public DbSet<WaterProfile> WaterProfiles { get; set; }
        public DbSet<GrainInfo> GrainInfos { get; set; }
        public DbSet<GrainType> GrainTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WaterProfileMap());
            modelBuilder.Configurations.Add(new GrainInfoMap());
            modelBuilder.Configurations.Add(new GrainTypeMap());
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();

            var added = this.ChangeTracker.Entries()
                       .Where(t => t.State == System.Data.Entity.EntityState.Added)
                       .Select(t => t.Entity)
                       .ToArray();

            var modified = this.ChangeTracker.Entries()
                       .Where(t => t.State == System.Data.Entity.EntityState.Modified)
                       .Select(t => t.Entity)
                       .ToArray();

            foreach (var entity in added)
            {
                if (entity is DBCommon)
                {
                    var track = entity as DBCommon;
                    track.CreateDate = DateTime.UtcNow;
                    track.ModifyDate = DateTime.UtcNow;
                }
            }

            foreach (var entity in modified)
            {
                if (entity is DBCommon)
                {
                    var track = entity as DBCommon;
                    track.ModifyDate = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}