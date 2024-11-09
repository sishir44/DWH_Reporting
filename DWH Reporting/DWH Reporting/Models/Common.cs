using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DWH_Reporting.Models
{
    public class Common
    {

        public static DataTable CheckReportAuth(string username, string secretkey)
        {
            try
            {
                DAL obj_dal = new DAL();
                obj_dal.ProcName = "CheckReportAuthentication";
                SPParameters sp = new SPParameters();
                sp.SetParam("username", SqlDbType.NVarChar, username);
                sp.SetParam("secretkey", SqlDbType.NVarChar, secretkey);
                return obj_dal.Getdata(sp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        internal static string UpdateAuthStatus(string authid)
        {
            try
            {
                DAL obj_dal = new DAL();
                obj_dal.ProcName = "UpdateReportAuthStatus";
                SPParameters sp = new SPParameters();
                sp.SetParam("recid", SqlDbType.NVarChar, authid);
                return obj_dal.AddData(sp);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}