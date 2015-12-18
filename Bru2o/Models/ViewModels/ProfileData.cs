using Bru2o.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Bru2o.Models.ViewModels
{
    public class ProfileData
    {
        Bru2oDBContext db = new Bru2oDBContext();

        public WaterProfile WaterProfile { get; set; }
        public List<GrainInfo> GrainInfos { get; set; }
        public List<GrainType> GrainTypes { get; set; }

        public ProfileData() : base() 
        {
            this.WaterProfile = new WaterProfile();
            this.WaterProfile.UserID = AppHelper.UserID;
            this.GrainInfos = new List<GrainInfo>();
            for (int i = 0;i < 8;i++) { this.GrainInfos.Add(new GrainInfo(AppHelper.UserID, this.WaterProfile.ID)); }
            this.GrainTypes = db.GrainTypes.ToList();
        }

        public ProfileData(int waterProfileID)
        {
            this.WaterProfile = db.WaterProfiles.Where(x => x.ID == waterProfileID && x.UserID == AppHelper.UserID).SingleOrDefault();

            if (WaterProfile != null)
            {
                this.GrainInfos =
                    db.GrainInfos.Where(x => x.WaterProfileID == waterProfileID && x.UserID == AppHelper.UserID)
                        .ToList();
                this.GrainTypes = db.GrainTypes.ToList();

                int newGrainInfoCount = 8 - this.GrainInfos.Count();
                for (int i = 0; i < newGrainInfoCount; i++)
                {
                    this.GrainInfos.Add(new GrainInfo(AppHelper.UserID, this.WaterProfile.ID));
                }
            }
        }
    }
}