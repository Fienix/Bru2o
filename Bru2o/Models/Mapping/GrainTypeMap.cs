using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Bru2o.Models.Mapping
{
    public class GrainTypeMap : EntityTypeConfiguration<GrainType>
    {
        public GrainTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Table & Column Mappings
            this.ToTable("GrainTypes");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.DefaultPH).HasColumnName("DefaultPH");
        }
    }
}