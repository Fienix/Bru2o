using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bru2o.Models
{
    public class WaterProfile : DBCommon
    {
        public WaterProfile()
        {
            this.GrainInfos = new HashSet<GrainInfo>();
        }

        [Key]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
        public int StartingCalcium { get; set; }
        public int StartingMagnesium { get; set; }
        public int StartingSodium { get; set; }
        public int StartingChloride { get; set; }
        public int StartingSulfate { get; set; }
        public int StartingAlkalinity { get; set; }
        public decimal GallonsMashWater { get; set; }
        public int MashWaterDilution { get; set; }
        public decimal GallonsSpargeWater { get; set; }
        public int SpargeWaterDilution { get; set; }
        public decimal Gypsum { get; set; }
        public decimal CalciumChloride { get; set; }
        public decimal EpsomSalt { get; set; }
        public decimal AcidulatedMalt { get; set; }
        public decimal LacticAcid { get; set; }
        public decimal SlakedLime { get; set; }
        public decimal BakingSoda { get; set; }
        public decimal Chalk { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual ICollection<GrainInfo> GrainInfos { get; set; }
    }
}