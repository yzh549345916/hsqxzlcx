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
    public abstract class ActionFilterAttributeBase : ActionFilterAttribute
    {
        protected static string Header = "";
        protected static string Footer = "";


        public dynamic ViewBag
        {
            get
            {
                return Controller.ViewBag;
            }
        }

        public Controller Controller { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller = filterContext.Controller as Controller;

            SetTheme();


        }

        protected void SetTheme()
        {
            var theme = "default-v2";
            var themeParam = Controller.HttpContext.Request.QueryString["theme"];
            var themeCookie = Controller.HttpContext.Request.Cookies["theme"];

            if (themeParam != null && Regex.IsMatch(themeParam, "[a-z0-9\\-]+", RegexOptions.IgnoreCase))
            {
                theme = themeParam;

                // update cookie
                HttpCookie cookie = new HttpCookie("theme");
                cookie.Value = theme;
                Controller.ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
            else if (themeCookie != null)
            {
                theme = themeCookie.Value;
            }

            var CommonFileCookie = Controller.HttpContext.Request.Cookies["commonFile"];

            ViewBag.Theme = theme;
            ViewBag.CommonFile = CommonFileCookie == null ? "common-empty" : CommonFileCookie.Value;
        }




    }
}