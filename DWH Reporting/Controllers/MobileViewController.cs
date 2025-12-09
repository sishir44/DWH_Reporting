using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using DWH_Reporting.Models.GpReport;
using DWH_Reporting.Models;
using System.Linq;
using DWH_Reporting.Helpers;

namespace DWH_Reporting.Controllers
{
    public class MobileViewController : Controller
    {
        // GET: MobileView
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StoreNumberMobileView()
        {
            try
            {
                //string userid = "2";
                //ShowStoreNumber(null, userid);
                //global.userID = Request.QueryString["userid"];
                //global.decrpedUserId = EncryptionHelper.Decrypt(global.userID);
                global.userID = Request.QueryString["userid"];
                global.decrpedUserId = EncryptionHelper.Decrypt(global.userID);
                ShowStoreNumber(null, global.decrpedUserId, "0");
                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message + "\n\n" + ex.StackTrace + "\nReport is being uploaded. Please try again in few minutes");
            }

        }

        [HttpPost]
        public ActionResult StoreNumberMobileView(string selectedDate, string isfinal)
        {
            try
            {
                if (selectedDate == "")
                {
                    selectedDate = null;
                }
                ViewBag.isfinal = isfinal;

                if (isfinal == "1")
                {
                    DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    if (selectedDate == "" || selectedDate == null)
                    {
                        selectedDate = DateTime.Now.ToString("yyyy-MM-dd");
                    }
                    if (DateTime.TryParse(selectedDate, out DateTime dateRangeParsed))
                    {
                        if (firstDayOfMonth <= dateRangeParsed)
                        {
                            DateTime lastDayPrevMonth = firstDayOfMonth.AddDays(-1);
                            selectedDate = lastDayPrevMonth.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            DateTime lastDayOfMonth = new DateTime(dateRangeParsed.Year, dateRangeParsed.Month, 1).AddMonths(1).AddDays(-1);
                            selectedDate = lastDayOfMonth.ToString("yyyy-MM-dd");
                        }
                    }
                }
                ViewBag.selectedDate = selectedDate;
                ShowStoreNumber(selectedDate, global.decrpedUserId, isfinal);

                if (isfinal == "1")
                {
                    if (Request.IsAjaxRequest()) // If AJAX, return the fragment
                    {
                        return PartialView("_StoreNumberM");
                    }
                }

                return View();
            }
            catch (Exception ex)
            {
                return Content(ex.Message + "\n\n" + ex.StackTrace + "\nReport is being uploaded. Please try again in few minutes");
            }

        }

