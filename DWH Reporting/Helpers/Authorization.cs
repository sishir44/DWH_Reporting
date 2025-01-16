using DWH_Reporting.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DWH_Reporting.Helpers
{
    public class Authorization : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var accessdeniedurl = System.Configuration.ConfigurationManager.AppSettings["accessdeniedurl"];
            var ReceivedToken = filterContext.HttpContext.Request.QueryString["token"];
            if (String.IsNullOrEmpty(ReceivedToken))
            {
                filterContext.Result = new RedirectResult(accessdeniedurl);
            }
            else
            {
                var Token = ReceivedToken.Replace('`', '.');
                ClaimsPrincipal claims = JwtManager.GetPrincipal(Token);
                if(claims == null)
                {
                    filterContext.Result = new RedirectResult(accessdeniedurl);
                }
                else
                {
                    //Get first claim of all types 
                    Claim userclaim = claims.Claims.Where(a => a.Type == ClaimTypes.Name).First();
                    Claim hashclaim = claims.Claims.Where(a => a.Type == ClaimTypes.Hash).First();
                    Claim useridclaim = claims.Claims.Where(a => a.Type == ClaimTypes.Sid).First();
                    //Get claim values from claims 
                    var username = userclaim.Value.ToString();
                    var hash = hashclaim.Value.ToString();
                    var userid = useridclaim.Value.ToString();
                    Common.recorderror("username", username + "-" + userid, "", "");
                    DataTable dt = Common.CheckReportAuth(username, hash);
                    var session = filterContext.HttpContext.Session;

             
                    if (session["isValid"] != null && dt.Rows.Count == 0)
                    {
                        session["username"] = username;
                        session["userid"] = userid;
                        Common.recorderror("sessionuserid", userid, "", "");
                    }
                    else if (dt.Rows.Count == 0)
                    {
                        filterContext.Result = new RedirectResult(System.Configuration.ConfigurationManager.AppSettings["accessdeniedurl"]);
                    }
                    else
                    {
                        var authid = dt.Rows[0]["ID"].ToString();
                        var updateres = Common.UpdateAuthStatus(authid);
                        session["isValid"] = "access hai";
                        session["username"] = username;
                        session["userid"] = userid;
                    }
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}