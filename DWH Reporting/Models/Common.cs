using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DWH_Reporting.Models
{
    public class Common
    {
        public static void recorderror(string modulename, string exception, string username, string linenumber)
        {
            try
            {
                DALMIS obj_dal = new DALMIS();
                obj_dal.ProcName = "InsertErrorLogs";
                SPParametersMIS sp = new SPParametersMIS();
                sp.SetParam("module", SqlDbType.NVarChar, modulename);
                sp.SetParam("expmsg", SqlDbType.NVarChar, exception);
                sp.SetParam("userid", SqlDbType.NVarChar, username);
                sp.SetParam("lineno", SqlDbType.NVarChar, linenumber);
                obj_dal.AddData(sp);
            }
            catch (Exception ex)
            {

            }

        }
        public static DataTable CheckReportAuth(string username, string secretkey)
        {
            try
            {
                DALMIS obj_dal = new DALMIS();
                obj_dal.ProcName = "CheckReportAuthentication";
                SPParametersMIS sp = new SPParametersMIS();
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
                DALMIS obj_dal = new DALMIS();
                obj_dal.ProcName = "UpdateReportAuthStatus";
                SPParametersMIS sp = new SPParametersMIS();
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