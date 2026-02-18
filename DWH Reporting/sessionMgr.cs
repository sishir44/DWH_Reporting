using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DWH_Reporting
{
    public static class sessionMgr
    {
        public static bool FromAction
        {
            get => HttpContext.Current.Session["FromAction"] != null
                   && (bool)HttpContext.Current.Session["FromAction"];
            set => HttpContext.Current.Session["FromAction"] = false;
        }
    }
}