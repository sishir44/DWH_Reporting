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
        public static DataTable GetFct_StoreSummary(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_Summary";

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
        
        public static DataTable FilterTotal(string dateParam, string Sd, string Tm, string Market, string MMM, string Store, string Tiers)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_StoreNumberTotal_filtereData";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("@DateParam", SqlDbType.DateTime, dateParam);
                spParam.SetParam("@Sd", SqlDbType.NVarChar, Sd);
                spParam.SetParam("@Tm", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Tm) ? null : Tm);
                spParam.SetParam("@Market", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Market) ? null : Market);
                spParam.SetParam("@MMM", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(MMM) ? null : MMM);
                spParam.SetParam("@Store", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Store) ? null : Store);
                spParam.SetParam("@Tiers", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Tiers) ? null : Tiers);

                DataTable DT = objDal.Getdata(spParam);
                return objDal.Getdata(spParam);
            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable EmpDet(string UniqueID, string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GETMTDEmployee_Detail";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("@Unique", SqlDbType.Int, UniqueID);
                spParam.SetParam("@date", SqlDbType.DateTime, dateParam);

                DataTable DT = objDal.Getdata(spParam);
                return objDal.Getdata(spParam);
            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public static DataTable SummaryFilterTotal(string dateParam, string Sd, string Tm, string Market, string MMM, string Store, string Tiers)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetRecords_summaryTotal";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("@DateParam", SqlDbType.DateTime, dateParam);
                spParam.SetParam("@Sd", SqlDbType.NVarChar, Sd);
                spParam.SetParam("@Tm", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Tm) ? null : Tm);
                spParam.SetParam("@Market", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Market) ? null : Market);
                spParam.SetParam("@MMM", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(MMM) ? null : MMM);
                spParam.SetParam("@Store", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Store) ? null : Store);
                spParam.SetParam("@Tiers", SqlDbType.NVarChar, string.IsNullOrWhiteSpace(Tiers) ? null : Tiers);

                DataTable DT = objDal.Getdata(spParam);
                return objDal.Getdata(spParam);
            }

            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }
        public static DataTable GetFct_EmployeeNumberTotal(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_EmployeeNumberTotal";

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
        public static DataTable Get_MUL_Mgr()
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "Get_MUL_Mgr";

                SPParameters spParam = new SPParameters();
                return objDal.Getdata(spParam);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                DataTable dt = new DataTable();
                return dt;
            }
        }
        public static DataTable GetFct_StoreNumberTotalTM(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_StoreNumberTotalTM";

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
        public static DataTable GetFct_StoreNumberTotalMMM(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetFct_StoreNumberTotalMMM";

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

        public static DataTable GetRecords_summaryTotal(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetRecords_summaryTotal";
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
        
        public static DataTable GetNoteByParameters(string UniqueID_Param)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "GetNoteByParameters";

                // Create SPParameters object and add the date parameter
                SPParameters spParam = new SPParameters();
                spParam.SetParam("@UniqueID", SqlDbType.NVarChar, UniqueID_Param);

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


        public static DataTable spUpdateNote(string UniqueID, string DealerCode, string DateKey, string Comment, string Note, string Employee)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "spUpdateNote";

                // Create SPParameters object and add the date parameter
                SPParameters spParam = new SPParameters();
                spParam.SetParam("@UniqueID", SqlDbType.NVarChar, UniqueID);
                spParam.SetParam("@DealerCode", SqlDbType.NVarChar, DealerCode);
                spParam.SetParam("@DateKey", SqlDbType.NVarChar, DateKey);
                spParam.SetParam("@Comment", SqlDbType.NVarChar, Comment);
                spParam.SetParam("@Note", SqlDbType.NVarChar, Note);
                spParam.SetParam("@Employee", SqlDbType.NVarChar, Employee);

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


        public static DataTable spInsertNote(string UniqueID, string DealerCode, string DateKey, string Comment, string Note, string Employee)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "spInsertNote";

                // Create SPParameters object and add the date parameter
                SPParameters spParam = new SPParameters();
                spParam.SetParam("@UniqueID", SqlDbType.NVarChar, UniqueID);
                spParam.SetParam("@DealerCode", SqlDbType.NVarChar, DealerCode);
                spParam.SetParam("@DateKey", SqlDbType.NVarChar, DateKey);
                spParam.SetParam("@Comment", SqlDbType.NVarChar, Comment);
                spParam.SetParam("@Note", SqlDbType.NVarChar, Note);
                spParam.SetParam("@Employee", SqlDbType.NVarChar, Employee);

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

        public static bool CheckIfNoteExists(string uniqueID)
        {
            string UniqueID_Param = uniqueID;
            bool res;

            DataTable GetNoteByParameters = GP_DAL_Functions.GetNoteByParameters(UniqueID_Param);

            if (GetNoteByParameters.Rows.Count > 0)
            {
                return res = true;
            }

            return res = false;
        }

        public static DataTable StoreNumberUploaded(string dateParam)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "RunTempEmpCommission";

                SPParameters spParam = new SPParameters();
                spParam.SetParam("@BackDate", SqlDbType.Date, dateParam);

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