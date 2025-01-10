using DWH_Reporting.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DWH_Reporting.Controllers
{
    public class MobilyMtdUploaderController : Controller
    {
        // GET: MobilyMtdUploader
        public ActionResult StoreNumberUploader(string selectedDate, string uploadType)
        {
            try
            {
                StoreNumberUpload(selectedDate, uploadType);
                return View();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return Content("Report is being uploaded. Please try again in few minutes");
            }

        }
        [HttpPost]
        private void StoreNumberUpload(string selectedDate, string uploadType)
        {
            if (uploadType == "Upload KPI")
            {
                if (selectedDate != null)
                {
                    string dateParam = selectedDate;

                    DataTable StoreNumberUploaded = GP_DAL_Functions.StoreNumberUploaded(selectedDate);

                    bool containsTrue = false;

                    if (StoreNumberUploaded != null && StoreNumberUploaded.Rows.Count > 0)
                    {
                        containsTrue = true;

                        //foreach (DataRow row in StoreNumberUploaded.Rows)
                        //{
                        //    if (row["Result"] != DBNull.Value && Convert.ToBoolean(row["Result"]))
                        //    {
                        //        containsTrue = true;
                        //        break;
                        //    }
                        //}

                    }

                    if (containsTrue)
                    {
                        ViewData["StoreNumberUploaderKPI"] = "KPI Uploaded Successfully!";
                    }
                    else
                    {
                        ViewData["StoreNumberUploaderKPI"] = "KPI Upload Failed.";
                    }
                }
            }
            else if (uploadType == "Upload Charge Back")
            {
                DataTable StoreNumberUploadedCB = GP_DAL_Functions.StoreNumberUploaded(selectedDate);

                bool CBcontainsTrue = false;

                if (StoreNumberUploadedCB != null && StoreNumberUploadedCB.Rows.Count > 0)
                {
                    CBcontainsTrue = true;

                    //foreach (DataRow row in StoreNumberUploadedCB.Rows)
                    //{
                    //    if (row["Result"] != DBNull.Value && Convert.ToBoolean(row["Result"]))
                    //    {
                    //        CBcontainsTrue = true;
                    //        break;
                    //    }
                    //}

                }

                if (CBcontainsTrue)
                {
                    ViewData["StoreNumberUploaderCB"] = "Charge Back Uploaded Successfully!";
                }
                else
                {
                    ViewData["StoreNumberUploaderCB"] = "Charge Back Upload Failed.";
                }
            }
        }
    }
}