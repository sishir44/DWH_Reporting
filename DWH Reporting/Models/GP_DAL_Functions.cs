using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DWH_Reporting.Models
{
    public class GP_DAL_Functions
    {
        public static DataTable GP_Raw_Data()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetReportData";
                //  objDal.ProcName = "GetGPDataByDate";
                SPParameters spParam = new SPParameters();

                spParam.SetParam("ReportName", SqlDbType.VarChar, "GPReport");
                //spParam.SetParam("province_code", SqlDbType.VarChar, provincecode);


                DataTable dt = objDal.Getdata(spParam);

                if (dt.Rows.Count <= 0)
                {
                    dt.Clear();
                    dt = GetPreviousReportData();
                }

                return dt;

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GP_Raw_DataDir()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetReportDataDir";
                //  objDal.ProcName = "GetGPDataByDate";
                SPParameters spParam = new SPParameters();

                spParam.SetParam("ReportName", SqlDbType.VarChar, "GPReport");
                //spParam.SetParam("province_code", SqlDbType.VarChar, provincecode);


                DataTable dt = objDal.Getdata(spParam);

                if (dt.Rows.Count <= 0)
                {
                    dt.Clear();
                    dt = GetPreviousReportData();
                }

                return dt;

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GetCalcValues()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "sp_GetCalcValues";
                //  objDal.ProcName = "GetGPDataByDate";
                SPParameters spParam = new SPParameters();

                spParam.SetParam("reportid", SqlDbType.Int, "1");
                //spParam.SetParam("province_code", SqlDbType.VarChar, provincecode);


                DataTable dt = objDal.Getdata(spParam);

                return dt;

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GetAllDataForMarkets()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetMarkets";

                SPParameters spParam = new SPParameters();
                return objDal.Getdata(spParam);

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GetAllUserByDesId(string desid)
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetAllUsersByDesID";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("desid", SqlDbType.Int, desid);
                return objDal.Getdata(spParam);

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        internal static string GetStoreCount()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetStoreCountRD";

                SPParameters spParam = new SPParameters();

                DataTable DT = objDal.Getdata(spParam);
                return DT.Rows[0][0].ToString();
            }

            catch (Exception ex)
            {
                return 0.ToString();
            }
        }

        public static DataTable GetPreviousReportData()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetReportByMaxRepTimeRD";

                SPParameters spParam = new SPParameters();

                spParam.SetParam("ReportName", SqlDbType.VarChar, "GPReport");
                //spParam.SetParam("province_code", SqlDbType.VarChar, provincecode);


                return objDal.Getdata(spParam);

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GetAllDataFromStoreUID(string userId)
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetMarketsByUserID";

                SPParameters spParam = new SPParameters();

                spParam.SetParam("userid", SqlDbType.Int, userId);


                return objDal.Getdata(spParam);

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static string GetDesginationID(string userid)
        {
            string designationid = "";
            try
            {
                DAL objDal = new DAL();

                // objDal.ProcName = "GetReportData";
                objDal.ProcName = "GetUserDesgination";
                SPParameters spParam = new SPParameters();

                spParam.SetParam("userid", SqlDbType.Int, userid);


                DataTable dt = objDal.Getdata(spParam);

                if (dt.Rows.Count > 0)
                {

                    designationid = Convert.ToString(dt.Rows[0][0]);
                }

                return designationid;

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return ex.Message;
            }
        }

        public static DataTable GetRoadRunner()
        {
            try
            {
                DAL objDal = new DAL();

                objDal.ProcName = "GetFocusAndGeoUID";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("key", SqlDbType.NVarChar, "roadrunner");
                return objDal.Getdata(spParam);

            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }

        }

        public static DataTable GetFct_StoreNumberCol2(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_StoreNumberCol2";

                // Create SPParameters object and add the date parameter
                SPParameters spParam = new SPParameters();
                spParam.SetParam("@DateParam", SqlDbType.DateTime, dateParam);

                // Execute stored procedure with the parameters
                DataTable DT = objDal.Getdata(spParam);
                return DT;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable GetFct_StoreNumberTotal(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_StoreNumberTotal";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("@DateParam", SqlDbType.DateTime, dateParam);

                DataTable DT = objDal.Getdata(spParam);
                return objDal.Getdata(spParam);
            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
        public static DataTable GetTrendingCommissionData(string date)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetTrendingCommission";
                SPParameters spParam = new SPParameters();
                spParam.SetParam("@date", SqlDbType.NVarChar, date);
                DataTable DT = objDal.Getdata(spParam);
                return DT;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
    }
}