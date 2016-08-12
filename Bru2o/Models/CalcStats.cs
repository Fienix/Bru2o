using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bru2o.Models
{
    public class CalcStats : DBCommon
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int WaterProfileID { get; set; }
        public decimal pH { get; set; }
        public decimal CSRatio { get; set; }
        public int EffectiveAlk { get; set; }
        public int ResidualAlk { get; set; }
        public int Calcium { get; set; }
        public int Magnesium { get; set; }
        public int Sodium { get; set; }
        public int Chloride { get; set; }
        public int Sulfate { get; set; }
        public decimal SpargeGypsum { get; set; }
        public decimal SpargeCalciumChloride { get; set; }
        public decimal SpargeEpsomSalt { get; set; }
        public decimal SpargeSlakedLime { get; set; }
        public decimal SpargeBakingSoda { get; set; }
        public decimal SpargeChalk { get; set; }
        public decimal TotalGrainWeight { get; set; }
        public decimal MashThickness { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual WaterProfile WaterProfile { get; set; }

        public CalcStats() : base() { }
        public CalcStats(string userID, int waterProfileID)
        {
            this.UserID = userID;
            this.WaterProfileID = waterProfileID;
        }
    }
}