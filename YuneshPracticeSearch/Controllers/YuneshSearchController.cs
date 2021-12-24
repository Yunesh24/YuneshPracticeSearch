using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YuneshPracticeSearch.Models;
namespace YuneshPracticeSearch.Controllers
{
    public class YuneshSearchController : Controller
    {
        // GET: YuneshSearch
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SaveCustomer(YuneshSearchModel model)
        {
            try
            {

                return Json(new { MSG = (new YuneshSearchModel()).SaveCustomer(model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SearchCustomer(string Prefix)
        {

            try
            {

                return Json(new YuneshSearchModel().SearchCustomer(Prefix), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

   