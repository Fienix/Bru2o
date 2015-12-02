using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace Bru2o.Helpers
{
    public static class AppHelper
    {
        public static string UserID 
        { 
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }
    }
}