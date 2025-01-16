using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DWH_Reporting.Models
{
    public class DALMIS
    {
        public static string GetConnectionstring()
        {
            string strconnectionstring = ConfigurationManager.ConnectionStrings["APIConnStr2"].ToString();
            return strconnectionstring;
        }


        SqlConnection cn;
        SqlCommand cmd;
        SqlDataAdapter adap = new SqlDataAdapter();

        public static string strconnectionstring = GetConnectionstring();

        private string sProcName;
        private string sqlqry;

        public DALMIS()
        {

            cn = new SqlConnection();
            cmd = new SqlCommand();
            cn.ConnectionString = strconnectionstring;

        }

        public string ProcName
        {
            get
            {
                return sProcName;
            }
            set
            {
                sProcName = value;
            }
        }

        public string SQLQuery
        {
            get
            {
                return sqlqry;
            }
            set
            {
                sqlqry = value;
            }
        }

        public DataTable GetQryData(string sqlquery)
        {

            DataTable dt = new DataTable();

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sqlquery;
            cmd.Connection = cn;


            adap.SelectCommand = cmd;
            int a = adap.Fill(dt);

            return dt;

        }

        public string AddData(string sqlquery)
        {
            int result = 0;
            string strMessage = "";
            try
            {
                cmd.CommandText = sqlquery;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cn.Open();

                result = cmd.ExecuteNonQuery();

                if (result == 0)
                    strMessage = "No Record Updated";
                else
                    strMessage = "Operation was successful";
            }
            catch (SqlException ex)
            {
                return @"Error has occured :  " + ex.Message;
            }

            finally
            {
                cn.Close();
            }
            return strMessage;

        }

        public DataTable GetQryData(SPParametersMIS spparam)
        {
            try
            {
                // SPParameters spparam = new SPParameters();

                DataTable dt = new DataTable();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlqry;
                cmd.Connection = cn;

                int i = 0;
                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                    cmd.Parameters.Add(pData.pName, pData.pDataType);
                    cmd.Parameters[i].Value = pData.pValue;
                    i = i + 1;
                }


                cn.Open();
                cmd.Connection = cn;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    cn.Close();

                    int count = ds.Tables[0].Rows.Count;

                    // Return the dataset
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;

            }

        }

        public string AddQryData(SPParametersMIS spparam)
        {

            int result = 0;
            string strMessage = "";
            try
            {
                cmd.CommandText = sqlqry;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                int i = 0;



                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                    cmd.Parameters.Add(pData.pName, pData.pDataType);

                    if (pData.pValue != null)
                    {
                        cmd.Parameters[i].Value = pData.pValue;
                    }
                    else if (pData.pic != null)
                    {

                        cmd.Parameters[i].Value = pData.pic;
                    }
                    i = i + 1;
                }


                cn.Open();

                result = int.Parse(cmd.ExecuteNonQuery().ToString());

                if (result == 0)
                    strMessage = "No Record Updated";
                else if (result == -1)
                    strMessage = "No Record Updated";
                else
                    strMessage = "Operation was successful";
            }
            catch (SqlException ex)
            {
                return @"Error has occured :  " + ex.Message;
            }

            finally
            {
                cmd.Parameters.Clear();
                cn.Close();
            }
            return result.ToString();

        }

        public DataTable Getdata(SPParametersMIS spparam)
        {
            try
            {
                // SPParameters spparam = new SPParameters();

                DataTable dt = new DataTable();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = sProcName;
                cmd.Connection = cn;
                cmd.CommandTimeout = 160;

                int i = 0;
                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                    cmd.Parameters.Add(pData.pName, pData.pDataType);
                    cmd.Parameters[i].Value = pData.pValue;
                    i = i + 1;
                }


                cn.Open();
                cmd.Connection = cn;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();

                    // Fill the DataSet using default values for DataTable names, etc
                    da.Fill(ds);

                    // Detach the SqlParameters from the command object, so they can be used again
                    cmd.Parameters.Clear();

                    cn.Close();

                    int count = ds.Tables[0].Rows.Count;

                    // Return the dataset
                    return ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                return dt;

            }

        }

        public string AddData(SPParametersMIS spparam)
        {

            int result = 0;
            string strMessage = "";
            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                int i = 0;



                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                    cmd.Parameters.Add(pData.pName, pData.pDataType);

                    if (pData.pValue != null)
                    {
                        cmd.Parameters[i].Value = pData.pValue;
                    }
                    else if (pData.pic != null)
                    {

                        cmd.Parameters[i].Value = pData.pic;
                    }
                    i = i + 1;
                }


                cn.Open();

                result = int.Parse(cmd.ExecuteNonQuery().ToString());

                if (result == 0)
                    strMessage = "No Record Updated";
                else if (result == -1)
                    strMessage = "No Record Updated";
                else
                    strMessage = "Operation was successful";
            }
            catch (SqlException ex)
            {
                return @"Error has occured :  " + ex.Message;
            }

            finally
            {
                cmd.Parameters.Clear();
                cn.Close();
            }
            return result.ToString();

        }

        public string AddDataRetScalar(SPParametersMIS spparam)
        {

            string result = "";
            string strMessage = "";
            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandTimeout = 0;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                int i = 0;



                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                    cmd.Parameters.Add(pData.pName, pData.pDataType);

                    if (pData.pValue != null)
                    {
                        cmd.Parameters[i].Value = pData.pValue;
                    }
                    else if (pData.pic != null)
                    {

                        cmd.Parameters[i].Value = pData.pic;
                    }
                    i = i + 1;
                }


                cn.Open();

                result = Convert.ToString(cmd.ExecuteScalar());

                //if (result == "")
                //    strMessage = "No Record Updated";
                //else if (result == "")
                //    strMessage = "No Record Updated";
                //else
                //    strMessage = "Operation was successful";
            }
            catch (SqlException ex)
            {
                return @"Error has occured :  " + ex.Message;
            }

            finally
            {
                cmd.Parameters.Clear();
                cn.Close();
            }
            return result.ToString();

        }

        public string AddandInsert(SPParametersMIS spparam)
        {

            int result = 0;
            string res = "";
            string strMessage = "";
            try
            {
                cmd.CommandText = sProcName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;

                int i = 0;
                SqlParameter output = new SqlParameter();

                IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
                while (myEnumerator.MoveNext())
                {
                    ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;

                    if (pData.pDirection == ParameterDirection.Output)
                    {
                        output = cmd.Parameters.Add(pData.pName, pData.pDataType);
                    }
                    else
                    {
                        cmd.Parameters.Add(pData.pName, pData.pDataType);
                    }

                    if (pData.pValue != null)
                    {
                        cmd.Parameters[i].Value = pData.pValue;
                        cmd.Parameters[i].Direction = pData.pDirection;

                    }
                    else if (pData.pic != null)
                    {

                        cmd.Parameters[i].Value = pData.pic;
                    }
                    else if (pData.pDirection == ParameterDirection.Output)
                    {
                        cmd.Parameters[i].Direction = pData.pDirection;
                    }

                    i = i + 1;
                }


                cn.Open();

                result = int.Parse(cmd.ExecuteNonQuery().ToString());

                res = output.Value.ToString();

                if (result == 0)
                    strMessage = "No Record Updated";
                else if (result == -1)
                    strMessage = "No Record Updated";
                else
                    strMessage = "Operation was successful";
            }
            catch (SqlException ex)
            {
                return "0";
            }

            finally
            {
                cmd.Parameters.Clear();
                cn.Close();
            }
            return res;

        }

        public DataSet GetMultipleTables(SPParametersMIS spparam)
        {
            DataSet dataSet = new DataSet();

            // Assuming you have a method to execute the stored procedure and return a DataSet
            // Replace this with your actual DAL method
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sProcName;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;

            int i = 0;
            IEnumerator myEnumerator = spparam.GetParams().GetEnumerator();
            while (myEnumerator.MoveNext())
            {
                ParamDataMIS pData = (ParamDataMIS)myEnumerator.Current;
                cmd.Parameters.Add(pData.pName, pData.pDataType);
                cmd.Parameters[i].Value = pData.pValue;
                i = i + 1;
            }

            //foreach (var param in spparam.sParams)
            //    {
            //        cmd.Parameters.Add(new SqlParameter(param.Key, param.Value.Type) { Value = param.Value.Value });
            //    }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataSet);
            //}

            return dataSet;
        }

    }

    struct ParamDataMIS
    {
        public string pName, pValue;
        public byte[] pic;
        public SqlDbType pDataType;
        public ParameterDirection pDirection;

        public ParamDataMIS(string pName, SqlDbType pDataType, string pValue)
        {
            this.pName = pName;
            this.pDataType = pDataType;
            this.pValue = pValue;
            this.pic = null;
            this.pDirection = ParameterDirection.Input;

        }

        public ParamDataMIS(string pName, SqlDbType pDataType, byte[] pValue)
        {

            this.pName = pName;
            this.pDataType = pDataType;
            this.pic = pValue;
            this.pValue = null;
            this.pDirection = ParameterDirection.Input;

        }

        public ParamDataMIS(string pName, SqlDbType pDataType, string pValue, ParameterDirection pDirection)
        {
            this.pName = pName;
            this.pDataType = pDataType;
            this.pValue = pValue;
            this.pic = null;
            this.pDirection = pDirection;
        }

        public ParamDataMIS(string pName, SqlDbType pDataType, ParameterDirection pDirection)
        {
            this.pName = pName;
            this.pDataType = pDataType;
            this.pValue = null;
            this.pic = null;
            this.pDirection = pDirection;
        }


    }

    public class SPParametersMIS : System.Collections.CollectionBase
    {

        public ArrayList sParams = new ArrayList();


        public void SetParam(string pName, SqlDbType pDataType, string pValue)
        {

            ParamDataMIS pData = new ParamDataMIS(pName, pDataType, pValue);
            sParams.Add(pData);
        }

        public void SetParam(string pName, SqlDbType pDataType, string pValue, ParameterDirection pDirection)
        {

            ParamDataMIS pData = new ParamDataMIS(pName, pDataType, pValue, pDirection);
            sParams.Add(pData);
        }

        public void SetParam(string pName, SqlDbType pDataType, ParameterDirection pDirection)
        {

            ParamDataMIS pData = new ParamDataMIS(pName, pDataType, pDirection);
            sParams.Add(pData);
        }

        public void SetParam(string pName, SqlDbType pDataType, byte[] pValue)
        {

            ParamDataMIS pData = new ParamDataMIS(pName, pDataType, pValue);
            sParams.Add(pData);
        }


        public ArrayList GetParams()
        {
            if (!(sParams == null))
            {
                return sParams;
            }
            else
            {
                return null;

            }

        }
    }
}