using Bru2o.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bru2o.Models.ViewModels
{
    public class ProfileData : BaseData
    {
        public WaterProfile WaterProfile { get; set; }
        public List<GrainInfo> GrainInfos { get; set; }
        public List<GrainType> GrainTypes { get; set; }
        public CalcStats CalcStats { get; set; }

        public ProfileData() : base() 
        {
            this.WaterProfile = new WaterProfile();
            this.WaterProfile.UserID = ah.UserID;
            this.CalcStats = new CalcStats();
            this.CalcStats.UserID = ah.UserID;
            this.CalcStats.WaterProfileID = this.WaterProfile.ID;
            this.GrainInfos = new List<GrainInfo>();
            for (int i = 0;i < 8;i++) { this.GrainInfos.Add(new GrainInfo(ah.UserID, this.WaterProfile.ID)); }
            this.GrainTypes = db.GrainTypes.ToList();
        }

        public ProfileData(int waterProfileID)
        {
            this.WaterProfile = db.WaterProfiles.Where(x => x.ID == waterProfileID && x.UserID == ah.UserID).SingleOrDefault();
            this.CalcStats = db.CalcStats.Where(x => x.WaterProfileID == this.WaterProfile.ID).SingleOrDefault();

            if (WaterProfile != null)
            {
                this.GrainInfos =
                    db.GrainInfos.Where(x => x.WaterProfileID == waterProfileID && x.UserID == ah.UserID)
                        .ToList();
                this.GrainTypes = db.GrainTypes.ToList();

                int newGrainInfoCount = 8 - this.GrainInfos.Count();
                for (int i = 0; i < newGrainInfoCount; i++)
                {
                    this.GrainInfos.Add(new GrainInfo(ah.UserID, this.WaterProfile.ID));
                }
            }
        }
    }
}