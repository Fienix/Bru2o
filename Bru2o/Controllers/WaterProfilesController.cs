using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Bru2o.Models;
using Bru2o.Models.ViewModels;
using Bru2o.Helpers;

namespace Bru2o.Controllers
{
    [Authorize]
    public class WaterProfilesController : Controller
    {
        private Bru2oDBContext db = new Bru2oDBContext();

        // GET: WaterProfiles
        public ActionResult Index()
        {
            return View(db.WaterProfiles.ToList());
        }

        // GET: WaterProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaterProfile waterProfile = db.WaterProfiles.Find(id);
            if (waterProfile == null)
            {
                return HttpNotFound();
            }
            return View(waterProfile);
        }

        #region Create
        // GET: WaterProfiles/Create
        public ActionResult Create()
        {
            return View(new ProfileData());
        }

        // POST: WaterProfiles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfileData data)
        {
            if (ModelState.IsValid)
            {
                foreach (GrainInfo g in data.GrainInfos) { if (g.GrainTypeID > 1) { data.WaterProfile.GrainInfos.Add(g); } }
                db.WaterProfiles.Add(data.WaterProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(data);
        }
        #endregion

        #region Edit
        // GET: WaterProfiles/Edit/5
        public ActionResult Edit(int id)
        {
            ProfileData data = new ProfileData(id);
            if (data.WaterProfile == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: WaterProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileData data)
        {
            WaterProfile wp = db.WaterProfiles.Find(data.WaterProfile.ID);

            if (ModelState.IsValid)
            {
                ProcessWaterProfile(data, wp);
                ProcessGrainInfo(data, wp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(data);
        }

        private void ProcessWaterProfile(ProfileData data, WaterProfile wp)
        {
            wp.AcidulatedMalt = data.WaterProfile.AcidulatedMalt;
            wp.BakingSoda = data.WaterProfile.BakingSoda;
            wp.CalciumChloride = data.WaterProfile.CalciumChloride;
            wp.Chalk = data.WaterProfile.Chalk;
            wp.EpsomSalt = data.WaterProfile.EpsomSalt;
            wp.GallonsMashWater = data.WaterProfile.GallonsMashWater;
            wp.GallonsSpargeWater = data.WaterProfile.GallonsSpargeWater;
            wp.Gypsum = data.WaterProfile.Gypsum;
            wp.LacticAcid = data.WaterProfile.LacticAcid;
            wp.MashWaterDilution = data.WaterProfile.MashWaterDilution;
            wp.SlakedLime = data.WaterProfile.SlakedLime;
            wp.SpargeWaterDilution = data.WaterProfile.SpargeWaterDilution;
            wp.StartingAlkalinity = data.WaterProfile.StartingAlkalinity;
            wp.StartingCalcium = data.WaterProfile.StartingCalcium;
            wp.StartingChloride = data.WaterProfile.StartingChloride;
            wp.StartingMagnesium = data.WaterProfile.StartingMagnesium;
            wp.StartingSodium = data.WaterProfile.StartingSodium;
            wp.StartingSulfate = data.WaterProfile.StartingSulfate;
            wp.Title = data.WaterProfile.Title;
            db.Entry(wp).State = EntityState.Modified;
        }

        private void ProcessGrainInfo(ProfileData data, WaterProfile wp)
        {
            foreach (GrainInfo g in data.GrainInfos)
            {
                GrainInfo existingG = db.GrainInfos.Find(g.ID);
                if (existingG != null && g.GrainTypeID == 1) { db.GrainInfos.Remove(existingG); }
                else if (existingG != null && g.GrainTypeID > 1)
                {
                    existingG.GrainTypeID = g.GrainTypeID;
                    existingG.Color = g.Color;
                    existingG.MashPH = g.MashPH;
                    db.Entry(existingG).State = EntityState.Modified;
                }
                else if (existingG == null && g.GrainTypeID > 1) { g.ID = -1; wp.GrainInfos.Add(g); }
            }
        }
        #endregion

        #region Delete
        // GET: WaterProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WaterProfile waterProfile = db.WaterProfiles.Find(id);
            if (waterProfile == null)
            {
                return HttpNotFound();
            }
            return View(waterProfile);
        }

        // POST: WaterProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WaterProfile waterProfile = db.WaterProfiles.Find(id);
            foreach (GrainInfo g in waterProfile.GrainInfos) { db.GrainInfos.Remove(g); }
            db.WaterProfiles.Remove(waterProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        public ActionResult GetGrainTypes()
        {
            return Json(db.GrainTypes.ToList(), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}