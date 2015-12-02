﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bru2o.Models
{
    public class GrainInfo : DBCommon
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        public int WaterProfileID { get; set; }
        public int GrainTypeID { get; set; }
        public decimal Weight { get; set; }
        public int? Color { get; set; }
        public decimal MashPH { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual WaterProfile WaterProfile { get; set; }
        public virtual GrainType GrainType { get; set; }

        public GrainInfo() : base() { }
        public GrainInfo(string userID, int waterProfileID)
        {
            this.UserID = userID;
            this.WaterProfileID = waterProfileID;
        }
    }
}