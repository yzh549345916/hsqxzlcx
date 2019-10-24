using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcApp3.Models;

namespace TelerikMvcApp3.Controllers
{
    public class RKTJController : Controller
    {
        // GET: rkGrid
        public ActionResult rkGrid()
        {
            return View();
        }
        public ActionResult Orders_Read2(string[] sdatepic, string[] edatepic, string[] datazl, [DataSourceRequest]DataSourceRequest request)
        {

            数据库处理 sjkcl = new 数据库处理();
            List<资料种类> dataLists = dataLists = sjkcl.获取数据库表信息();
            List<SKViewModel> result = new List<SKViewModel>();
            if (datazl == null || datazl.Length == 0)
            {
                result = sjkcl.获取指定时间统计信息(Convert.ToDateTime(sdatepic[0]), Convert.ToDateTime(edatepic[0]));
            }
            else
            {
                bool bs = false;
                foreach (string item in datazl)
                {
                    if (item.ToLower() == "all")
                        bs = true;
                }
                if (bs)
                {
                    result = sjkcl.获取指定时间统计信息(Convert.ToDateTime(sdatepic[0]), Convert.ToDateTime(edatepic[0]));
                }
                else
                {
                    string filter = "";
                    foreach (string item in datazl)
                        filter += $"\'{item}\',";
                    filter = filter.Substring(0, filter.Length - 1);
                    result = sjkcl.获取指定时间统计信息(Convert.ToDateTime(sdatepic[0]), Convert.ToDateTime(edatepic[0]), filter);
                }
            }
            for (int i = 0; i < result.Count; i++)
            {
                资料种类 item = dataLists.First(y => y.数据库表名称 == result[i].名称);
                if (item != null)
                {
                    result[i].名称 = item.表名称;
                }
            }
            return Json(result.ToDataSourceResult(request));
        }
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, "我的表格");
        }

        public ActionResult GetCustomers(string text)
        {

            数据库处理 sjkcl = new 数据库处理();
            List<资料种类> result = sjkcl.获取数据库表信息();
            result.Add(new 资料种类(0, "所有", "all"));
            var myresult = result.Where(y => y.表名称.Contains(text)).OrderBy(y => y.序号).ToList();
            return Json(myresult, JsonRequestBehavior.AllowGet);
        }
        public ActionResult MyCustom()
        {
            return PartialView("rkGrid");
        }
        public ActionResult MyCustoms(string dataName)
        {
            if (dataName == "表格")
                return PartialView("rkGrid");
            return PartialView("Index");
        }
    }
}