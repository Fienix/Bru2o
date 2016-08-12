using Bru2o.Helpers;
using Bru2o.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bru2o.Controllers
{
    public class BaseController : Controller
    {
        protected Bru2oDBContext db = new Bru2oDBContext();
        protected AppHelper ah = new AppHelper();
    }
}