using System.Data.Entity.ModelConfiguration;

namespace Bru2o.Models.Mapping
{
    public class GrainInfoMap : EntityTypeConfiguration<GrainInfo>
    {
        public GrainInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Table & Column Mappings
            this.ToTable("GrainInfos");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.WaterProfileID).HasColumnName("WaterProfileID");
            this.Property(t => t.GrainTypeID).HasColumnName("GrainTypeID");
            this.Property(t => t.Weight).HasColumnName("Weight");
            this.Property(t => t.Color).HasColumnName("Color");
            this.Property(t => t.MashPH).HasColumnName("MashPH");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.WaterProfile)
                .WithMany(t => t.GrainInfos)
                .HasForeignKey(t => t.WaterProfileID);

            this.HasRequired(t => t.GrainType)
                .WithMany(t => t.GrainInfos)
                .HasForeignKey(t => t.GrainTypeID);
        }
    }
}