using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp3.Models;

namespace TelerikMvcApp3.Controllers
{
    public class 卡片Controller : Controller
    {
        // GET: 卡片

        public ActionResult 获取分钟降水量()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Pre_Minute");
            return Json(result);
        }
        public ActionResult 获取分钟温度()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Tem_Minute");

            return Json(result);
        }
        public ActionResult 获取分钟实况其他资料()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Other_Minute");
            return Json(result);
        }
        public ActionResult 获取分钟气压()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_PRS_Minute");

            return Json(result);
        }
        public ActionResult 获取分钟相对湿度()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_RHU_Minute");
            return Json(result);
        }
        public ActionResult 获取分钟风()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Wind_Minute");

            return Json(result);
        }
        public ActionResult 获取小时降水量()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Pre_Hour");
            return Json(result);
        }
        public ActionResult 获取小时温度()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Tem_Hour");

            return Json(result);
        }
        public ActionResult 获取小时实况其他资料()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Other_Hour");
            return Json(result);
        }
        public ActionResult 获取小时气压()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_PRS_Hour");

            return Json(result);
        }
        public ActionResult 获取小时相对湿度()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_RHU_Hour");
            return Json(result);
        }
        public ActionResult 获取小时风()
        {
            List<SKViewModel> result = getdateDefault(DateTime.Now.AddDays(-3), DateTime.Now, "SK_Wind_Hour");

            return Json(result);
        }
        public List<SKViewModel> getdateDefault(DateTime sdate, DateTime edate, string datazl)
        {

            数据库处理 sjkcl = new 数据库处理();
            List<资料种类> dataLists = sjkcl.获取数据库表信息();
            List<SKViewModel> result = new List<SKViewModel>();
            result = sjkcl.获取指定时间资料名统计信息(sdate, edate, datazl);
            for (int i = 0; i < result.Count; i++)
            {
                资料种类 item = dataLists.First(y => y.数据库表名称 == result[i].名称);
                if (item != null)
                {
                    result[i].名称 = item.表名称;

                }
            }

            return result;
        }
    }
}