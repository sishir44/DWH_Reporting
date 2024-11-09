using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DWH_Reporting.Models.GpReport;
using DWH_Reporting.Models;
using DWH_Reporting.Helpers;

namespace DWH_Reporting.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //[Authorization]
        public ActionResult StoreNo()
        {
            try
            {
                //string user;
                ////user = Request.QueryString["userid"];
                //user = Session["userid"].ToString();


            }
            catch (Exception ex)
            {

                return Content("Something Went Wrong.!! Please Contact MIS Department " + ex.Message);
            }

            return View();

        }

        //[Authorization]
        public ActionResult EmpNo()
        {
            try
            {
                //string user;
                ////user = Request.QueryString["userid"];
                //user = Session["userid"].ToString();


            }
            catch (Exception ex)
            {

                return Content("Something Went Wrong.!! Please Contact MIS Department " + ex.Message);
            }


            return View();

        }

        //[Authorization]
        public ActionResult StoreNumber(int? selectedMonth)
        {
            //if (selectedMonth == null)
            //{
            selectedMonth = 9;
            //}
            // Create a list of SelectListItem for months
            var months = new List<SelectListItem>
            {
                new SelectListItem { Text = "JAN", Value = "1" },
                new SelectListItem { Text = "FEB", Value = "2" },
                new SelectListItem { Text = "MAR", Value = "3" },
                new SelectListItem { Text = "APR", Value = "4" },
                new SelectListItem { Text = "MAY", Value = "5" },
                new SelectListItem { Text = "JUN", Value = "6" },
                new SelectListItem { Text = "JUL", Value = "7" },
                new SelectListItem { Text = "AUG", Value = "8" },
                new SelectListItem { Text = "SEP", Value = "9" },
                new SelectListItem { Text = "OCT", Value = "10" },
                new SelectListItem { Text = "NOV", Value = "11" },
                new SelectListItem { Text = "DEC", Value = "12" }
            };

            // Pass the list to the view via ViewBag
            ViewBag.Months = months;

            if (selectedMonth.HasValue)
            {
                // Construct the date parameter (e.g., "2024-09-01" for September)
                string dateParam = $"2024-{selectedMonth.Value.ToString("D2")}-01";

                // Call your function with the date parameter
                DataTable Fct_StoreNumber = GP_DAL_Functions.GetFct_StoreNumberCol2(dateParam);

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
                        TM = row["TM"].ToString(),
                        RSMSRSM = row["RSM/SRSM"].ToString(),
                        Role = row["Role"].ToString(),
                        MonthlyAchievedHoursTrendingPercentage = row["Monthly _Achived Hours Trending %"].ToString(),
                        GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                        GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                        GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
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
                        ChargeBack6 = row["Charge Back6"].ToString(),
                        AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
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
                        TimeStamp = row["TimeStamp"].ToString()

                    };

                    // Add the populated model to the list
                    lisn2_lst.Add(model);
                }
                ViewBag.Date = GpReport.DateTimes;
                ViewData["Fct_StoreNumber"] = lisn2_lst;


                ////////////////Get Total
                ///
                DataTable Fct_StoreNumberTotal = GP_DAL_Functions.GetFct_StoreNumberTotal();
                List<Fct_StoreNumberAttributesModel> lisn_tot = new List<Fct_StoreNumberAttributesModel>();

                foreach (DataRow row in Fct_StoreNumberTotal.Rows)
                {
                    Fct_StoreNumberAttributesModel model3 = new Fct_StoreNumberAttributesModel
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
                        TM = row["TM"].ToString(),
                        RSMSRSM = row["RSM/SRSM"].ToString(),
                        Role = row["Role"].ToString(),
                        MonthlyAchievedHoursTrendingPercentage = row["Monthly _Achived Hours Trending %"].ToString(),
                        GrossAddsTrendToGoal = row["GROSS ADDS Trend% To Goal"].ToString(),
                        GrossAddsGoals = row["GROSS ADDS Goals"].ToString(),
                        GrossAddsNetOFF = row["GROSS ADDS Net OFF"].ToString(),
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
                        ChargeBack6 = row["Charge Back6"].ToString(),
                        AIAInternetNetOff = row["AIA Internet Net Off"].ToString(),
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
                        TimeStamp = row["TimeStamp"].ToString()

                    };

                    // Add the populated model to the list
                    lisn_tot.Add(model3);
                }
                ViewBag.Date = GpReport.DateTimes;

                ViewData["Fct_StoreNumberTotal"] = lisn_tot;

                if (selectedMonth != 9)
                {
                    ViewData["Fct_StoreNumberTotal"] = null;
                }
                ViewData["Day"] = "17" + "th";
                ViewData["Mon"] = "SEP";

                return View();

            }
            else
            {
                // Handle the case where no month is selected
                ViewBag.ErrorMessage = "No month selected.";
            }

            // Return the view with the data or an error message
            return View();
        }

    }
}