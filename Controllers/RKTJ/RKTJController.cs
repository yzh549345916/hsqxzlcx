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
        public ActionResult 折线图()
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
        public ActionResult Orders_Read3(string[] sdatepic, string[] edatepic, string[] datazl)
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
           
            return Json(result);
        }
        public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, "我的表格");
        }
        public ActionResult 备用(string[] sdatepic, string[] edatepic, string[] datazl, [DataSourceRequest]DataSourceRequest request)
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
                    if (result[i].名称.Contains("分钟"))
                    {
                        result[i].种类 = "分钟";
                    }
                    else if (result[i].名称.Contains("小时"))
                    {
                        result[i].种类 = "小时";
                    }
                    else
                        result[i].种类 = "其他";
                }
            }
            List<SKViewModel> result2 = new List<SKViewModel>();
            foreach (var item in result)
            {
                string name = item.名称.Replace("小时", "").Replace("分钟", "");
                if (!result2.Exists(y => y.名称 == name && y.时间 == item.时间))
                {
                    result2.Add(new SKViewModel()
                    {
                        名称 = name,
                        时间 = item.时间
                    }); ;

                }
                if (item.种类 == "分钟")
                {
                    result2.First(y => y.名称 == name && y.时间 == item.时间).分钟个数 = item.统计个数;

                }
                else
                    result2.First(y => y.名称 == name && y.时间 == item.时间).小时个数 = item.统计个数;

            }
            //result = result.Where(y => y.名称.Contains("小时")).ToList();
            return Json(result2);
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
            return PartialView("折线图");
        }
        public ActionResult MyCustoms(string dataName)
        {
            if (dataName == "表格")
                return PartialView("rkGrid");
            else if (dataName == "图表")
                return PartialView("折线图");
            else if (dataName == "股票图")
                return PartialView("股票图");
            return PartialView("Index");
        }
        [HttpPost]
        public ActionResult Export_Save(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}