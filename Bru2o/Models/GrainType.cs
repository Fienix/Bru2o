using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Bru2o.Models
{
    public class GrainType
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal DefaultPH { get; set; }

        [JsonIgnore]
        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual List<GrainInfo> GrainInfos { get; set; }
    }
}