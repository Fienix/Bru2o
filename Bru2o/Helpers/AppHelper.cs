using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Bru2o.Helpers
{
    public class AppHelper
    {
        public string UserID 
        { 
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        public string GetFlashErrorString(List<ModelError> allErrors)
        {
            string errors = "";
            int i = 0;
            foreach (ModelError error in allErrors)
            {
                if (i == 0) { errors = error.ErrorMessage; }
                else { errors = errors + "<br />" + error.ErrorMessage; }

                i++;
            }

            return errors;
        }
    }
}