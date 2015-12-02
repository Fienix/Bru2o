using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bru2o.Models
{
    interface DBCommon
    {
        System.DateTime CreateDate { get; set; }
        System.DateTime ModifyDate { get; set; }
    }
}