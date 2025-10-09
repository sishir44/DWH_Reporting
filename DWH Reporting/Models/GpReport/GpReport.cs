using System;
using System.Collections.Generic;
using System.Data;
using DWH_Reporting.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DWH_Reporting.Models.GpReport
{
    public class GpReport
    {
        public static string DateTimes;
        internal static List<GpReportAttributesModel> GpTableToList(int a = 0)
        {
            List<GpReportAttributesModel> List = new List<GpReportAttributesModel>();
            try
            {
                DataTable dtCalcValues = GP_DAL_Functions.GetCalcValues();
                int SpifAIA = Convert.ToInt32(dtCalcValues.Rows[0]["AIA_Spif"]);
                DataTable GP_Report_Raw_Data = null;
                if (a == 1)
                    GP_Report_Raw_Data = GP_DAL_Functions.GP_Raw_DataDir();
                else
                    GP_Report_Raw_Data = GP_DAL_Functions.GP_Raw_Data();
                int StoreCount = int.Parse(GP_DAL_Functions.GetStoreCount());

                for (int i = 1; i <= StoreCount; i++)
                {
                    GpReportAttributesModel d = new GpReportAttributesModel();
                    foreach (DataRow dr in GP_Report_Raw_Data.Rows)
                    {
                        DateTimes = dr["TimeStamp"].ToString();
                        if (int.Parse(dr["RowIndex"].ToString()) == i)
                        {
                            if (int.Parse(dr["Attribute_Id"].ToString()) == 1)
                                d.Opus = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 3)
                                d.UID = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 2)
                                d.Dealer_Code = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 53)
                                d.VP = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 54)
                                d.SSM = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 4)
                                d.SD = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 5)
                                d.TM = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 6)
                                d.Market = dr["RepData"].ToString();

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 7)
                                d.Store = dr["RepData"].ToString();

                            // Edited by Ammad dtd: 11/03/2022 due to addition of Addition of Role in Mobily GP Report i.e. MU-SM

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 56)
                                d.MUSM = dr["RepData"].ToString();

                            // End of Edited by Ammad dtd: 11/03/2022 due to Addition of Role in Mobily GP Report i.e. MU-SM

                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 44)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_Today_Goals = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.GP_Today_Goals = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 45)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPS_Today_Goals = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.OPS_Today_Goals = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 11)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.TOTAL_GP = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.TOTAL_GP = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 12)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.Total_Spiff = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.Total_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 13)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.TOTAL_OPPS = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.TOTAL_OPPS = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 14)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_GA = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.OPPS_GA = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 15)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_UPG = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.OPPS_UPG = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 16)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_IPBB = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.OPPS_IPBB = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 17)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_DTV = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.OPPS_DTV = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 18)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_Tab_Wear = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.OPPS_Tab_Wear = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 19)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.OPPS_Prepaid = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.OPPS_Prepaid = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 20)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_GA = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_GA = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 21)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_UPG = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_UPG = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 22)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_IPBB = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_IPBB = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 23)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_DTV = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_DTV = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 24)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_Tab_Wear = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_Tab_Wear = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 25)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.GP_Prepaid = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.GP_Prepaid = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 26)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.NextUp = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.NextUp = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 27)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.Acc = Convert.ToString(dr["RepData"]);
                                }
                                else
                                {
                                    d.Acc = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 28)
                            {
                                if (dr["RepData"].ToString() != "")
                                {
                                    d.Acc_GP = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Acc_GP = "0";
                                }

                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 29)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.CRU_GA = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.CRU_GA = "0";
                                }

                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 30)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.FirstNet_GA = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.FirstNet_GA = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 31)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Trade_In = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Trade_In = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 35)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {

                                    d.Pstpd_Data_GA_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Pstpd_Data_GA_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 36)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.UYW_Elite_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.UYW_Elite_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 37)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Next_Up_Spiff = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Next_Up_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 38)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Upg_QI_Spiff = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Upg_QI_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 39)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Act_QI_Spiff = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Act_QI_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 40)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {

                                    d.BB_QI_Spiff = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.BB_QI_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 41)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Total_Geo_Spiff = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Total_Geo_Spiff = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 42)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Premium_TV = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Premium_TV = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 43)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Geo_Spif_Rate = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Geo_Spif_Rate = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 46)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.GP_Achieved_Per = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.GP_Achieved_Per = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 47)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.OPS_Achieved_Per = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.OPS_Achieved_Per = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 48)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.IRU_Postpaid_Gross_Adds = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.IRU_Postpaid_Gross_Adds = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 49)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.IPBB_100Mb = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.IPBB_100Mb = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 50)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Tot_ProtAdv_Feat_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Tot_ProtAdv_Feat_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 51)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Galaxy_S21_Series = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Galaxy_S21_Series = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 52)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.iPhone_12_Series = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.iPhone_12_Series = "0";
                                }

                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 55)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.GA_Today_Goals = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.GA_Today_Goals = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 57)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.BYOD_PPV_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.BYOD_PPV_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 58)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Postpaid_DF_Voice_Gross_Adds = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Postpaid_DF_Voice_Gross_Adds = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 59)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.HTP_Feat_Add_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.HTP_Feat_Add_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 60)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.IPBB_100MBto300MB_Migr_Cnt = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.IPBB_100MBto300MB_Migr_Cnt = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 61)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Acc_Units_per_Opp = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Acc_Units_per_Opp = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 62)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.Samsung_GS23 = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.Samsung_GS23 = "0";
                                }
                            }
                            else if (int.Parse(dr["Attribute_Id"].ToString()) == 63)
                            {
                                if (!String.IsNullOrEmpty(dr["RepData"].ToString()))
                                {
                                    d.AIA = Convert.ToString(dr["RepData"]);

                                }
                                else
                                {
                                    d.AIA = "0";
                                }
                            }
                            d.AIASpiff = Convert.ToString(Convert.ToInt32(d.AIA) * SpifAIA);
                            try
                            {
                                d.GA_Achieved_Per = Convert.ToString(Calc_Division(Convert.ToDouble(d.OPPS_GA), Convert.ToDouble(d.GA_Today_Goals)));
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }

                    List.Add(d);
                }
            }
            catch (Exception ex)
            {
                return List;
            }



            return List;
        }

        public static List<GpReportAttributesModel_Totals> SumCountGroupWise(List<GpReportAttributesModel> gplist)
        {
            List<GpReportAttributesModel_Totals> TotalModel_List = new List<GpReportAttributesModel_Totals>();
            GpReportAttributesModel_Totals TotalModel = new GpReportAttributesModel_Totals();
            try
            {
                foreach (var GP_Data in gplist)
                {
                    if (!String.IsNullOrEmpty(GP_Data.Market))
                    {
                        TotalModel.Market = GP_Data.Market;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.VP))
                    {
                        TotalModel.VP = GP_Data.VP;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.SSM))
                    {
                        TotalModel.SSM = GP_Data.SSM;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.SD))
                    {
                        TotalModel.SD = GP_Data.SD;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.TM))
                    {
                        TotalModel.TM = GP_Data.TM;
                    }

                    // Edited by Ammad dtd: 11/03/2022 due to addition of Addition of Role in Mobily GP Report i.e. MU-SM

                    if (!String.IsNullOrEmpty(GP_Data.MUSM))
                    {
                        TotalModel.MUSM = GP_Data.MUSM;
                    }

                    // End of Edited by Ammad dtd: 11/03/2022 due to Addition of Role in Mobily GP Report i.e. MU-SM

                    if (!String.IsNullOrEmpty(GP_Data.GP_Today_Goals))
                    {
                        TotalModel.Total_GP_Today_Goals += Convert.ToDecimal(GP_Data.GP_Today_Goals);

                    }
                    else
                    {
                        TotalModel.Total_GP_Today_Goals += 0;
                    }

                    if (!String.IsNullOrEmpty(GP_Data.OPS_Today_Goals))
                    {
                        TotalModel.Total_OPS_Today_Goals += Convert.ToDecimal(GP_Data.OPS_Today_Goals);

                    }
                    else
                    {
                        TotalModel.Total_OPS_Today_Goals += 0;
                    }

                    if (!String.IsNullOrEmpty(GP_Data.TOTAL_GP))
                    {
                        TotalModel.Total_TOTAL_GP += Convert.ToDecimal(GP_Data.TOTAL_GP);

                    }
                    else
                    {
                        TotalModel.Total_TOTAL_GP += 0;
                    }

                    if (!String.IsNullOrEmpty(GP_Data.Total_Spiff))
                    {
                        TotalModel.Total_Total_Spiff += Convert.ToDecimal(GP_Data.Total_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_Total_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.TOTAL_OPPS))
                    {
                        TotalModel.Total_TOTAL_OPPS += Convert.ToDecimal(GP_Data.TOTAL_OPPS);

                    }
                    else
                    {
                        TotalModel.Total_TOTAL_OPPS += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_GA))
                    {
                        TotalModel.Total_OPPS_GA += Convert.ToDecimal(GP_Data.OPPS_GA);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_GA += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_UPG))
                    {
                        TotalModel.Total_OPPS_UPG += Convert.ToDecimal(GP_Data.OPPS_UPG);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_UPG += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_IPBB))
                    {
                        TotalModel.Total_OPPS_IPBB += Convert.ToDecimal(GP_Data.OPPS_IPBB);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_IPBB += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_DTV))
                    {
                        TotalModel.Total_OPPS_DTV += Convert.ToDecimal(GP_Data.OPPS_DTV);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_DTV += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_Prepaid))
                    {
                        TotalModel.Total_OPPS_Prepaid += Convert.ToDecimal(GP_Data.OPPS_Prepaid);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_Prepaid += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_GA))
                    {
                        TotalModel.Total_GP_GA += Convert.ToDecimal(GP_Data.GP_GA);

                    }
                    else
                    {
                        TotalModel.Total_GP_GA += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_UPG))
                    {
                        TotalModel.Total_GP_UPG += Convert.ToDecimal(GP_Data.GP_UPG);

                    }
                    else
                    {
                        TotalModel.Total_GP_UPG += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_IPBB))
                    {
                        TotalModel.Total_GP_IPBB += Convert.ToDecimal(GP_Data.GP_IPBB);

                    }
                    else
                    {
                        TotalModel.Total_GP_IPBB += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_DTV))
                    {
                        TotalModel.Total_GP_DTV += Convert.ToDecimal(GP_Data.GP_DTV);

                    }
                    else
                    {
                        TotalModel.Total_GP_DTV += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_Prepaid))
                    {
                        TotalModel.Total_GP_Prepaid += Convert.ToDecimal(GP_Data.GP_Prepaid);

                    }
                    else
                    {
                        TotalModel.Total_GP_Prepaid += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.NextUp))
                    {
                        TotalModel.Total_NextUp += Convert.ToDecimal(GP_Data.NextUp);

                    }
                    else
                    {
                        TotalModel.Total_NextUp += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Acc))
                    {
                        TotalModel.Total_Acc += Convert.ToDecimal(GP_Data.Acc);

                    }
                    else
                    {
                        TotalModel.Total_Acc += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Acc_GP))
                    {
                        TotalModel.Total_Acc_GP += Convert.ToDecimal(GP_Data.Acc_GP);

                    }
                    else
                    {
                        TotalModel.Total_Acc_GP += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.CRU_GA))
                    {
                        TotalModel.Total_CRU_GA += Convert.ToDecimal(GP_Data.CRU_GA);

                    }
                    else
                    {
                        TotalModel.Total_CRU_GA += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.FirstNet_GA))
                    {
                        TotalModel.Total_FirstNet_GA += Convert.ToDecimal(GP_Data.FirstNet_GA);

                    }
                    else
                    {
                        TotalModel.Total_FirstNet_GA += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Trade_In))
                    {
                        TotalModel.Total_Trade_In += Convert.ToDecimal(GP_Data.Trade_In);

                    }
                    else
                    {
                        TotalModel.Total_Trade_In += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Pstpd_Data_GA_Cnt))
                    {
                        TotalModel.Total_Pstpd_Data_GA_Cnt += Convert.ToDecimal(GP_Data.Pstpd_Data_GA_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_Pstpd_Data_GA_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.UYW_Elite_Cnt))
                    {
                        TotalModel.Total_UYW_Elite_Cnt += Convert.ToDecimal(GP_Data.UYW_Elite_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_UYW_Elite_Cnt += 0;
                    }

                    if (!String.IsNullOrEmpty(GP_Data.IRU_Postpaid_Gross_Adds))
                    {
                        TotalModel.Total_IRU_Postpaid_Gross_Adds += Convert.ToDecimal(GP_Data.IRU_Postpaid_Gross_Adds);

                    }
                    else
                    {
                        TotalModel.Total_IRU_Postpaid_Gross_Adds += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.IPBB_100Mb))
                    {
                        TotalModel.Total_IPBB_100Mb += Convert.ToDecimal(GP_Data.IPBB_100Mb);

                    }
                    else
                    {
                        TotalModel.Total_IPBB_100Mb += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Tot_ProtAdv_Feat_Cnt))
                    {
                        TotalModel.Total_Tot_ProtAdv_Feat_Cnt += Convert.ToDecimal(GP_Data.Tot_ProtAdv_Feat_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_Tot_ProtAdv_Feat_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.HTP_Feat_Add_Cnt))
                    {
                        TotalModel.Total_HTP_Feat_Add_Cnt += Convert.ToDecimal(GP_Data.HTP_Feat_Add_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_HTP_Feat_Add_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.iPhone_12_Series))
                    {
                        TotalModel.Total_iPhone_12_Series += Convert.ToDecimal(GP_Data.iPhone_12_Series);

                    }
                    else
                    {
                        TotalModel.Total_iPhone_12_Series += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Next_Up_Spiff))
                    {
                        TotalModel.Total_Next_Up_Spiff += Convert.ToDecimal(GP_Data.Next_Up_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_Next_Up_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Upg_QI_Spiff))
                    {
                        TotalModel.Total_Upg_QI_Spiff += Convert.ToDecimal(GP_Data.Upg_QI_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_Upg_QI_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Act_QI_Spiff))
                    {
                        TotalModel.Total_Act_QI_Spiff += Convert.ToDecimal(GP_Data.Act_QI_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_Act_QI_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.BB_QI_Spiff))
                    {
                        TotalModel.Total_BB_QI_Spiff += Convert.ToDecimal(GP_Data.BB_QI_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_BB_QI_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Total_Geo_Spiff))
                    {
                        TotalModel.Total_Total_Geo_Spiff += Convert.ToDecimal(GP_Data.Total_Geo_Spiff);

                    }
                    else
                    {
                        TotalModel.Total_Total_Geo_Spiff += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.OPPS_Tab_Wear))
                    {
                        TotalModel.Total_OPPS_Tab_Wear += Convert.ToDecimal(GP_Data.OPPS_Tab_Wear);

                    }
                    else
                    {
                        TotalModel.Total_OPPS_Tab_Wear += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.GP_Tab_Wear))
                    {
                        TotalModel.Total_GP_Tab_Wear += Convert.ToDecimal(GP_Data.GP_Tab_Wear);

                    }
                    else
                    {
                        TotalModel.Total_GP_Tab_Wear += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Premium_TV))
                    {
                        TotalModel.Total_Premium_TV += Convert.ToDecimal(GP_Data.Premium_TV);

                    }
                    else
                    {
                        TotalModel.Total_Premium_TV += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Geo_Spif_Rate))
                    {
                        TotalModel.Total_Geo_Spif_Rate += Convert.ToDecimal(GP_Data.Geo_Spif_Rate);

                    }
                    else
                    {
                        TotalModel.Total_Geo_Spif_Rate += 0;
                    }

                    if (TotalModel.Total_GP_Today_Goals == 0)
                    {

                        TotalModel.Total_GP_Achieved_Per = 0;
                    }
                    else
                    {
                        TotalModel.Total_GP_Achieved_Per = (Convert.ToDecimal(TotalModel.Total_TOTAL_GP) / Convert.ToDecimal(TotalModel.Total_GP_Today_Goals)) * 100;

                    }
                    if (TotalModel.Total_OPS_Today_Goals == 0)
                    {

                        TotalModel.Total_OPS_Achieved_Per = 0;
                    }
                    else
                    {
                        TotalModel.Total_OPS_Achieved_Per = (Convert.ToDecimal(TotalModel.Total_TOTAL_OPPS) / Convert.ToDecimal(TotalModel.Total_OPS_Today_Goals)) * 100;

                    }
                    if (!String.IsNullOrEmpty(GP_Data.GA_Today_Goals))
                    {
                        TotalModel.Total_GA_Today_Goals += Convert.ToDecimal(GP_Data.GA_Today_Goals);

                    }
                    else
                    {
                        TotalModel.Total_GA_Today_Goals += 0;
                    }
                    if (TotalModel.Total_GA_Today_Goals == 0)
                    {

                        TotalModel.Total_GA_Achieved_Per = 0;
                    }
                    else
                    {
                        TotalModel.Total_GA_Achieved_Per = (Convert.ToDecimal(TotalModel.Total_OPPS_GA) / Convert.ToDecimal(TotalModel.Total_GA_Today_Goals)) * 100;

                    }
                    if (!String.IsNullOrEmpty(GP_Data.BYOD_PPV_Cnt))
                    {
                        TotalModel.Total_BYOD_PPV_Cnt += Convert.ToDecimal(GP_Data.BYOD_PPV_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_BYOD_PPV_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Postpaid_DF_Voice_Gross_Adds))
                    {
                        TotalModel.Total_Postpaid_DF_Voice_Gross_Adds += Convert.ToDecimal(GP_Data.Postpaid_DF_Voice_Gross_Adds);

                    }
                    else
                    {
                        TotalModel.Total_Postpaid_DF_Voice_Gross_Adds += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.HTP_Feat_Add_Cnt))
                    {
                        TotalModel.Total_HTP_Feat_Add_Cnt += Convert.ToDecimal(GP_Data.HTP_Feat_Add_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_HTP_Feat_Add_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.IPBB_100MBto300MB_Migr_Cnt))
                    {
                        TotalModel.Total_IPBB_100MBto300MB_Migr_Cnt += Convert.ToDecimal(GP_Data.IPBB_100MBto300MB_Migr_Cnt);

                    }
                    else
                    {
                        TotalModel.Total_IPBB_100MBto300MB_Migr_Cnt += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Acc_Units_per_Opp))
                    {
                        TotalModel.Total_Acc_Units_per_Opp += Convert.ToDecimal(GP_Data.Acc_Units_per_Opp);

                    }
                    else
                    {
                        TotalModel.Total_Acc_Units_per_Opp += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.Samsung_GS23))
                    {
                        TotalModel.Total_Samsung_GS23 += Convert.ToDecimal(GP_Data.Samsung_GS23);

                    }
                    else
                    {
                        TotalModel.Total_Samsung_GS23 += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.AIA))
                    {
                        TotalModel.Total_AIA += Convert.ToDecimal(GP_Data.AIA);

                    }
                    else
                    {
                        TotalModel.Total_AIA += 0;
                    }
                    if (!String.IsNullOrEmpty(GP_Data.AIASpiff))
                    {
                        TotalModel.Total_AIASpiff += Convert.ToDecimal(GP_Data.AIASpiff);

                    }
                    else
                    {
                        TotalModel.Total_AIASpiff += 0;
                    }
                }


                TotalModel_List.Add(TotalModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return TotalModel_List;
        }

        public static SelectList ToSelectList2(DataTable dt, string markets)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Text = "Select Markets", Value = "0" });
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row["Market"].ToString(),
                    Value = row["Market"].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList3(DataTable dt, string markets)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Text = "Select SD", Value = "0" });
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row["UserName"].ToString(),
                    Value = row["UserName"].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static SelectList ToSelectList4(DataTable dt, string markets)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem() { Text = "Select TM", Value = "0" });
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row["UserName"].ToString(),
                    Value = row["UserName"].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        public static List<GpReportAttributesModel> RoadRunner(List<GpReportAttributesModel> GPS)
        {
            DataTable dt = GP_DAL_Functions.GetRoadRunner();
            List<GpReportAttributesModel> Foc = new List<GpReportAttributesModel>();
            foreach (DataRow dr in dt.Rows)
            {
                foreach (var a in GPS)
                {
                    if (dr[0].ToString() == a.Dealer_Code)
                    {
                        Foc.Add(a);
                    }
                }
            }
            return Foc;
        }

        public static double Calc_Division(double nunumerator, double denominator)
        {
            double result = 0;
            if (denominator != 0)
            {
                result = (nunumerator / denominator) * 100;
            }
            return result;
        }

        public static DataSet GetNoGA(string endDate)
        {
            try
            {
                DAL objDal = new DAL();
                objDal.ProcName = "getNoActivity";

                //SPParameters spParam = new SPParameters();
                //spParam.SetParam("@endDate", SqlDbType.VarChar, endDate);

                SPParameters spParam = new SPParameters();
                //return objDal.Getdata(spParam);

                return objDal.GetMultipleTables(spParam); // Assuming GetMultipleTables returns a DataSet
            }
            catch (Exception ex)
            {
                // Handle exceptions if needed
                throw;
            }
        }

        public static List<DTACT> ToSelectListGA(DataTable dt)
        {
            List<DTACT> list = new List<DTACT>();

            foreach (DataRow row in dt.Rows)
            {
                DTACT item = new DTACT();

                item.StoreNo = row["StoreNo"].ToString();
                item.DeaalerCode = row["DeaalerCode"].ToString();
                item.UID = row["UID"].ToString();
                item.Market = row["Market"].ToString();
                item.Store = row["Store"].ToString();
                item.SM = row["SM"].ToString();
                item.ZeroGA = row["ZeroGA"].ToString();


                list.Add(item);
            }

            list = list.OrderBy(x => x.Market).ThenBy(x => x.Store).ToList();

            return list;
        }
        public static List<DTACT> ToSelectListOps(DataTable dt)
        {
            List<DTACT> list = new List<DTACT>();

            foreach (DataRow row in dt.Rows)
            {
                DTACT item = new DTACT();

                item.StoreNo = row["StoreNo"].ToString();
                item.DeaalerCode = row["DeaalerCode"].ToString();
                item.UID = row["UID"].ToString();
                item.Market = row["Market"].ToString();
                item.Store = row["Store"].ToString();
                item.SM = row["SM"].ToString();
                item.ZeroOps = row["ZeroOps"].ToString();


                list.Add(item);
            }

            list = list.OrderBy(x => x.Market).ThenBy(x => x.Store).ToList();

            return list;
        }
        public static List<DTACT> ToSelectListActivity(DataTable dt)
        {
            List<DTACT> list = new List<DTACT>();

            foreach (DataRow row in dt.Rows)
            {
                DTACT item = new DTACT();

                item.StoreNo = row["StoreNo"].ToString();
                item.DeaalerCode = row["DeaalerCode"].ToString();
                item.UID = row["UID"].ToString();
                item.Market = row["Market"].ToString();
                item.Store = row["Store"].ToString();
                item.SM = row["SM"].ToString();
                item.NoActivity = row["NoActivity"].ToString();


                list.Add(item);
            }

            list = list.OrderBy(x => x.Market).ThenBy(x => x.Store).ToList();

            return list;
        }
    }


    public class DTACT
    {
        public string StoreNo { get; set; }
        public string DeaalerCode { get; set; }
        public string UID { get; set; }
        public string Market { get; set; }
        public string Store { get; set; }
        public string SM { get; set; }
        public string ZeroOps { get; set; }
        public string NoActivity { get; set; }
        public string ZeroGA { get; set; }
    }


    public class GpReportAttributesModel
    {
        public string Opus { get; set; }
        public string Dealer_Code { get; set; }
        public string UID { get; set; }
        public string VP { get; set; }
        public string SSM { get; set; }
        public string SD { get; set; }
        public string TM { get; set; }
        public string MUSM { get; set; }
        public string Market { get; set; }
        public string Store { get; set; }
        public string GP_Today_Goals { get; set; }
        public string OPS_Today_Goals { get; set; }
        public string GA_Today_Goals { get; set; }
        public string TOTAL_GP { get; set; }
        public string Total_Spiff { get; set; }
        public string TOTAL_OPPS { get; set; }
        public string OPPS_GA { get; set; }
        public string OPPS_UPG { get; set; }
        public string OPPS_IPBB { get; set; }
        public string OPPS_DTV { get; set; }
        public string OPPS_Tab_Wear { get; set; }
        public string OPPS_Prepaid { get; set; }
        public string GP_GA { get; set; }
        public string GP_UPG { get; set; }
        public string GP_IPBB { get; set; }
        public string GP_DTV { get; set; }
        public string GP_Tab_Wear { get; set; }
        public string GP_Prepaid { get; set; }
        public string NextUp { get; set; }
        public string Acc { get; set; }
        public string Acc_GP { get; set; }
        public string CRU_GA { get; set; }
        public string FirstNet_GA { get; set; }
        public string Trade_In { get; set; }
        public string Pstpd_Data_GA_Cnt { get; set; }
        public string UYW_Elite_Cnt { get; set; }
        public string IRU_Postpaid_Gross_Adds { get; set; }
        public string IPBB_100Mb { get; set; }
        public string Tot_ProtAdv_Feat_Cnt { get; set; }
        public string Galaxy_S21_Series { get; set; }
        public string iPhone_12_Series { get; set; }
        public string Next_Up_Spiff { get; set; }
        public string Upg_QI_Spiff { get; set; }
        public string Act_QI_Spiff { get; set; }
        public string BB_QI_Spiff { get; set; }
        public string Total_Geo_Spiff { get; set; }
        public string Premium_TV { get; set; }
        public string Geo_Spif_Rate { get; set; }
        public string GP_Achieved_Per { get; set; }
        public string OPS_Achieved_Per { get; set; }
        public string GA_Achieved_Per { get; set; }
        public string BYOD_PPV_Cnt { get; set; }
        public string Postpaid_DF_Voice_Gross_Adds { get; set; }
        public string HTP_Feat_Add_Cnt { get; set; }
        public string IPBB_100MBto300MB_Migr_Cnt { get; set; }
        public string Acc_Units_per_Opp { get; set; }
        public string Samsung_GS23 { get; set; }
        public string AIA { get; set; }
        public string AIASpiff { get; set; }
    }
    public class GpReportAttributesModel_Totals
    {
        //public string TM { get; set; }
        //public string SD { get; set; }
        public string Market { get; set; }
        public string VP { get; set; }
        public string SSM { get; set; }
        public string SD { get; set; }
        public string TM { get; set; }
        public string MUSM { get; set; }
        public decimal Total_GP_Today_Goals { get; set; }
        public decimal Total_OPS_Today_Goals { get; set; }
        public decimal Total_GA_Today_Goals { get; set; }
        public decimal Total_TOTAL_GP { get; set; }
        public decimal Total_Total_Spiff { get; set; }
        public decimal Total_TOTAL_OPPS { get; set; }
        public decimal Total_OPPS_GA { get; set; }
        public decimal Total_OPPS_UPG { get; set; }
        public decimal Total_OPPS_IPBB { get; set; }
        public decimal Total_OPPS_DTV { get; set; }
        public decimal Total_OPPS_Tab_Wear { get; set; }
        public decimal Total_OPPS_Prepaid { get; set; }
        public decimal Total_GP_GA { get; set; }
        public decimal Total_GP_UPG { get; set; }
        public decimal Total_GP_IPBB { get; set; }
        public decimal Total_GP_DTV { get; set; }
        public decimal Total_GP_Tab_Wear { get; set; }
        public decimal Total_GP_Prepaid { get; set; }
        public decimal Total_NextUp { get; set; }
        public decimal Total_Acc { get; set; }
        public decimal Total_Acc_GP { get; set; }
        public decimal Total_CRU_GA { get; set; }
        public decimal Total_FirstNet_GA { get; set; }
        public decimal Total_Trade_In { get; set; }
        public decimal Total_Pstpd_Data_GA_Cnt { get; set; }
        public decimal Total_UYW_Elite_Cnt { get; set; }
        public decimal Total_IRU_Postpaid_Gross_Adds { get; set; }
        public decimal Total_IPBB_100Mb { get; set; }
        public decimal Total_Tot_ProtAdv_Feat_Cnt { get; set; }
        public decimal Total_Galaxy_S21_Series { get; set; }
        public decimal Total_iPhone_12_Series { get; set; }
        public decimal Total_Next_Up_Spiff { get; set; }
        public decimal Total_Upg_QI_Spiff { get; set; }
        public decimal Total_Act_QI_Spiff { get; set; }
        public decimal Total_BB_QI_Spiff { get; set; }
        public decimal Total_Total_Geo_Spiff { get; set; }
        public decimal Total_Premium_TV { get; set; }
        public decimal Total_Geo_Spif_Rate { get; set; }
        public decimal Total_GP_Achieved_Per { get; set; }
        public decimal Total_OPS_Achieved_Per { get; set; }
        public decimal Total_GA_Achieved_Per { get; set; }
        public decimal Total_BYOD_PPV_Cnt { get; set; }
        public decimal Total_Postpaid_DF_Voice_Gross_Adds { get; set; }
        public decimal Total_HTP_Feat_Add_Cnt { get; set; }
        public decimal Total_IPBB_100MBto300MB_Migr_Cnt { get; set; }
        public decimal Total_Acc_Units_per_Opp { get; set; }
        public decimal Total_Samsung_GS23 { get; set; }
        public decimal Total_AIA { get; set; }
        public decimal Total_AIASpiff { get; set; }
    }
    public class GpReportDataModel
    {
        public List<GpReportAttributesModel> GP_Rows { get; set; }
        public List<GpReportAttributesModel_Totals> Total { get; set; }
    }
    public class GpReportDataModelSD
    {
        public List<GpReportDataModel> GP_Rows_Tm { get; set; }
        public List<GpReportDataModelTM> GP_Rows_TM { get; set; }
        public List<GpReportAttributesModel_Totals> SD_Total { get; set; }
    }

    public class GpReportDataModelVP
    {
        public List<GpReportDataModelSD> GP_Rows_SD { get; set; }
        public List<GpReportAttributesModel_Totals> VP_Total { get; set; }
    }

    // Edited by Ammad dtd: 11/03/2022 due to addition of Addition of Role in Mobily GP Report i.e. MU-SM

    public class GpReportDataModelTM
    {
        public List<GpReportDataModel> GP_Rows_MUSM { get; set; }
        public List<GpReportAttributesModel_Totals> TM_Total { get; set; }
    }

    public class Fct_StoreNumberAttributesModel
    {
        public string VP { get; set; }
        public string Region { get; set; }
        public string SD { get; set; }
        public string UniqueID { get; set; }
        public string DealerCode { get; set; }
        public string Tiers { get; set; }
        public string Store { get; set; }
        public string HITStore { get; set; }
        public string Market { get; set; }
        public string MULMkt { get; set; }
        public string MULMngr { get; set; }
        public string TM { get; set; }
        public string RSMSRSM { get; set; }
        public string Role { get; set; }
        public string MonthlyAchievedHoursTrendingPercentage { get; set; }
        public string GrossAddsTrendToGoal { get; set; }
        public string GrossAddsGoals { get; set; }
        public string GrossAddsNetOFF { get; set; }
        public string Converged { get; set; }
        public string PPVGAPerTrafficPercentage { get; set; }
        public string TotalTraffic { get; set; }
        public string FiberConversion { get; set; }
        public string BroadbandFiberNetOFF { get; set; }
        public string FiberGreenCheck { get; set; }
        public string APO { get; set; }
        public string CSAT { get; set; }
        public string ProtAdvHomeTechPercentage { get; set; }
        public string BreakEvenNumbers { get; set; }
        public string TrendingToBreakEven { get; set; }
        public string GPWithSpifTrending { get; set; }
        public string GPTrendingPercentage { get; set; }
        public string TotalGPGoals { get; set; }
        public string OPSTrendingToGoalsPercentage { get; set; }
        public string TotalOPSGoals { get; set; }
        public string TotalOPS { get; set; }
        public string UpgradeTrendingToGoalsPercentage { get; set; }
        public string MTDUpgradesGoals { get; set; }
        public string MTDUpgradesNetOFF { get; set; }
        public string TWCDevicesTrendToGoalPercentage { get; set; }
        public string TWCDevicesQtyGoals { get; set; }
        public string TWCDevicesQtyNetOFF { get; set; }
        public string AIABusinessConversion { get; set; }
        public string AIAConsumerConversion { get; set; }
        public string AIAGreenCheckConsumer { get; set; }
        public string AIACInternet { get; set; }
        public string AIABInternet { get; set; }
        public string TotalOPSTrending { get; set; }
        public string OPSPerTrafficPercentage { get; set; }
        public string CRUAchMTD { get; set; }
        public string FNAchMTD { get; set; }
        public string GPPerBox { get; set; }
        public string TotalGPAchievedWithSpif { get; set; }
        public string GrossAdds { get; set; }
        public string ChargeBack { get; set; }
        public string GrossAddsTrend { get; set; }
        public string GrossAddsGP { get; set; }
        public string GrossAddsGPTrending { get; set; }
        public string ARBusinessFNProgramAverageQ2Target { get; set; }
        public string FNAchAverageQTD { get; set; }
        public string RemainingToAchieveFNAverageQ2Target { get; set; }
        public string FNAchTrend { get; set; }
        public string FNAchDollars { get; set; }
        public string ARBusinessCRUProgramAverageQ2Target { get; set; }
        public string CRUAchVoice { get; set; }
        public string CRUAchDATA { get; set; }
        public string CRUAchAverageQTD { get; set; }
        public string RemainingToAchieveCRUAverageQ2Target { get; set; }
        public string CRUAchTrend { get; set; }
        public string CRUAchVoiceDollars { get; set; }
        public string CRUAchDATADollars { get; set; }
        public string MTDUpgrades { get; set; }
        public string ChargeBack1 { get; set; }
        public string MTDUpgradesTrending { get; set; }
        public string MTDUpgradesGP { get; set; }
        public string MTDUpgradesGPTrending { get; set; }
        public string NextUp { get; set; }
        public string NextUpSpif { get; set; }
        public string PremiumActivation { get; set; }
        public string PremiumActivationNetOFF { get; set; }
        public string ChargeBack2 { get; set; }
        public string PremiumActivationGP { get; set; }
        public string ExtraActivation { get; set; }
        public string ExtraActivationNetOFF { get; set; }
        public string ChargeBack3 { get; set; }
        public string ExtraActivationGP { get; set; }
        public string NonExtraNonPremiumActivation { get; set; }
        public string NonExtraNonPremiumActivationNetOFF { get; set; }
        public string NonExtraNonPremiumActivationGP { get; set; }
        public string PremiumUpgrade { get; set; }
        public string ChargeBack4 { get; set; }
        public string PremiumUpgradeNetOff { get; set; }
        public string PremiumUpgradeGP { get; set; }
        public string ExtraUpgrade { get; set; }
        public string ChargeBack5 { get; set; }
        public string ExtraUpgradeNetOff { get; set; }
        public string ExtraUpgradeGP { get; set; }
        public string AIAGoals { get; set; }
        public string AIAInternet { get; set; }
        public string ChargeBack6 { get; set; }
        public string AIAInternetNetOff { get; set; }
        public string AIATrending { get; set; }
        public string AIAInternetGP { get; set; }
        public string BroadbandGoals { get; set; }
        public string BroadbandLessThen300MB { get; set; }
        public string NewFiber300MB { get; set; }
        public string NewFiber500MB { get; set; }
        public string NewFiber1G { get; set; }
        public string TotalBroadbandNewFiber { get; set; }
        public string BroadbandNewFiberNetOFF { get; set; }
        public string ChargeBack7 { get; set; }
        public string FiberUpgrades { get; set; }
        public string FiberUpgradesNetOFF { get; set; }
        public string ChargeBack8 { get; set; }
        public string BroadbandFiber { get; set; }
        public string BroadbandFiberTrend { get; set; }
        public string BroadbandFiberTrendPercentage { get; set; }
        public string BroadbandLessThen300MBQISpiff { get; set; }
        public string NewFiber300MBQISpiff { get; set; }
        public string NewFiber500MBQISpiff { get; set; }
        public string NewFiber1GQISpiff { get; set; }
        public string FiberUpgradeGP { get; set; }
        public string BroadbandGP { get; set; }
        public string BroadbandFiberGP { get; set; }
        public string BroadbandFiberGPTrending { get; set; }
        public string TurboFeature { get; set; }
        public string TurboFeatureGP { get; set; }
        public string PremVideoGoals { get; set; }
        public string PremVideo { get; set; }
        public string PremVideoNetOFF { get; set; }
        public string ChargeBack9 { get; set; }
        public string PremVideoTrend { get; set; }
        public string PremVideoTrendPercentage { get; set; }
        public string PremVideoGP { get; set; }
        public string PremVideoSpiff { get; set; }
        public string PremVideoGPTrending { get; set; }
        public string EntertainmentGoals { get; set; }
        public string EntertainmentAch { get; set; }
        public string EntertainmentTrending { get; set; }
        public string EntertainmentTrendingToGoalsPercentage { get; set; }
        public string TWCDevicesQty { get; set; }
        public string ChargeBack10 { get; set; }
        public string TWCDevicesQtyTrend { get; set; }
        public string TWCDevicesDollars { get; set; }
        public string ProjectedGeographicSpif { get; set; }
        public string PrepaidQty { get; set; }
        public string PrepaidToGA { get; set; }
        public string PrepaidNetOFF { get; set; }
        public string ChargeBack11 { get; set; }
        public string PrepaidGP { get; set; }
        public string PrepaidWithAutopay { get; set; }
        public string AccessGP { get; set; }
        public string AccessQty { get; set; }
        public string AccessQtyTrending { get; set; }
        public string AccessRevenue { get; set; }
        public string FeaturesQty { get; set; }
        public string FeaturesQtyNetOFF { get; set; }
        public string ChargeBack12 { get; set; }
        public string TotalProtectionPercentage { get; set; }
        public string ProtAdv1 { get; set; }
        public string ProtAdv4 { get; set; }
        public string FeaturesGP { get; set; }
        public string WeeklyBudgetedHRS { get; set; }
        public string WeeklyEmployeeAveragePerStore { get; set; }
        public string MonthlyBudgetedHRS { get; set; }
        public string TrainingHours { get; set; }
        public string MonthlyAchievedHRS { get; set; }
        public string MonthlyAchievedHoursTrending { get; set; }
        public string GACloseRt { get; set; }
        public string HomeTechProtect { get; set; }
        public string TimeStamp { get; set; }
        public string DateKey { get; set; }
        public string PAY { get; set; }
        public string CommissionBucketMTD { get; set; }
        public string EOMCommissionTrendingBucket { get; set; }
        public string EffectiveRate { get; set; }

        //New Dot Report
        public string NextUPTrendPer                             { get; set; }
        public string PremiumPerGA                              { get; set; }
        public string ExtraPerGA                               { get; set; }
        public string ExtraUnlMix70Per                         { get; set; }
        public string FiberUpgTrend                             { get; set; }
        public string TotalProt                                 { get; set; }
        public string TotalProtNetOFF                            { get; set; }
        public string ProtAdv1NetOff                            { get; set; }
        public string ProtAdv4NetOff                            { get; set; }
        public string PrepQTYTrend                              { get; set; }
        public string AccessRevTrend                            { get; set; }
        public string WeeklyAchivedHRS                          { get; set; }
        public string BudgEmp                                 { get; set; }
        public string FullTimeHourlyHeadCount                 { get; set; }
        public string PartTimeHourlyHeadCount                 { get; set; }
        public string TotalHourlyCount                         { get; set; }
        public string HourlyHeadCntsVar                 { get; set; }
        public string HourlyCurrHeadCntVarHrs       { get; set; }


        //Employee
        public string EmpID { get; set; }
        public string Employees { get; set; }
        public string Status { get; set; }
        public string HireDate { get; set; }
        public string PayType { get; set; }
        public string GPGOAL { get; set; }
        public string Trending { get; set; }
        public string TrendingPer { get; set; }
        public string AIAInternetCancellation { get; set; }
        public string TotalNewFiber { get; set; }
        public string BroadbandFiberUpgrade { get; set; }
        public string BroadbandFiberUpgradeNetOFF { get; set; }
        public string BroadbandNewFiberGP { get; set; }
        public string INSURANCE { get; set; }
        public string INSURANCEGP { get; set; }
        public string PROTECTION_PACK { get; set; }
        public string PROTECTION_PACK_NetOFF { get; set; }
        public string PROTECTION_PACK_GP { get; set; }
        
    }
    
    public class Fct_EmployeeNumberAttributesModel
    {
        //Hierarchy Attributes
        public string VP { get; set; }
        public string MUL_MktMngr { get; set; }
        public string MUL_Market { get; set; }
        public string Region { get; set; }
        public string SD { get; set; }
        public string UniqueID { get; set; }
        public string DealerCode { get; set; }
        public string Tiers { get; set; }
        public string Store { get; set; }
        public string HITStore { get; set; }
        public string Market { get; set; }
        public string TM { get; set; }
        public string RSMSRSM { get; set; }
        public string Role { get; set; }

        //Calculated Attributes
        public string TOTALOPS                                      { get; set; }
        public string GPperBOX                                      { get; set; }
        public string GPGOAL                                        { get; set; }
        public string TotalGPAchievedWithSpif                       { get; set; }
        public string Trending                                      { get; set; }
        public string CSAT                                          { get; set; }
        public string GROSSADDS                                     { get; set; }
        public string GROSSADDSNetOFF                               { get; set; }
        public string ChargeBackACTIVATION                          { get; set; }
        public string GROSSADDSGP                                  { get; set; }
        public string FNAchMTD                                      { get; set; }
        public string FNAch                                        { get; set; }
        public string CRUAchMTD                                     { get; set; }
        public string CRUAch                                       { get; set; }
        public string NextUP                                         { get; set; }
        public string NextUPSpif                                    { get; set; }
        public string PremiumActivation                              { get; set; }
        public string PremiumActivationNetOFF                      { get; set; }
        public string ChargeBackPREMIUMACT                         { get; set; }
        public string PremiumActivationGP                         { get; set; }
        public string ExtraActivation                                { get; set; }
        public string ExtraActivationNetOFF                        { get; set; }
        public string ChargeBackEXTRAACT                           { get; set; }
        public string ExtraActivationGP                            { get; set; }
        public string NonExtraNonPremiumActivation              { get; set; }
        public string NonExtraNonPremiumActivationNetOFF      { get; set; }
        public string NonExtraNonPremiumActivationGP          { get; set; }
        public string MTDUPGRADES                                    { get; set; }
        public string MTDUPGRADESNetOFF                            { get; set; }
        public string ChargeBackUPGRADE                             { get; set; }
        public string MTDUPGRADESGP                                { get; set; }
        public string PremiumUpgrade                                 { get; set; }
        public string ChargeBackPREMIUMUPGRADE                     { get; set; }
        public string PremiumUpgradeNetOff                         { get; set; }
        public string PremiumUpgradeGP                             { get; set; }
        public string ExtraUpgrade                                   { get; set; }
        public string ChargeBackEXTRAUPGRADE                       { get; set; }
        public string ExtraUpgradeNetOff                           { get; set; }
        public string ExtraUpgradeGP                               { get; set; }
        public string AIAInternet                                    { get; set; }
        public string AIAInternetCancellation                       { get; set; }
        public string AIAInternetNetOff                            { get; set; }
        public string BroadBandlessThen300MB                     { get; set; }
        public string NewFiber300MB                                { get; set; }
        public string NewFiber500MB                                { get; set; }
        public string NewFiber1G                                   { get; set; }
        public string TotalNewFiber                                { get; set; }
        public string TotalBroadbandNewfiber                      { get; set; }
        public string BroadbandNewfiberNetOFF                   { get; set; }
        public string ChargeBackBB                                  { get; set; }
        public string FiberUpgrades                                  { get; set; }
        public string FiberUpgradesNetOFF                          { get; set; }
        public string ChargeBackFIBREUPG                          { get; set; }
        public string BroadBandFiberUpgrade                      { get; set; }
        public string BroadBandFiberUpgradeNetOFF              { get; set; }
        public string BroadbandLessthen300MbQISpiff             { get; set; }
        public string Newfiber300MBQISpiff                      { get; set; }
        public string Newfiber500MBQISpiff                      { get; set; }
        public string Newfiber1GQISpiff                          { get; set; }
        public string FiberUpgradesGP                               { get; set; }
        public string BroadbandNewfiberGP                       { get; set; }
        public string BroadbandFiberGP                           { get; set; }
        public string TurboFeature                                   { get; set; }
        public string TurboFeatureGP                               { get; set; }
        public string PremVideo                                      { get; set; }
        public string PremVideoNetOFF                              { get; set; }
        public string ChargeBackTV                                  { get; set; }
        public string PremVideoGP                                  { get; set; }
        public string PremVideoSpiff                                { get; set; }
        public string TWCDevicesQTY                               { get; set; }
        public string TWCDevicesNetOFF                              { get; set; }
        public string ChargeBackTWC                                 { get; set; }
        public string TWCDevicesGP                                  { get; set; }
        public string ProjectedGeographicSpif                       { get; set; }
        public string PrepaidGP                                     { get; set; }
        public string PrepaidQTY                                     { get; set; }
        public string PrepaidtoGA                                  { get; set; }
        public string PrepaidNetOFF                                 { get; set; }
        public string ChargeBackPREPAIDACTIVATION                  { get; set; }
        public string PrepaidwithAutopay                            { get; set; }
        public string AccessGP                                      { get; set; }
        public string AccessQty                                      { get; set; }
        public string AccessRevenue                                { get; set; }
        public string INSURANCE                                       { get; set; }
        public string ChargeBackINSURANCE                           { get; set; }
        public string INSURANCEGP                                   { get; set; }
        public string ProtAdv1                                       { get; set; }
        public string ProtAdv4                                       { get; set; }
        public string PROTECTIONPACK                                 { get; set; }
        public string PROTECTIONPACKNetOFF                         { get; set; }
        public string ChargeBackPROTECTION                          { get; set; }
        public string PROTECTIONPACKGP                             { get; set; }
        public string HomeTechProtect                                { get; set; }
        public string ProtAdvHomeTech                            { get; set; }
        public string DREAMWk1to7                                { get; set; }
        public string DREAMWk8to14                              { get; set; }
        public string DREAMWk15to21                              { get; set; }
        public string DREAMWk22to28                              { get; set; }
        public string DREAMWk29to31                              { get; set; }
        public string PPVGACommission                                { get; set; }
        public string PPVGAEliteCommission                          { get; set; }
        public string PPVGAExtraCommission                          { get; set; }
        public string FNCommission                                   { get; set; }
        public string CRUCommission                                  { get; set; }
        public string PremiumVideoCommission                        { get; set; }
        public string AIAInternetCommission                         { get; set; }
        public string BroadbandCommission                            { get; set; }
        public string NewFiberCommission                            { get; set; }
        public string FiberUpgradeCommission                        { get; set; }
        public string UPGCommission                                  { get; set; }
        public string UpgradeEliteCommission                        { get; set; }
        public string UpgradeExtraCommission                        { get; set; }
        public string WearbaleTabletCommission                    { get; set; }
        public string Hometech                                        { get; set; }
        public string NextUp                                          { get; set; }
        public string ProtectAdvantage1Commission                  { get; set; }
        public string ProtectAdvantage4Commission                  { get; set; }
        public string PrepaidCommission                              { get; set; }
        public string AccessoryGPCommission                         { get; set; }
        public string Commission                                      { get; set; }
        public string CommissionEligibility                         { get; set; }
        public string RegHrsAchved                                  { get; set; }
        public string OT                                              { get; set; }
        public string HourlyRateUSD                                { get; set; }
        public string Salary                                          { get; set; }
        public string GeoSPIFFOPS                                   { get; set; }
        public string GeoRate                                        { get; set; }
        public string MinWage                                        { get; set; }
        public string MinWage150                                { get; set; }
        public string TotalCompNew                                  { get; set; }
        public string TotalCompAfter150Check                    { get; set; }
        public string CommissionBucketMTD                           { get; set; }
        public string EOMCommissionTrendingBucket                   { get; set; }
    }


    public class GetTrendingCommission
    {
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string Role { get; set; }
        public string Store { get; set; }
        public string StoreUID { get; set; }
        public string EmployeeRank { get; set; }
        public string TrendingCommissionRank { get; set; }
        public string EOMCommissionTrendingBucket { get; set; }
        public string BucketWage { get; set; }
        public string TOTALOPS { get; set; }
        public string CSAT { get; set; }
        public string GROSSADDSNetOFF { get; set; }
        public string FNAchMTD { get; set; }
        public string CRUAchMTD { get; set; }
        public string NextUP { get; set; }
        public string MTDUPGRADES { get; set; }
        public string AIAInternet { get; set; }
        public string FiberUpgrades { get; set; }
        public string BroadbandNewfiberNetOFF { get; set; }
        public string PremVideoNetOFF { get; set; }
        public string TabWearConnectedDevicesNetOFF { get; set; }
        public string PrepaidNetOFF { get; set; }
        public string AccessGP { get; set; }
        public string AccessQty { get; set; }
        public string ProtAdv1 { get; set; }
        public string ProtAdv4 { get; set; }
        public string ReportDate { get; set; }
        public string MaxDate { get; set; }


    }

    public class Fct_StoreSummaryAttributesModel
    {
        public string SD { get; set; }
        public string TM { get; set; }
        public string Market { get; set; }
        public string Store { get; set; }
        public string Tier { get; set; }
        public string StoreContact { get; set; }
        public string ReportDate { get; set; }
        public string DealerCode { get; set; }
        public string GrossAddsGoals { get; set; }
        public string GrossAddsTrendToGoal { get; set; }
        public string GrossAdds { get; set; }
        public string GrossAddsNetOff { get; set; }
        public string TotalTraffic { get; set; }
        public string PPVGAPerTraffic { get; set; }
        public string FiberConversion { get; set; }
        public string BroadbandFiberNetOff { get; set; }
        public string FiberGreenCheck { get; set; }
        public string APO { get; set; }
        public string CSAT { get; set; }
        public string ProtAdvHomeTechPercentage { get; set; }
        public string BreakEvenNumbers { get; set; }
        public string GPWithSpifTrending { get; set; }
        public string GPTrendingPercentage { get; set; }
        public string GPTrending { get; set; }
        public string TotalGPGoals { get; set; }
        public string OPSTrendingToGoalsPercentage { get; set; }
        public string TotalOPSGoals { get; set; }
        public string TotalOPS { get; set; }
        public string UpgradeTrendingToGoalsPercentage { get; set; }
        public string MTDUpgradesGoals { get; set; }
        public string MTDUpgradesNetOff { get; set; }
        public string TWDevicesTrendToGoalPercentage { get; set; }
        public string TWDevicesQtyGoals { get; set; }
        public string TWDevicesQtyNetOff { get; set; }
        public string AIABusinessConversion { get; set; }
        public string AIAConsumerConversion { get; set; }
        public string AIAGreenCheckConsumer { get; set; }
        public string AIABInternet { get; set; }
        public string AIACInternet { get; set; }
        public string OPSPerTrafficPercentage { get; set; }
        public string CRUAchMTD { get; set; }
        public string FNAchMTD { get; set; }
        public string GrossAddsTrend { get; set; }
        public string BroadbandGoals { get; set; }
        public string BroadbandFiber { get; set; }
        public string BroadBandLessThan300MB { get; set; }
        public string NewFiber300MB { get; set; }
        public string NewFiber500MB { get; set; }
        public string NewFiber1G { get; set; }
        public string FiberUpgradesNetOff { get; set; }
        public string BroadbandFiberTrend { get; set; }
        public string BroadbandFiberTrendPercentage { get; set; }
        public string PremVideoGoals { get; set; }
        public string PremVideo { get; set; }
        public string PremVideoNetOff { get; set; }
        public string PremVideoTrendPercentage { get; set; }
        public string PremVideoTrend { get; set; }
        public string TotalGPAchievedWithSpif { get; set; }
        public string GPPerBox { get; set; }
        public string TotalOPSTrending { get; set; }
        public string GrossAddsGP { get; set; }
        public string MTDUpgrades { get; set; }
        public string MTDUpgradesGP { get; set; }
        public string BroadBandFiberGP { get; set; }
        public string BroadbandGP { get; set; }
        public string BroadbandLessThan300MBQISpiff { get; set; }
        public string NewFiber300MBQISpiff { get; set; }
        public string NewFiber500MBQISpiff { get; set; }
        public string NewFiber1GQISpiff { get; set; }
        public string PremVideoGP { get; set; }
        public string PremVideoSpiff { get; set; }
        public string AccessGP { get; set; }
        public string AccessRevenue { get; set; }
        public string AccessQty { get; set; }
        public string FNAchAverageQTD { get; set; }
        public string FNAch { get; set; }
        public string CRUVGACnt { get; set; }
        public string CRUAchAverageQTD { get; set; }
        public string CRUAchVoice { get; set; }
        public string CRUAchVoiceDollar { get; set; }
        public string CRUAchData { get; set; }
        public string TWDevicesQtyTrend { get; set; }
        public string TWDevicesGP { get; set; }
        public string HomeTechProtect { get; set; }
        public string TotalProtectionPercentage { get; set; }
        public string ProtAdv1 { get; set; }
        public string TimeStamp { get; set; }
        public string ProtAdv4 { get; set; }
        public string Monthly_AchivedHoursTrending { get; set; }
        public string AIAGreenCheck { get; set; }

        public string WeeklyBudgetedHRS { get; set; }
    }

    public class NoteModel
    {
        public string UniqueID { get; set; }
        public string DealerCode { get; set; }
        public string DateKey { get; set; }
        public string Comment { get; set; }
        public string Note { get; set; }
        public string Employee { get; set; }
        
    }

    public class StoreSummaryAttributes
    {
        public string MonthlyAchievedHoursTrending { get; set; }
        public string WeeklyBudgetedHRS { get; set; }
        public string GrossAddsTrendToGoal { get; set; }
        public string GrossAddsGoals { get; set; }
        public string GrossAdds { get; set; }
        public string GrossAddsNetOff { get; set; }
        public string TotalTraffic { get; set; }
        public string PPVGAPerTraffic { get; set; }
        public string FiberConversion { get; set; }
        public string BroadbandFiberNetOff { get; set; }
        public string FiberGreenCheck { get; set; }
        public string APO { get; set; }
        public string CSAT { get; set; }
        public string ProtAdvHomeTech { get; set; }
        public string BreakEvenNumbers { get; set; }
        public string GPWithSpifTrending { get; set; }
        public string TotalGPGoals { get; set; }
        public string GPTrending { get; set; }
        public string OPSTrendingToGoals { get; set; }
        public string TotalOPSGoals { get; set; }
        public string TotalOPS { get; set; }
        public string UpgradeTrendingToGoals { get; set; }
        public string MTDUpgradesGoals { get; set; }
        public string MTDUpgradesNetOff { get; set; }
        public string TWDevicesTrendToGoal { get; set; }
        public string TWDevicesQtyGoals { get; set; }
        public string TWDevicesQtyNetOff { get; set; }
        public string AIABusinessConversion { get; set; }
        public string AIAConsumerConversion { get; set; }
        public string AIAGreenCheck { get; set; }
        public string AIABInternet { get; set; }
        public string AIACInternet { get; set; }
        public string OPSPerTraffic { get; set; }
        public string CRUAchMTD { get; set; }
        public string FNAchMTD { get; set; }
        public string GrossAddsTrend { get; set; }
        public string BroadbandGoals { get; set; }
        public string BroadbandFiber { get; set; }
        public string BroadbandLessThan300MB { get; set; }
        public string NewFiber300MB { get; set; }
        public string NewFiber500MB { get; set; }
        public string NewFiber1G { get; set; }
        public string FiberUpgradesNetOff { get; set; }
        public string BroadbandFiberTrend { get; set; }
        public string BroadbandFiberTrendPercentage { get; set; }
        public string PremVideoGoals { get; set; }
        public string PremVideo { get; set; }
        public string PremVideoNetOff { get; set; }
        public string PremVideoTrendPercentage { get; set; }
        public string PremVideoTrend { get; set; }
        public string GPAchievedWithSpif { get; set; }
        public string GPPerBox { get; set; }
        public string TotalOPSTrending { get; set; }
        public string GrossAddsGP { get; set; }
        public string MTDUpgrades { get; set; }
        public string MTDUpgradesGP { get; set; }
        public string BroadbandFiberGP { get; set; }
        public string BroadbandGP { get; set; }
        public string BroadbandLessThan300MBQISpiff { get; set; }
        public string NewFiber300MBQISpiff { get; set; }
        public string NewFiber500MBQISpiff { get; set; }
        public string NewFiber1GQISpiff { get; set; }
        public string PremVideoGP { get; set; }
        public string PremVideoSpiff { get; set; }
        public string AccessGP { get; set; }
        public string AccessRevenue { get; set; }
        public string AccessQty { get; set; }
        public string FNAchAverageQTD { get; set; }
        public string FNAch { get; set; }
        public string CRUAchVoice { get; set; }
        public string CRUAchAverageQTD { get; set; }
        public string CRUAchVoiceDollar { get; set; }
        public string CRUAchData { get; set; }
        public string TWDevicesQtyTrend { get; set; }
        public string TWDevicesGP { get; set; }
        public string HomeTechProtect { get; set; }
        public string TotalProtectionPercentage { get; set; }
        public string ProtAdv1 { get; set; }
        public string ProtAdv4 { get; set; }
    }

}