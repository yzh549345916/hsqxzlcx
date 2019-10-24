﻿using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Kendo.Models;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using Kendo.Mvc.Examples.Models;

namespace TelerikMvcApp3
{
    public static class HtmlExtensions
    {
        public static IHtmlString ExampleLink(this HtmlHelper html, NavigationExample example)
        {
            var href = html.ExampleUrl(example);

            var className = "";

            if (example.New)
            {
                className += "new-example";
            }

            if (example.Updated)
            {
                className += "updated-example";
            }

            var routeData = html.ViewContext.RouteData;
            var currentAction = routeData.Values["action"].ToString().ToLower().Replace("_", "-");
            var currentController = routeData.Values["controller"].ToString().ToLower().Replace("_", "-");

            if (href.EndsWith(currentController + "/" + currentAction))
            {
                className += " active";
            }

            StringBuilder link = new StringBuilder();

            link.Append("<a ");

            if (!String.IsNullOrEmpty(className))
            {
                link.Append("class=\"" + className + "\" ");
            }

            link.Append("href=\"" + href + "\">");

            if (example.New)
            {
                link.Append("<span class=\"new-widget\"></span>");
            }

            if (example.Updated)
            {
                link.Append("<span class=\"updated-widget\"></span>");
            }

            link.Append(example.Text).Append("</a>");

            return html.Raw(link.ToString());
        }

        public static string ExampleUrl(this HtmlHelper html, NavigationExample example)
        {
            var sectionAndExample = example.Url.Split('/');

            return new UrlHelper(html.ViewContext.RequestContext).ExampleUrl(sectionAndExample[0], sectionAndExample[1]);
        }

        public static string ExampleUrl(this HtmlHelper html, NavigationExample example, string product)
        {
            var sectionAndExample = example.Url.Split('/');

            var url = string.Join("/", LiveSamplesRoot, product, sectionAndExample[0], sectionAndExample[1]);

            return url;
        }

        public static string ProductExampleUrl(this HtmlHelper html, NavigationExample example, string product)
        {
            var sectionAndExample = example.Url.Split('/');

            var url = string.Join("/", LiveSamplesRoot, product, sectionAndExample[0]);

            return url;
        }

        public static string LiveSamplesRoot
        {
            get
            {
                return "http://demos.telerik.com";
            }
        }

        public static IHtmlString WidgetLink(this HtmlHelper html, NavigationWidget widget, string product)
        {
            var viewBag = html.ViewContext.Controller.ViewBag;

            var href = html.ExampleUrl(widget.Items[0]);

            var text = widget.Text;

            StringBuilder link = new StringBuilder();

            link.AppendFormat("<a href=\"{0}\">", href);
            link.Append(text);

            if (widget.Beta)
            {
                link.Append("<span class=\"beta-widget\"></span>");
            }

            if (widget.New)
            {
                link.Append("<span class=\"new-widget\"></span>");
            }

            if (widget.Updated)
            {
                link.Append("<span class=\"updated-widget\"></span>");
            }

            link.Append("</a>");

            return html.Raw(link.ToString());
        }

        public static string StyleRel(this HtmlHelper html, string styleName)
        {
            if (styleName.ToLowerInvariant().EndsWith("less"))
            {
                return "stylesheet/less";
            }

            return "stylesheet";
        }

        public static IHtmlString StyleLink(this HtmlHelper html, string styleName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var url = urlHelper.Style(styleName);
            return html.Raw("<link href=\"" + url + "\" rel=\"" + html.StyleRel(styleName) + "\" />");
        }

        public static IHtmlString StyleLink(this HtmlHelper html, string styleName, string theme, string common)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var disabled = "";
            if (common == "common-empty" && (
                  styleName.Contains("kendo.rtl") ||
                  styleName.Contains("CURRENT_COMMON") ||
                  styleName.Contains("CURRENT_THEME.mobile")))
            {
                disabled = "-disabled";
            }
            var url = urlHelper.Style(styleName, theme, common);
            return html.Raw("<link href=\"" + url + "\" rel=\"" + html.StyleRel(styleName) + disabled + "\" />");
        }
    }
    public static class UrlExtensions
    {
        public static string Widget(this UrlHelper url, string widget)
        {
            return url.ExampleUrl(widget, "index");
        }

        public static string ExampleUrl(this UrlHelper url, string widget, string example)
        {
            var widgetUrl = url.Action(example, widget);

            if (example == "index" && !widgetUrl.EndsWith("index"))
            {
                widgetUrl += "/index";
            }

            return widgetUrl;
        }

        public static string Script(this UrlHelper url, string file)
        {
            return ResourceUrl(url, "js", file, IsAbsoluteUrl(file));
        }

        public static string Style(this UrlHelper url, string file, string theme, string commonFile)
        {
            return url.Style(file)
                .Replace("CURRENT_THEME", theme)
                .Replace("CURRENT_COMMON", commonFile);
        }

        public static string Style(this UrlHelper url, string file)
        {
            return ResourceUrl(url, "styles", file, IsAbsoluteUrl(file));
        }

        private static string ResourceUrl(UrlHelper url, string assetType, string file, bool isAbsoluteUrl)
        {
            if (assetType == "styles")
            {
                return url.Content(string.Format("~/content/kendo/2019.3.917/{0}", file));
            }

            return url.Content(string.Format("~/Scripts/kendo/2019.3.917/{0}", file));

        }

        public static bool IsAbsoluteUrl(string url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }
    }
}
