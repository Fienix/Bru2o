using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;

namespace Bru2o.Models.Mapping
{
    public class WaterProfileMap : EntityTypeConfiguration<WaterProfile>
    {
        public WaterProfileMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Table & Column Mappings
            this.ToTable("WaterProfiles");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.StartingCalcium).HasColumnName("StartingCalcium");
            this.Property(t => t.StartingMagnesium).HasColumnName("StartingMagnesium");
            this.Property(t => t.StartingSodium).HasColumnName("StartingSodium");
            this.Property(t => t.StartingChloride).HasColumnName("StartingChloride");
            this.Property(t => t.StartingSulfate).HasColumnName("StartingSulfate");
            this.Property(t => t.StartingAlkalinity).HasColumnName("StartingAlkalinity");
            this.Property(t => t.GallonsMashWater).HasColumnName("GallonsMashWater");
            this.Property(t => t.MashWaterDilution).HasColumnName("MashWaterDilution");
            this.Property(t => t.GallonsSpargeWater).HasColumnName("GallonsSpargeWater");
            this.Property(t => t.SpargeWaterDilution).HasColumnName("SpargeWaterDilution");
            this.Property(t => t.Gypsum).HasColumnName("Gypsum");
            this.Property(t => t.CalciumChloride).HasColumnName("CalciumChloride");
            this.Property(t => t.EpsomSalt).HasColumnName("EpsomSalt");
            this.Property(t => t.AcidulatedMalt).HasColumnName("AcidulatedMalt");
            this.Property(t => t.LacticAcid).HasColumnName("LacticAcid");
            this.Property(t => t.SlakedLime).HasColumnName("SlakedLime");
            this.Property(t => t.BakingSoda).HasColumnName("BakingSoda");
            this.Property(t => t.Chalk).HasColumnName("Chalk");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
        }
    }
}