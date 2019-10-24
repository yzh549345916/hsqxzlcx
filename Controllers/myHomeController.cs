using Kendo.Mvc.Examples.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IOFile = System.IO.File;

namespace TelerikMvcApp3.Controllers
{

    public class myHomeController : Controller
    {
        
        public Controller Controller { get; set; }
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
        [myHome]
        public ActionResult 首页()
        {
            ViewBag.Message = "Your app description page.";
           
            return View();
        }
        public ActionResult MyCustom()
        {
            return PartialView("Index");
        }
        
    }
}
