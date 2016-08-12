using Bru2o.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bru2o.Models.ViewModels
{
    public class BaseData
    {
        protected Bru2oDBContext db = new Bru2oDBContext();
        protected AppHelper ah = new AppHelper();
    }
}