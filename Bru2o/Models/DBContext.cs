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
        public DbSet<CalcStats> CalcStats { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WaterProfileMap());
            modelBuilder.Configurations.Add(new GrainInfoMap());
            modelBuilder.Configurations.Add(new GrainTypeMap());
            modelBuilder.Configurations.Add(new CalcStatsMap());
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

            int i = SaveChangesBase();

            return i;
        }

        public int SaveChangesBase()
        {
            int i = 0;
            try
            {
                i = base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        System.Diagnostics.Debug.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
            return i;
        }
    }
}