        private void ShowStoreNumber(string selectedDate, string UserID, string isfinal)
        {
            string dateParam = selectedDate;

            DataTable Fct_StoreNumber = GP_DAL_Functions.GetFct_StoreNumberCol2(dateParam, UserID, isfinal);

            if (dateParam != null)
            {
                if (Fct_StoreNumber != null && Fct_StoreNumber.Rows.Count > 0)
                {
                    if (isfinal == "0")
                    {
                        var latestTimeStamp = Fct_StoreNumber.Rows[0].Field<DateTime>("TimeStamp");
                        //var adjustedTimeStamp = latestTimeStamp.AddDays(-1).Date;
                        var adjustedTimeStamp = DateTime.ParseExact(Fct_StoreNumber.Rows[0]["DateKey"].ToString(), "yyyyMMdd", null);
                        ViewBag.LatestTimeStamp = latestTimeStamp.ToString("yyyy-MM-dd HH:mm:ss"); // Format as needed
                        ViewBag.ReportDate = adjustedTimeStamp.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        DateTime parsedDate = DateTime.Parse(selectedDate);
                        ViewBag.ReportDate = parsedDate.ToString("yyyy-MM-dd");
                        var latestTimeStamp = Fct_StoreNumber.Rows[0].Field<DateTime>("TimeStamp");
                        ViewBag.LatestTimeStamp = latestTimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            else
            {
                if (Fct_StoreNumber != null && Fct_StoreNumber.Rows.Count > 0)
                {
                    if (isfinal == "0")
                    {
                        var latestTimeStamp = Fct_StoreNumber.AsEnumerable().Max(row => row.Field<DateTime>("TimeStamp"));
                        //var adjustedTimeStamp = latestTimeStamp.AddDays(-1);
                        var adjustedTimeStamp = Fct_StoreNumber.AsEnumerable().Where(row => row["DateKey"] != DBNull.Value).Max(row => DateTime.ParseExact(row["DateKey"].ToString(), "yyyyMMdd", null));
                        ViewBag.LatestTimeStamp = latestTimeStamp.ToString("yyyy-MM-dd HH:mm:ss");
                        ViewBag.ReportDate = adjustedTimeStamp.ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        DateTime parsedDate = DateTime.Parse(selectedDate);
                        ViewBag.LatestTimeStamp = parsedDate.ToString("yyyy-MM-dd HH:mm:ss");
                        ViewBag.ReportDate = parsedDate.ToString("yyyy-MM-dd");
                    }
                }
            }

            List<Fct_StoreNumberAttributesModel> lisn2_lst = new List<Fct_StoreNumberAttributesModel>();

            foreach (DataRow row in Fct_StoreNumber.Rows)
            {
                Fct_StoreNumberAttributesModel model = new Fct_StoreNumberAttributesModel
                {
                    VP = row["VP"].ToString(),
                    Region = row["Region"].ToString(),
                    SD = row["SD"].ToString(),
                    UniqueID = row["Unique ID"].ToString(),
                    DealerCode = row["Dealer Code"].ToString(),
                    Tiers = row["Tiers"].ToString(),
                    Store = row["Store"].ToString(),
                    HITStore = row["HIT Store"].ToString(),
                    Market = row["Market"].ToString(),
                    //MULMngr = row["MUL_MktMngr"].ToString(),
                    MULMngr = row["SD"].ToString(),
                    MULMkt = row["MUL_Market"].ToString(),
                    TM = row["TM"].ToString(),
                    RSMSRSM = row["RSM/SRSM"].ToString(),
                    Role = row["Role"].ToString(),
                    MonthlyAchievedHoursTrendingPercentage = row["Monthly _Achived Hours Trending %"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    Converged = row["converged%"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    DateKey = row["DateKey"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString()

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()
                };

                lisn2_lst.Add(model);
            }

            ViewBag.Date = GpReport.DateTimes;
            ViewData["Fct_StoreNumber"] = lisn2_lst;

            ////////////////Get Total

            DataTable Fct_StoreNumberTotal = GP_DAL_Functions.GetFct_StoreNumberTotal(dateParam, UserID, isfinal);
            List<Fct_StoreNumberAttributesModel> lisn_tot = new List<Fct_StoreNumberAttributesModel>();

            foreach (DataRow row in Fct_StoreNumberTotal.Rows)
            {
                Fct_StoreNumberAttributesModel model3 = new Fct_StoreNumberAttributesModel
                {
                    MonthlyAchievedHoursTrendingPercentage = row["Monthly Achieved Hours Trending%"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    Converged = row["converged%"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    //TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString()

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()
                };

                lisn_tot.Add(model3);
            }
            ViewBag.Date = GpReport.DateTimes;

            ViewData["GetFct_StoreNumberTotal"] = lisn_tot;

            ///////////////////////////////////////////////////////////////////////////////


            ////////////////Get TM Total

            DataTable Fct_StoreNumberTotalTM = GP_DAL_Functions.GetFct_StoreNumberTotalTM(dateParam, UserID, isfinal);
            List<Fct_StoreNumberAttributesModel> lisn_totTM = new List<Fct_StoreNumberAttributesModel>();

            foreach (DataRow row in Fct_StoreNumberTotalTM.Rows)
            {
                Fct_StoreNumberAttributesModel model4 = new Fct_StoreNumberAttributesModel
                {
                    Market = row["Market"].ToString(),
                    TM = row["TM"].ToString(),

                    MonthlyAchievedHoursTrendingPercentage = row["Monthly Achieved Hours Trending%"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    Converged = row["converged%"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    //TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString()

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()
                };

                lisn_totTM.Add(model4);
            }

            ViewData["GetFct_StoreNumberTotalTM"] = lisn_totTM;

            ///////////////////////////////////////////////////////////////////////////////

            ////////////////Get MMM Total

            DataTable Fct_StoreNumberTotalMMM = GP_DAL_Functions.GetFct_StoreNumberTotalMMM(dateParam, UserID, isfinal);
            List<Fct_StoreNumberAttributesModel> lisn_totMMM = new List<Fct_StoreNumberAttributesModel>();

            foreach (DataRow row in Fct_StoreNumberTotalMMM.Rows)
            {
                Fct_StoreNumberAttributesModel model4 = new Fct_StoreNumberAttributesModel
                {
                    //Market = row["Market"].ToString(),
                    MULMngr = row["sd"].ToString(),

                    MonthlyAchievedHoursTrendingPercentage = row["Monthly Achieved Hours Trending%"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    Converged = row["converged%"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    //TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString()

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()
                };

                lisn_totMMM.Add(model4);
            }

            ViewData["GetFct_StoreNumberTotalSD"] = lisn_totMMM;

            ///////////////////////////////////////////////////////////////////////////////

            //For Last TimeStamp

            //if (Fct_StoreNumber != null && Fct_StoreNumber.Rows.Count > 0)
            //{
            //    // Use LINQ to find the latest TimeStamp
            //    var latestTimeStamp = Fct_StoreNumber.AsEnumerable()
            //                                         .Max(row => row.Field<DateTime>("TimeStamp"));
            //    var adjustedTimeStamp = latestTimeStamp.AddDays(-1);
            //    // Output the result
            //    ViewBag.LatestTimeStamp = latestTimeStamp.ToString("yyyy-MM-dd HH:mm:ss"); // Format as needed
            //                                                                               // Subtract one day from the TimeStamp

            //    ViewBag.ReportDate = adjustedTimeStamp.ToString("yyyy-MM-dd");
            //}
            //else
            //{
            //    ViewBag.LatestTimeStamp = "No data available";
            //}

            //For Filters

            List<SelectListItem> vpList = new List<SelectListItem>();
            List<SelectListItem> regionList = new List<SelectListItem>();
            List<SelectListItem> sdList = new List<SelectListItem>();
            List<SelectListItem> tiersList = new List<SelectListItem>();
            List<SelectListItem> storesList = new List<SelectListItem>();
            List<SelectListItem> marketList = new List<SelectListItem>();
            List<SelectListItem> mmarketList = new List<SelectListItem>();
            List<SelectListItem> mmmList = new List<SelectListItem>();
            List<SelectListItem> tmList = new List<SelectListItem>();
            List<SelectListItem> rsmList = new List<SelectListItem>();
            List<SelectListItem> roleList = new List<SelectListItem>();

            HashSet<string> vpNamesSet = new HashSet<string>();
            HashSet<string> regionNamesSet = new HashSet<string>();
            HashSet<string> sdNamesSet = new HashSet<string>(); // To store unique region names
            HashSet<string> tiersNamesSet = new HashSet<string>();
            HashSet<string> storesNamesSet = new HashSet<string>();
            HashSet<string> marketNamesSet = new HashSet<string>();
            HashSet<string> mmarketNamesSet = new HashSet<string>();
            HashSet<string> mmmNamesSet = new HashSet<string>();
            HashSet<string> tmNamesSet = new HashSet<string>();
            HashSet<string> rsmNamesSet = new HashSet<string>();
            HashSet<string> roleNamesSet = new HashSet<string>();

            // Assuming your DataTable has a column named "SD" that holds the region names
            foreach (DataRow row in Fct_StoreNumber.Rows)
            {
                string vpName = row["VP"].ToString();
                string sdName = row["SD"].ToString(); // Assuming the SD column contains SD names
                string tiersName = row["Tiers"].ToString();
                string regionName = row["Region"].ToString();
                string storesName = row["Store"].ToString();
                string marketName = row["Market"].ToString();
                string mmarketName = row["MUL_Market"].ToString();
                string mmmName = row["MUL_MktMngr"].ToString();
                string tmName = row["TM"].ToString();
                string rsmName = row["RSM/SRSM"].ToString();
                string roleName = row["Role"].ToString();

                vpNamesSet.Add(vpName);
                sdNamesSet.Add(sdName);
                tiersNamesSet.Add(tiersName);
                regionNamesSet.Add(regionName);
                storesNamesSet.Add(storesName);
                marketNamesSet.Add(marketName);
                mmarketNamesSet.Add(mmarketName);
                mmmNamesSet.Add(mmmName);
                //storesNamesSet.Add(mmmName);
                //storesNamesSet.Add(tmName);
                //storesNamesSet.Add(rsmName);
                //storesNamesSet.Add(roleName);

                //// Add to the list as SelectListItem
                //vpList.Add(new SelectListItem
                //{
                //    Value = vpName, // You can change this to another column if needed
                //    Text = vpName
                //});

                //// Add to the list as SelectListItem
                //regionList.Add(new SelectListItem
                //{
                //    Value = regionName,
                //    Text = regionName
                //});

                // Add to the list as SelectListItem
                sdList.Add(new SelectListItem
                {
                    Value = sdName,
                    Text = sdName
                });

                tiersList.Add(new SelectListItem
                {
                    Value = tiersName,
                    Text = tiersName
                });

                storesList.Add(new SelectListItem
                {
                    Value = storesName,
                    Text = storesName
                });

                marketList.Add(new SelectListItem
                {
                    Value = marketName,
                    Text = marketName
                });
                mmarketList.Add(new SelectListItem
                {
                    Value = mmarketName,
                    Text = mmarketName
                });
                mmmList.Add(new SelectListItem
                {
                    Value = mmmName,
                    Text = mmmName
                });

                tmList.Add(new SelectListItem
                {
                    Value = tmName,
                    Text = tmName
                });

                //rsmList.Add(new SelectListItem
                //{
                //    Value = rsmName,
                //    Text = rsmName
                //});

                //roleList.Add(new SelectListItem
                //{
                //    Value = roleName,
                //    Text = roleName
                //});

            }

            ////Unique record working in list

            //if (vpList != null && vpList.Count > 0)
            //{
            //    // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
            //    var distinctVPList = vpList.GroupBy(x => x.Value)
            //                               .Select(g => g.First()) // Or group by x.Text if needed
            //                               .ToList();
            //    ViewBag.VPList = distinctVPList;
            //}
            //else
            //{
            //    ViewBag.VPList = new List<SelectListItem>();
            //}
            //if (regionList != null && regionList.Count > 0)
            //{
            //    // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
            //    var distinctRegionList = regionList.GroupBy(x => x.Value)
            //                               .Select(g => g.First()) // Or group by x.Text if needed
            //                               .ToList();
            //    ViewBag.RegionList = distinctRegionList;
            //}
            //else
            //{
            //    ViewBag.RegionList = new List<SelectListItem>();
            //}
            if (sdList != null && sdList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctSDList = sdList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.SDList = distinctSDList;
            }
            else
            {
                ViewBag.SDList = new List<SelectListItem>();
            }
            if (tiersList != null && tiersList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distincttiersList = tiersList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.tiersList = distincttiersList;
            }
            else
            {
                ViewBag.tiersList = new List<SelectListItem>();
            }
            if (storesList != null && storesList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctStoresList = storesList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.StoresList = distinctStoresList;
            }
            else
            {
                ViewBag.StoresList = new List<SelectListItem>();
            }
            if (marketList != null && marketList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctMarketList = marketList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.MarketList = distinctMarketList;
            }
            else
            {
                ViewBag.MarketList = new List<SelectListItem>();
            }
            if (mmarketList != null && mmarketList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctmMarketList = mmarketList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.mMarketList = distinctmMarketList;
            }
            else
            {
                ViewBag.mMarketList = new List<SelectListItem>();
            }
            if (mmmList != null && mmmList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctmmmList = mmmList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.mmmList = distinctmmmList;
            }
            else
            {
                ViewBag.mmmList = new List<SelectListItem>();
            }
            if (tmList != null && tmList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctTMList = tmList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.TMList = distinctTMList;
            }
            else
            {
                ViewBag.TMList = new List<SelectListItem>();
            }
            //if (rsmList != null && rsmList.Count > 0)
            //{
            //    // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
            //    var distinctRSMList = rsmList.GroupBy(x => x.Value)
            //                               .Select(g => g.First()) // Or group by x.Text if needed
            //                               .ToList();
            //    ViewBag.RSMList = distinctRSMList;
            //}
            //else
            //{
            //    ViewBag.RSMList = new List<SelectListItem>();
            //}
            //if (roleList != null && roleList.Count > 0)
            //{
            //    // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
            //    var distinctRoleList = roleList.GroupBy(x => x.Value)
            //                               .Select(g => g.First()) // Or group by x.Text if needed
            //                               .ToList();
            //    ViewBag.RoleList = distinctRoleList;
            //}
            //else
            //{
            //    ViewBag.RoleList = new List<SelectListItem>();
            //}
        }


        [HttpPost]
        public JsonResult SaveUniqueIDToSession(string UniqueID, string DealerCode, string DateKey)
        {
            Session["UniqueID"] = UniqueID;
            Session["DealerCode"] = DealerCode;
            Session["DateKey"] = DateKey;
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult GetNoteData(string UniqueID)
        {
            string UniqueID_Param = UniqueID;

            DataTable GetNoteByParameters = GP_DAL_Functions.GetNoteByParameters(UniqueID_Param);

            NoteModel result = null;

            if (GetNoteByParameters.Rows.Count > 0)
            {
                var row = GetNoteByParameters.Rows[0];
                result = new NoteModel
                {
                    UniqueID = row["Unique ID"].ToString(),
                    DealerCode = row["Dealer Code"].ToString(),
                    DateKey = row["DateKey"].ToString(),
                    Comment = row["Comment"].ToString(),
                    Note = row["Note"].ToString(),
                    Employee = row["Employee"].ToString(),
                };
            }

            return Json(result); // Returning a single NoteModel
        }

        public JsonResult AddOrUpdateNote(string Comment, string Note, string Employee)
        {
            var uniqueID = Session["UniqueID"] as string;
            var dealerCode = Session["DealerCode"] as string;
            var dateKey = Session["DateKey"] as string;
            string message;

            try
            {
                // Check if the note with the given UniqueID exists
                bool noteExists = GP_DAL_Functions.CheckIfNoteExists(uniqueID); // Implement this function to check existence

                if (noteExists)
                {
                    // Call the update stored procedure
                    GP_DAL_Functions.spUpdateNote(uniqueID, dealerCode, dateKey, Comment, Note, Employee);
                    message = "Note updated successfully.";
                }
                else
                {
                    // Call the insert stored procedure
                    GP_DAL_Functions.spInsertNote(uniqueID, dealerCode, dateKey, Comment, Note, Employee);
                    message = "Employee Note Inserted successfully.";
                }

                return Json(new { success = true, message = message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message });
            }
        }

        [HttpGet]
        public JsonResult GetEmpDetail(string UniqueID, string dateParam)
        {
            ////////////////Get Employee Detail
            List<Fct_StoreNumberAttributesModel> lisn_emp = new List<Fct_StoreNumberAttributesModel>();
            DataTable EmpDet = GP_DAL_Functions.EmpDet(UniqueID, dateParam);
            foreach (DataRow row in EmpDet.Rows)
            {
                Fct_StoreNumberAttributesModel modelDetail = new Fct_StoreNumberAttributesModel
                {
                    UniqueID = row["Unique ID"].ToString(),
                    DealerCode = row["Dealer Code"].ToString(),
                    Store = row["Store"].ToString(),
                    Market = row["Market"].ToString(),
                    TM = row["TM"].ToString(),
                    MULMngr = row["Multi Market Manager"].ToString(),
                    EmpID = row["Emp ID (Paycom)"].ToString(),
                    Employees = row["Employees"].ToString(),
                    Role = row["Role"].ToString(),
                    Status = row["Status"].ToString(),
                    HireDate = row["Hire Date"].ToString(),
                    PayType = row["Pay Type"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    GPGOAL = row["GP GOAL ($)"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    Trending = row["Trending ($)"].ToString(),
                    TrendingPer = row["Trending % "].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    CRUAchDATADollars = row["CRU Ach $"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack1 = row["Charge Back 1"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back 2"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra/ Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra/ Non Premium Activation $GP"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back 3"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back 4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back 5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIABInternet = row["AIA Internet"].ToString(),
                    AIAInternetCancellation = row["AIA Internet Cancellation"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack6 = row["Charge Back 6"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back 7"].ToString(),
                    BroadbandFiberUpgrade = row["Broad Band +Fiber Upgrade"].ToString(),
                    BroadbandFiberUpgradeNetOFF = row["Broad Band +Fiber Upgrade Net OFF"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrades GP"].ToString(),
                    BroadbandNewFiberGP = row["Broadband + New fiber $GP"].ToString(),
                    BroadbandFiberGP = row["Broadband +Fiber $GP"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back 8"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    TWCDevicesQty = row["T.W.C Devices QTY"].ToString(),
                    TWCDevicesQtyNetOFF = row["Tab+Wear+Connected Devices Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back 9"].ToString(),
                    TWCDevicesDollars = row["Tab+Wear+Connected Devices $ GP"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic Spif"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack10 = row["Charge Back 10"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    INSURANCE = row["INSURANCE"].ToString(),
                    ChargeBack11 = row["Charge Back 11"].ToString(),
                    INSURANCEGP = row["INSURANCE $GP"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    PROTECTION_PACK = row["PROTECTION PACK"].ToString(),
                    PROTECTION_PACK_NetOFF = row["PROTECTION PACK Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back 12"].ToString(),
                    PROTECTION_PACK_GP = row["PROTECTION PACK $GP"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    PAY = row["PAY"].ToString(),
                    CommissionBucketMTD = row["Commission Bucket MTD(5th Nov' 24)"].ToString(),
                    EOMCommissionTrendingBucket = row["Nov’24 EOM Commission Trending Bucket"].ToString(),
                    EffectiveRate = row["Effective Rate"].ToString(),
                    //ProtAdv & HomeTech % DREAM(Wk 1 to 7)    
                    //DREAM(Wk 8 to 14)   DREAM(Wk 15 to 21)  
                    //DREAM(Wk 22 to 28)  DREAM(Wk 29 to 31)  
                    //Working Days    Trending Days   Remaing Days    PPVGA Commission    PPVGA Elite Commission  PPVGA Extra Commission  FN Commission   CRU Commission  Premium Video Commission    AIA Internet Commission Broadband Commission    New Fiber Commission    Fiber Upgrade Commission    UPGCommission   Upgrade Elite Commission    Upgrade Extra Commission    Wearbale & Tablet Commission    Hometech    Next Up 1   Protect Advantage 1 Commission  Protect Advantage 4 Commission  Prepaid Commission  Accessory GP Commission CSAT 1  Commission  Semi Comm Calculation   Commission Eligibility(YES - 1)(NO - 0)   Reg Hrs Achved  OT  Hourly Rate(USD)   Salary  Geo SPIFF OPS   Geo Rate    Total Comp New  PAY Commission Bucket MTD(5th Nov' 24)	Nov’24 EOM Commission Trending Bucket	Effective Rate	ReportDate	TimeStamp

                };
                lisn_emp.Add(modelDetail);
            }
            ViewData["GetEmpDetail"] = lisn_emp;
            return Json(lisn_emp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetGTotal(string dateParam, string Sd, string TM, string MKT, string MMM, string Stores, string Tiers, string isfinal, string UserID)
        {

            ////////////////Get Filter Total
            string NullIfEmpty(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

            dateParam = NullIfEmpty(dateParam);
            Sd = NullIfEmpty(Sd);
            TM = NullIfEmpty(TM);
            MKT = NullIfEmpty(MKT);
            MMM = NullIfEmpty(MMM);
            Stores = NullIfEmpty(Stores);
            Tiers = NullIfEmpty(Tiers);
            isfinal = NullIfEmpty(isfinal);

            DataTable FilterTotal = GP_DAL_Functions.FilterTotal(dateParam, Sd, TM, MKT, MMM, Stores, Tiers, isfinal, UserID);
            List<Fct_StoreNumberAttributesModel> lisn_tot = new List<Fct_StoreNumberAttributesModel>();

            foreach (DataRow row in FilterTotal.Rows)
            {
                Fct_StoreNumberAttributesModel modelFT = new Fct_StoreNumberAttributesModel
                {
                    MonthlyAchievedHoursTrendingPercentage = row["Monthly Achieved Hours Trending%"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    Converged = row["converged%"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    //TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString()

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()

                };

                lisn_tot.Add(modelFT);
            }

            ViewData["GetFilterTotal"] = lisn_tot;

            Fct_StoreNumberAttributesModel result = null;

            if (FilterTotal.Rows.Count > 0)
            {
                var row = FilterTotal.Rows[0];
                result = new Fct_StoreNumberAttributesModel
                {
                    MonthlyAchievedHoursTrendingPercentage = row["Monthly Achieved Hours Trending%"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
                    PPVGAPerTrafficPercentage = row["PPVGA per Traffic%"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    Converged = row["converged%"].ToString(),
                    BroadbandFiberNetOFF = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    TrendingToBreakEven = row["Trending to BreakEven"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOFF = row["MTD UPGRADES Net OFF"].ToString(),
                    TWCDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWCDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWCDevicesQtyNetOFF = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    ChargeBack = row["Charge Back"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    GrossAddsGPTrending = row["GROSS ADDS $GP Trending"].ToString(),
                    ARBusinessFNProgramAverageQ2Target = row["AR Business FN Program Average Q2 Target"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    RemainingToAchieveFNAverageQ2Target = row["Remaining to Achive FN Average Q2 target"].ToString(),
                    FNAchTrend = row["FN Ach Trend"].ToString(),
                    FNAchDollars = row["FN Ach $"].ToString(),
                    ARBusinessCRUProgramAverageQ2Target = row["AR Business CRU Program Average Q2 Target"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchDATA = row["CRU Ach DATA"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    RemainingToAchieveCRUAverageQ2Target = row["Remaining to Achive CRU Average Q2 target"].ToString(),
                    CRUAchTrend = row["CRU Ach Trend"].ToString(),
                    CRUAchVoiceDollars = row["CRU Ach Voice $"].ToString(),
                    CRUAchDATADollars = row["CRU Ach DATA $"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    ChargeBack1 = row["Charge Back1"].ToString(),
                    MTDUpgradesTrending = row["MTD UPGRADES_Trending"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    MTDUpgradesGPTrending = row["MTD UPGRADES $GP Trending"].ToString(),
                    NextUp = row["Next UP"].ToString(),
                    NextUpSpif = row["Next UP Spif"].ToString(),
                    PremiumActivation = row["Premium Activation"].ToString(),
                    PremiumActivationNetOFF = row["Premium Activation Net OFF"].ToString(),
                    ChargeBack2 = row["Charge Back2"].ToString(),
                    PremiumActivationGP = row["Premium Activation $GP"].ToString(),
                    ExtraActivation = row["Extra Activation"].ToString(),
                    ExtraActivationNetOFF = row["Extra Activation Net OFF"].ToString(),
                    ChargeBack3 = row["Charge Back3"].ToString(),
                    ExtraActivationGP = row["Extra Activation $GP"].ToString(),
                    NonExtraNonPremiumActivation = row["Non Extra / Non Premium Activation"].ToString(),
                    NonExtraNonPremiumActivationNetOFF = row["Non Extra / Non Premium Activation Net OFF"].ToString(),
                    NonExtraNonPremiumActivationGP = row["Non Extra / Non Premium Activation $GP"].ToString(),
                    PremiumUpgrade = row["Premium Upgrade"].ToString(),
                    ChargeBack4 = row["Charge Back4"].ToString(),
                    PremiumUpgradeNetOff = row["Premium Upgrade Net Off"].ToString(),
                    PremiumUpgradeGP = row["Premium Upgrade $GP"].ToString(),
                    ExtraUpgrade = row["Extra Upgrade"].ToString(),
                    ChargeBack5 = row["Charge Back5"].ToString(),
                    ExtraUpgradeNetOff = row["Extra Upgrade Net Off"].ToString(),
                    ExtraUpgradeGP = row["Extra Upgrade $GP"].ToString(),
                    AIAGoals = row["AIA Goals"].ToString(),
                    AIAInternet = row["AIA Internet"].ToString(),
                    ChargeBack6 = row["Charge Back6"].ToString(),
                    AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
                    AIATrending = row["AIA Trending"].ToString(),
                    AIAInternetGP = row["AIA Internet $GP"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandLessThen300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    TotalBroadbandNewFiber = row["Total Broadband + Newfiber"].ToString(),
                    BroadbandNewFiberNetOFF = row["Broadband + New fiber Net OFF"].ToString(),
                    ChargeBack7 = row["Charge Back7"].ToString(),
                    FiberUpgrades = row["Fiber Upgrades"].ToString(),
                    FiberUpgradesNetOFF = row["Fiber Upgrades Net OFF"].ToString(),
                    ChargeBack8 = row["Charge Back8"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    BroadbandLessThen300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    FiberUpgradeGP = row["Fiber Upgrade  $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandFiberGPTrending = row["Broad Band + Fiber _$GP Trending"].ToString(),
                    TurboFeature = row["Turbo Feature"].ToString(),
                    TurboFeatureGP = row["Turbo Feature $GP"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOFF = row["Prem Video Net OFF"].ToString(),
                    ChargeBack9 = row["Charge Back9"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    PremVideoGPTrending = row["Prem Video $GP Trending"].ToString(),
                    EntertainmentGoals = row["Entertainment Goals"].ToString(),
                    EntertainmentAch = row["Entertainment Ach"].ToString(),
                    EntertainmentTrending = row["Entertainment _Trending"].ToString(),
                    EntertainmentTrendingToGoalsPercentage = row["Entertainment Trending to Goals %"].ToString(),
                    TWCDevicesQty = row["T#W#C Devices QTY"].ToString(),
                    ChargeBack10 = row["Charge Back10"].ToString(),
                    TWCDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWCDevicesDollars = row["T,W,C Devices $"].ToString(),
                    ProjectedGeographicSpif = row["Projected Geographic spif"].ToString(),
                    PrepaidQty = row["Prepaid QTY"].ToString(),
                    PrepaidToGA = row["Prepaid to GA"].ToString(),
                    PrepaidNetOFF = row["Prepaid Net OFF"].ToString(),
                    ChargeBack11 = row["Charge Back11"].ToString(),
                    PrepaidGP = row["Prepaid $GP"].ToString(),
                    PrepaidWithAutopay = row["Prepaid with Autopay"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    AccessQtyTrending = row["Access Qty _Trending"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    FeaturesQty = row["Features QTY"].ToString(),
                    FeaturesQtyNetOFF = row["Features QTY Net OFF"].ToString(),
                    ChargeBack12 = row["Charge Back12"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                    FeaturesGP = row["Features $GP"].ToString(),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    WeeklyEmployeeAveragePerStore = row["Weekly Employee Average Per store"].ToString(),
                    MonthlyBudgetedHRS = row["Monthly Budgeted HRS"].ToString(),
                    //TrainingHours = row["Training Hours"].ToString(),
                    MonthlyAchievedHRS = row["Monthly Achived HRS"].ToString(),
                    MonthlyAchievedHoursTrending = row["Monthly Achived Hours Trending"].ToString(),
                    GACloseRt = row["GA Close Rt"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),

                    //New Dot Columns
                    NextUPTrendPer = row["Next UP Trend %"].ToString(),
                    PremiumPerGA = row["Premium % to GA"].ToString(),
                    ExtraPerGA = row["Extra % to GA"].ToString(),
                    ExtraUnlMix70Per = row["Extra +Unl Mix (70%)"].ToString(),
                    TotalNewFiber = row["Total New Fiber"].ToString(),
                    FiberUpgTrend = row["Fiber Upgarde Trending"].ToString(),
                    TotalProt = row["Total Protection"].ToString(),
                    TotalProtNetOFF = row["Total Protection Net OFF "].ToString(),
                    ProtAdv1NetOff = row["ProtAdv 1 Net Off"].ToString(),
                    ProtAdv4NetOff = row["ProtAdv 4 Net Off"].ToString(),
                    PrepQTYTrend = row["Prepaid Qty Trend"].ToString(),
                    AccessRevTrend = row["Access Revenue Trending"].ToString(),
                    WeeklyAchivedHRS = row["Weekly Achived HRS"].ToString(),
                    BudgEmp = row["Budgeted Empolyees"].ToString(),
                    FullTimeHourlyHeadCount = row["Full Time Hourly Head Count"].ToString(),
                    PartTimeHourlyHeadCount = row["Part Time Hourly Head Count"].ToString(),
                    TotalHourlyCount = row["Total Hourly Count (Full time+Part time/2)"].ToString(),
                    HourlyHeadCntsVar = row["Hourly Head Counts Variance"].ToString(),
                    HourlyCurrHeadCntVarHrs = row["Hourly Current Head Count Variance Hours"].ToString(),
                    HomeInternet = row["Home Internet"].ToString()

                };
            }

            return Json(result);
        }



        public ActionResult SummaryMobileView()
        {
            try
            {
                //global.userID = Request.QueryString["userid"];

                //userID = Session["userid"].ToString();

                //global.decrpedUserId = EncryptionHelper.Decrypt(global.userID);

                global.decrpedUserId = "1";

                ShowStoreSummary(null, global.decrpedUserId, "0");

                return View();

            }
            catch (Exception ex)
            {
                return Content(ex.Message + "\n\n" + ex.StackTrace + "\nReport is being uploaded. Please try again in few minutes");
                throw;
            }
        }

        [HttpPost]
        public ActionResult SummaryMobileView(string selectedDate, string isfinal)
        {
            try
            {
                global.decrpedUserId = EncryptionHelper.Decrypt(global.userID);
                ShowStoreSummary(selectedDate, global.decrpedUserId, isfinal);

                return View();

            }
            catch (Exception ex)
            {
                return Content(ex.Message + "\n\n" + ex.StackTrace + "\nReport is being uploaded. Please try again in few minutes");
                throw;
            }
        }

        private void ShowStoreSummary(string selectedDate, string UserID, string isfinal)
        {

            string dateParam = selectedDate;

            // Call your function with the date parameter
            DataTable GetFct_Summary = GP_DAL_Functions.GetFct_StoreSummary(dateParam, UserID, isfinal);

            List<Fct_StoreSummaryAttributesModel> lisn2_lst = new List<Fct_StoreSummaryAttributesModel>();

            foreach (DataRow row in GetFct_Summary.Rows)
            {
                Fct_StoreSummaryAttributesModel model = new Fct_StoreSummaryAttributesModel
                {
                    SD = row["SD"].ToString(),
                    TM = row["TM"].ToString(),
                    Market = row["Market"].ToString(),
                    Store = row["Store"].ToString(),
                    Tier = row["Tier"].ToString(),
                    StoreContact = row["StoreContact"].ToString(),
                    ReportDate = row["ReportDate"].ToString(),
                    DealerCode = row["DealerCode"].ToString(),
                    Monthly_AchivedHoursTrending = MultiplyAndRoundPercentage(row["Monthly_AchivedHoursTrending"]),
                    WeeklyBudgetedHRS = row["Weekly Budgeted HRS"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                    GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                    GrossAdds = row["GROSS ADDS"].ToString(),
                    GrossAddsNetOff = row["GROSS ADDS Net OFF"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    PPVGAPerTraffic = row["PPVGA per Traffic%"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    BroadbandFiberNetOff = row["Broadband +_Fiber Net OFF"].ToString(),
                    FiberGreenCheck = row["Fiber Green Check"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    ProtAdvHomeTechPercentage = row["ProtAdv & HomeTech %"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrending = MultiplyAndRoundPercentage(row["GP Trending %"]),
                    GPTrendingPercentage = row["GP Trending %"].ToString(),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPS Trending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["Upgrade Trending to Goals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOff = row["MTD UPGRADES Net OFF"].ToString(),
                    TWDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWDevicesQtyGoals = row["T,W,C Devices QTY Goals"].ToString(),
                    TWDevicesQtyNetOff = row["T,W,C Devices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA Consumer Conversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA Green Check Consumer"].ToString(),
                    AIABInternet = row["AIA B Internet"].ToString(),
                    AIACInternet = row["AIA C Internet"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per- Traffic %"].ToString(),
                    CRUAchMTD = row["CRU Ach MTD"].ToString(),
                    FNAchMTD = row["FN Ach MTD"].ToString(),
                    GrossAddsTrend = row["GROSS ADDS Trend"].ToString(),
                    BroadbandGoals = row["Broadband Goals"].ToString(),
                    BroadbandFiber = row["Broadband +_Fiber"].ToString(),
                    BroadBandLessThan300MB = row["Broad Band less Then (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    FiberUpgradesNetOff = row["Fiber Upgrades Net OFF"].ToString(),
                    BroadbandFiberTrend = row["Broadband +_Fiber _Trend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband + Fiber Trend%"].ToString(),
                    PremVideoGoals = row["Prem Video Goals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOff = row["Prem Video Net OFF"].ToString(),
                    PremVideoTrendPercentage = row["Prem Video Trend %"].ToString(),
                    PremVideoTrend = row["Prem Video Trend"].ToString(),
                    TotalGPAchievedWithSpif = row["Total $GP Achieved - With Spif"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalOPSTrending = row["TOTAL OPS Trending"].ToString(),
                    GrossAddsGP = row["GROSS ADDS $GP"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES $GP"].ToString(),
                    BroadBandFiberGP = row["Broad Band + Fiber _$GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandLessThan300MBQISpiff = row["Broadband Less then (300Mb) QI Spiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber (300 MB) QI Spiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber (500 MB) QI Spiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber (1G) QI Spiff"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessRevenue = row["Access $ Revenue"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    FNAchAverageQTD = row["FN Ach  Average QTD"].ToString(),
                    FNAch = row["FN Ach $"].ToString(),
                    //CRUVGACnt = row["CRU VGA Cnt"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach  Average QTD"].ToString(),
                    CRUAchVoiceDollar = row["CRU Ach Voice $"].ToString(),
                    CRUAchData = row["CRU Ach DATA $"].ToString(),
                    TWDevicesQtyTrend = row["T,W,C Devices QTY Trend"].ToString(),
                    TWDevicesGP = row["T,W,C Devices GP"].ToString(),
                    HomeTechProtect = row["HomeTech Protect"].ToString(),
                    TotalProtectionPercentage = row["Total Protection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    //TimeStamp = row["TimeStamp"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),

                };

                lisn2_lst.Add(model);
            }

            //ViewBag.Date = GpReport.DateTimes;
            ViewData["GetFct_Summary"] = lisn2_lst;


            ////////////////Get_Summary Total
            DataTable Fct_StoreNumberTotal = GP_DAL_Functions.GetRecords_summaryTotal(dateParam, UserID, isfinal);
            List<Fct_StoreSummaryAttributesModel> lisn_tot = new List<Fct_StoreSummaryAttributesModel>();
            foreach (DataRow row in Fct_StoreNumberTotal.Rows)
            {
                Fct_StoreSummaryAttributesModel totalNum = new Fct_StoreSummaryAttributesModel
                {
                    Monthly_AchivedHoursTrending = row["MonthlyAchived HoursTrending %"].ToString(),
                    WeeklyBudgetedHRS = row["WeeklyBudgetedHRS"].ToString(),
                    GrossAddsTrendToGoal = row["GROSS ADDSTrend% To Goal"].ToString(),
                    TotalTraffic = row["Total Traffic"].ToString(),
                    PPVGAPerTraffic = row["PPVGA perTraffic%"].ToString(),
                    FiberConversion = row["Fiber Conversion"].ToString(),
                    APO = row["APO"].ToString(),
                    CSAT = row["CSAT"].ToString(),
                    //ProtAdvHomeTechPercentage = row["ProtAdv &HomeTech%"].ToString(),
                    BreakEvenNumbers = row["BreakEven Numbers"].ToString(),
                    GPWithSpifTrending = row["$GP - With Spif Trending"].ToString(),
                    GPTrending = MultiplyAndRoundPercentage(row["GPTrending%"]),
                    TotalGPGoals = row["Total $GP Goals"].ToString(),
                    OPSTrendingToGoalsPercentage = row["OPSTrending to Goals %"].ToString(),
                    TotalOPSGoals = row["TOTAL OPS Goals"].ToString(),
                    TotalOPS = row["TOTAL OPS"].ToString(),
                    UpgradeTrendingToGoalsPercentage = row["UpgradeTrending toGoals %"].ToString(),
                    MTDUpgradesGoals = row["MTD UPGRADES Goals"].ToString(),
                    MTDUpgradesNetOff = row["MTDUPGRADESNet OFF"].ToString(),
                    TWDevicesTrendToGoalPercentage = row["T,W,C Devices Trend to Goal %"].ToString(),
                    TWDevicesQtyGoals = row["T,W,CDevices QTY Goals"].ToString(),
                    TWDevicesQtyNetOff = row["T,W,CDevices QTY Net OFF"].ToString(),
                    AIABusinessConversion = row["AIA Business Conversion"].ToString(),
                    AIAConsumerConversion = row["AIA CConversion"].ToString(),
                    AIAGreenCheckConsumer = row["AIA GreenCheck"].ToString(),
                    AIABInternet = row["AIA BInternet"].ToString(),
                    AIACInternet = row["AIA CInternet"].ToString(),
                    OPSPerTrafficPercentage = row["OPS Per-Traffic %"].ToString(),
                    CRUAchMTD = row["CRU AchMTD"].ToString(),
                    FNAchMTD = row["FN AchMTD"].ToString(),
                    //GrossAddsTrend = row["GROSSADDSTrend"].ToString(),
                    BroadbandGoals = row["BroadbandGoals"].ToString(),
                    BroadbandFiber = row["Broadband +Fiber"].ToString(),
                    BroadBandLessThan300MB = row["BroadBand lessThen (300MB)"].ToString(),
                    NewFiber300MB = row["New Fiber (300MB)"].ToString(),
                    NewFiber500MB = row["New Fiber (500MB)"].ToString(),
                    NewFiber1G = row["New Fiber (1G)"].ToString(),
                    FiberUpgradesNetOff = row["Fiber UpgradesNet OFF"].ToString(),
                    BroadbandFiberTrend = row["Broadband + FiberTrend"].ToString(),
                    BroadbandFiberTrendPercentage = row["Broadband +Fiber Trend%"].ToString(),
                    PremVideoGoals = row["Prem VideoGoals"].ToString(),
                    PremVideo = row["Prem Video"].ToString(),
                    PremVideoNetOff = row["Prem Video Net OFF"].ToString(),
                    PremVideoTrendPercentage = row["Prem VideoTrend %"].ToString(),
                    PremVideoTrend = row["Prem VideoTrend"].ToString(),
                    TotalGPAchievedWithSpif = row["$GP Achieved -With Spif"].ToString(),
                    GPPerBox = row["$GP per BOX"].ToString(),
                    TotalOPSTrending = row["TOTAL OPSTrending"].ToString(),
                    GrossAddsGP = row["GROSS ADDS$GP"].ToString(),
                    MTDUpgrades = row["MTD UPGRADES"].ToString(),
                    MTDUpgradesGP = row["MTD UPGRADES$GP"].ToString(),
                    BroadBandFiberGP = row["Broad Band +Fiber $GP"].ToString(),
                    BroadbandGP = row["Broadband $GP"].ToString(),
                    BroadbandLessThan300MBQISpiff = row["Broadband Lessthen (300Mb) QISpiff"].ToString(),
                    NewFiber300MBQISpiff = row["New Fiber(300 MB) QISpiff"].ToString(),
                    NewFiber500MBQISpiff = row["New Fiber(500 MB) QISpiff"].ToString(),
                    NewFiber1GQISpiff = row["New Fiber(1G) QI Spiff"].ToString(),
                    PremVideoGP = row["Prem Video $GP"].ToString(),
                    PremVideoSpiff = row["Prem Video Spiff"].ToString(),
                    AccessGP = row["Access $GP"].ToString(),
                    AccessRevenue = row["Access $Revenue"].ToString(),
                    AccessQty = row["Access Qty"].ToString(),
                    FNAchAverageQTD = row["FN AchAverage QTD"].ToString(),
                    FNAch = row["FN Ach $"].ToString(),
                    CRUAchVoice = row["CRU Ach Voice"].ToString(),
                    CRUAchAverageQTD = row["CRU Ach AverageQTD"].ToString(),
                    CRUAchVoiceDollar = row["CRU Ach Voice $"].ToString(),
                    CRUAchData = row["CRU AchDATA $"].ToString(),
                    TWDevicesQtyTrend = row["T,W,C DevicesQTY Trend"].ToString(),
                    TWDevicesGP = row["T,W,C DevicesGP"].ToString(),
                    HomeTechProtect = row["HomeTechProtect"].ToString(),
                    TotalProtectionPercentage = row["TotalProtection %"].ToString(),
                    ProtAdv1 = row["ProtAdv 1"].ToString(),
                    ProtAdv4 = row["ProtAdv 4"].ToString(),
                };
                //ViewBag.CSAT = row["CSAT"].ToString();
                //ViewBag.TotalOps = row["TOTAL OPS"].ToString();
                //ViewBag.ActualBudHRS = row["Monthly Budgeted HRS"].ToString();
                //ViewBag.Traffic = row["Total Traffic"].ToString();
                lisn_tot.Add(totalNum);
            }
            //ViewBag.Date = GpReport.DateTimes;
            ViewData["GetRecords_summaryTotal"] = lisn_tot;
            ///ViewBag.Date = GpReport.DateTimes;

            //For Last TimeStamp

            if (GetFct_Summary != null && GetFct_Summary.Rows.Count > 0)
            {
                // Use LINQ to find the latest TimeStamp
                var latestTimeStamp = GetFct_Summary.AsEnumerable()
                                                     .Max(row => row.Field<DateTime>("TimeStamp"));
                //var adjustedTimeStamp = latestTimeStamp.AddDays(-1);
                var ReportDate = GetFct_Summary.AsEnumerable()
                                                     .Max(row => row.Field<DateTime>("ReportDate"));
                // Output the result
                ViewBag.LatestTimeStamp = latestTimeStamp.ToString("yyyy-MM-dd"); // Format as needed
                                                                                  // Subtract one day from the TimeStamp

                //ViewBag.ReportDate = adjustedTimeStamp.ToString("yyyy-MM-dd");
                ViewBag.ReportDate = ReportDate.ToString("yyyy-MM-dd");
            }
            else
            {
                ViewBag.LatestTimeStamp = "No data available";
            }

            List<SelectListItem> sdList = new List<SelectListItem>();
            List<SelectListItem> marketList = new List<SelectListItem>();
            List<SelectListItem> mmarketList = new List<SelectListItem>();
            List<SelectListItem> mmmList = new List<SelectListItem>();
            List<SelectListItem> tmList = new List<SelectListItem>();
            List<SelectListItem> tiersList = new List<SelectListItem>();
            List<SelectListItem> storesList = new List<SelectListItem>();
            //List<SelectListItem> roleList = new List<SelectListItem>();

            HashSet<string> sdNamesSet = new HashSet<string>(); // To store unique region names
            HashSet<string> marketNamesSet = new HashSet<string>();
            HashSet<string> mmarketNamesSet = new HashSet<string>();
            HashSet<string> mmmNamesSet = new HashSet<string>();
            HashSet<string> tmNamesSet = new HashSet<string>();
            HashSet<string> tiersNamesSet = new HashSet<string>();
            HashSet<string> storesNamesSet = new HashSet<string>();
            //HashSet<string> roleNamesSet = new HashSet<string>();

            // Assuming your DataTable has a column named "SD" that holds the region names
            foreach (DataRow row in GetFct_Summary.Rows)
            {
                string sdName = row["SD"].ToString(); // Assuming the SD column contains SD names
                string marketName = row["Market"].ToString();
                string tmName = row["TM"].ToString();
                string tiersName = row["Tier"].ToString();
                string storName = row["Store"].ToString();
                //string roleName = row["Role"].ToString();

                sdNamesSet.Add(sdName);
                marketNamesSet.Add(marketName);
                tmNamesSet.Add(tmName);
                tiersNamesSet.Add(tiersName);
                storesNamesSet.Add(storName);
                //roleNamesSet.Add(roleName);

                // Add to the list as SelectListItem
                storesList.Add(new SelectListItem
                {
                    Value = storName,
                    Text = storName
                });

                sdList.Add(new SelectListItem
                {
                    Value = sdName,
                    Text = sdName
                });

                tiersList.Add(new SelectListItem
                {
                    Value = tiersName,
                    Text = tiersName
                });


                marketList.Add(new SelectListItem
                {
                    Value = marketName,
                    Text = marketName
                });
                //mmarketList.Add(new SelectListItem
                //{
                //    Value = mmarketName,
                //    Text = mmarketName
                //});
                //mmmList.Add(new SelectListItem
                //{
                //    Value = mmmName,
                //    Text = mmmName
                //});

                tmList.Add(new SelectListItem
                {
                    Value = tmName,
                    Text = tmName
                });

                //roleList.Add(new SelectListItem
                //{
                //    Value = roleName,
                //    Text = roleName
                //});

            }

            if (storesList != null && storesList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctStoresList = storesList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.StoresList = distinctStoresList;
            }
            else
            {
                ViewBag.StoresList = new List<SelectListItem>();
            }

            if (sdList != null && sdList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctSDList = sdList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.SDList = distinctSDList;
            }
            else
            {
                ViewBag.SDList = new List<SelectListItem>();
            }
            if (tiersList != null && tiersList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distincttiersList = tiersList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.tiersList = distincttiersList;
            }
            else
            {
                ViewBag.tiersList = new List<SelectListItem>();
            }

            if (marketList != null && marketList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctMarketList = marketList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.MarketList = distinctMarketList;
            }
            else
            {
                ViewBag.MarketList = new List<SelectListItem>();
            }
            if (mmarketList != null && mmarketList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctmMarketList = mmarketList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.mMarketList = distinctmMarketList;
            }
            else
            {
                ViewBag.mMarketList = new List<SelectListItem>();
            }
            if (mmmList != null && mmmList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctmmmList = mmmList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.mmmList = distinctmmmList;
            }
            else
            {
                ViewBag.mmmList = new List<SelectListItem>();
            }
            if (tmList != null && tmList.Count > 0)
            {
                // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
                var distinctTMList = tmList.GroupBy(x => x.Value)
                                           .Select(g => g.First()) // Or group by x.Text if needed
                                           .ToList();
                ViewBag.TMList = distinctTMList;
            }
            else
            {
                ViewBag.TMList = new List<SelectListItem>();
            }

            //if (roleList != null && roleList.Count > 0)
            //{
            //    // Ensure distinct items by grouping by the 'Value' property (or 'Text' if necessary)
            //    var distinctRoleList = roleList.GroupBy(x => x.Value)
            //                               .Select(g => g.First()) // Or group by x.Text if needed
            //                               .ToList();
            //    ViewBag.RoleList = distinctRoleList;
            //}
            //else
            //{
            //    ViewBag.RoleList = new List<SelectListItem>();
            //}
        }

        private static string MultiplyAndRoundPercentage(object value)
        {
            if (decimal.TryParse(value?.ToString(), out var decimalValue))
            {
                return Math.Round(decimalValue * 100, 2).ToString();
            }
            return "0"; // Default fallback
        }

        private static string RoundToNearestWhole(object value)
        {
            if (decimal.TryParse(value?.ToString(), out var decimalValue))
            {
                return Math.Round(decimalValue, MidpointRounding.AwayFromZero).ToString();
            }
            return "0"; // Default fallback
        }
    }
}