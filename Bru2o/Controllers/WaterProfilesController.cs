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
using MvcFlashMessages;
using Newtonsoft.Json;

namespace Bru2o.Controllers
{
    public class WaterProfilesController : BaseController
    {
        // GET: WaterProfiles
        public ActionResult Index()
        {
            return View(db.WaterProfiles.Where(x => x.UserID == ah.UserID).ToList());
        }

        [HttpPost]
        public ActionResult IndexLocal(string[] data)
        {
            if (data == null) { return Json(new {success = false, responseText = "Data is null."}); }

            List<ProfileData> d = new List<ProfileData>();
            foreach (string p in data)
            {
                d.Add(JsonConvert.DeserializeObject<ProfileData>(p));
            }

            List<WaterProfile> viewModel = d.Select(x => x.WaterProfile).ToList();

            return Json(new
            {
                success = true,
                html = RenderRazorViewToString(ControllerContext, "_IndexTableContent", viewModel)
            });
        }

        // GET: WaterProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfileData data = new ProfileData(id.Value);
            if (data.WaterProfile == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        public ActionResult DetailsLocal()
        {
            ProfileData data = (ProfileData)TempData["waterProfile"];
            return View("Details", data);
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
            List<ModelError> allErrors = new List<ModelError>();

            foreach (GrainInfo g in data.GrainInfos) { if (g.GrainTypeID > 1) { data.WaterProfile.GrainInfos.Add(g); } }
            data.CalcStats.WaterProfile = data.WaterProfile;

            if (ah.UserID == null)
            {
                data.LocalStorageID = GenerateLocalStorageID();
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                    Formatting = Formatting.Indented
                };

                data.WaterProfile.CreateDate = DateTime.Now;
                data.WaterProfile.ModifyDate = DateTime.Now;
                data.JsonData = JsonConvert.SerializeObject(data, settings);
                TempData["waterProfile"] = data;
                return RedirectToAction("DetailsLocal");
            }

            if (ModelState.IsValid)
            {
                data.WaterProfile.UserID = ah.UserID;
                db.CalcStats.Add(data.CalcStats);
                db.WaterProfiles.Add(data.WaterProfile);
                db.SaveChanges();
                this.Flash("success", "Successfully created " + data.WaterProfile.Title + ".");
                return RedirectToAction("Index");
            }
            else
            {
                allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                this.Flash("error", ah.GetFlashErrorString(allErrors));
                return View(data);
            }
        }

        private string GenerateLocalStorageID()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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

        public ActionResult EditLocal()
        {
            ProfileData data = (ProfileData)TempData["waterProfile"];
            return View("Edit", data);
        }

        [HttpPost]
        public ActionResult EditLocal(string data)
        {
            ProfileData x = JsonConvert.DeserializeObject<ProfileData>(data);
            TempData["waterProfile"] = x;
            return Json(new { url = new UrlHelper(Request.RequestContext).Action("EditLocal", "WaterProfiles") }, JsonRequestBehavior.AllowGet);
        }

        // POST: WaterProfiles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProfileData data)
        {
            WaterProfile wp = db.WaterProfiles.Where(x => x.UserID == ah.UserID && x.ID == data.WaterProfile.ID).SingleOrDefault();
            CalcStats cs = db.CalcStats.Where(x => x.WaterProfileID == wp.ID).SingleOrDefault();
            if (cs == null)
            {
                cs = new CalcStats();
                cs.WaterProfileID = wp.ID;
                cs.UserID = ah.UserID;
                cs.ID = -1;
                db.CalcStats.Add(cs);
            }

            List<ModelError> allErrors = new List<ModelError>();
            if (ModelState.IsValid)
            {
                ProcessWaterProfile(data, wp);
                ProcessGrainInfo(data, wp);
                ProcessCalcStats(data, cs);
                db.SaveChanges();
                this.Flash("success", "Successfully modified " + data.WaterProfile.Title + ".");
                return RedirectToAction("Index");
            }
            else
            {
                allErrors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                this.Flash("error", ah.GetFlashErrorString(allErrors));
                return View(data);
            }
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
            wp.ManualPH = data.WaterProfile.ManualPH;
        }

        private void ProcessCalcStats(ProfileData data, CalcStats cs)
        {
            cs.pH = data.CalcStats.pH;
            cs.CSRatio = data.CalcStats.CSRatio;
            cs.EffectiveAlk = data.CalcStats.EffectiveAlk;
            cs.ResidualAlk = data.CalcStats.ResidualAlk;
            cs.Calcium = data.CalcStats.Calcium;
            cs.Magnesium = data.CalcStats.Magnesium;
            cs.Sodium = data.CalcStats.Sodium;
            cs.Chloride = data.CalcStats.Chloride;
            cs.Sulfate = data.CalcStats.Sulfate;
            cs.SpargeGypsum = data.CalcStats.SpargeGypsum;
            cs.SpargeCalciumChloride = data.CalcStats.SpargeCalciumChloride;
            cs.SpargeEpsomSalt = data.CalcStats.SpargeEpsomSalt;
            cs.SpargeSlakedLime = data.CalcStats.SpargeSlakedLime;
            cs.SpargeBakingSoda = data.CalcStats.SpargeBakingSoda;
            cs.SpargeChalk = data.CalcStats.SpargeChalk;
            cs.TotalGrainWeight = data.CalcStats.TotalGrainWeight;
            cs.MashThickness = data.CalcStats.MashThickness;
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
                    existingG.Weight = g.Weight;
                }
                else if (existingG == null && g.GrainTypeID > 1) { g.ID = -1; wp.GrainInfos.Add(g); }
            }
        }
        #endregion

        #region Delete
        // POST: WaterProfiles/Delete/5
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            WaterProfile waterProfile = db.WaterProfiles.Where(x => x.UserID == ah.UserID && x.ID == id).SingleOrDefault();
            if (waterProfile != null)
            {
                db.WaterProfiles.Remove(waterProfile);
                db.SaveChanges();

                this.Flash("success", "Deleted " + waterProfile.Title + ".");
            }
            else { this.Flash("error", "Water Profile does not exist."); }

            return Json(new { Url = new UrlHelper(Request.RequestContext).Action("Index", "WaterProfiles") });

        }
        #endregion

        public ActionResult GetGrainTypes()
        {
            return Json(db.GrainTypes.ToList(), JsonRequestBehavior.AllowGet);
        }

        private string RenderRazorViewToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var sw = new System.IO.StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
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