using System.Data.Entity.ModelConfiguration;

namespace Bru2o.Models.Mapping
{
    public class CalcStatsMap : EntityTypeConfiguration<CalcStats>
    {
        public CalcStatsMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Table & Column Mappings
            this.ToTable("CalcStats");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.WaterProfileID).HasColumnName("WaterProfileID");
            this.Property(t => t.pH).HasColumnName("pH");
            this.Property(t => t.CSRatio).HasColumnName("CSRatio");
            this.Property(t => t.EffectiveAlk).HasColumnName("EffectiveAlk");
            this.Property(t => t.ResidualAlk).HasColumnName("ResidualAlk");
            this.Property(t => t.Calcium).HasColumnName("Calcium");
            this.Property(t => t.Magnesium).HasColumnName("Magnesium");
            this.Property(t => t.Sodium).HasColumnName("Sodium");
            this.Property(t => t.Chloride).HasColumnName("Chloride");
            this.Property(t => t.Sulfate).HasColumnName("Sulfate");
            this.Property(t => t.SpargeGypsum).HasColumnName("SpargeGypsum");
            this.Property(t => t.SpargeCalciumChloride).HasColumnName("SpargeCalciumChloride");
            this.Property(t => t.SpargeEpsomSalt).HasColumnName("SpargeEpsomSalt");
            this.Property(t => t.SpargeSlakedLime).HasColumnName("SpargeSlakedLime");
            this.Property(t => t.SpargeBakingSoda).HasColumnName("SpargeBakingSoda");
            this.Property(t => t.SpargeChalk).HasColumnName("SpargeChalk");
            this.Property(t => t.TotalGrainWeight).HasColumnName("TotalGrainWeight");
            this.Property(t => t.MashThickness).HasColumnName("MashThickness");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");

            this.HasRequired(t => t.WaterProfile)
                .WithMany(t => t.CalcStats)
                .HasForeignKey(t => t.WaterProfileID);
        }
    }
}