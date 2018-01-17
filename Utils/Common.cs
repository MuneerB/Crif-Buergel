using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Umbraco.Core.Models;
using System.Web.Mvc;

namespace CrifAustria.Utils
{
    public static class Common
    {
        public static string GetSiteDomainName()
        {
            var req = HttpContext.Current.Request;
            return req.Url.Host + (req.Url.IsDefaultPort ? "" : ":" + req.Url.Port);
        }
        public static string GetDomainUrl(int pageId, int defaultPort)
        {
            try
            {
                var domains = umbraco.library.GetCurrentDomains(pageId);
                string url = "";
                if (domains != null)
                {
                    foreach (var domain in domains)
                    {
                        if (domain.Name.Contains("https://"))
                        {
                            Uri uri = new Uri(domain.Name);
                            url = uri.Scheme + System.Uri.SchemeDelimiter + uri.Host + (uri.Port != defaultPort ? ":" + uri.Port : "");
                        }
                        else
                        {
                            Uri uri = new Uri(umbraco.library.NiceUrlWithDomain(pageId).ToString());
                            url = uri.Scheme + System.Uri.SchemeDelimiter + uri.Host + (uri.Port != defaultPort ? ":" + uri.Port : "");
                        }
                    }
                }
                else
                {
                    Uri uri = new Uri(umbraco.library.NiceUrlWithDomain(pageId).ToString());
                    url = uri.Scheme + System.Uri.SchemeDelimiter + uri.Host + (uri.Port != defaultPort ? ":" + uri.Port : "");
                }
                return url;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
        public static string GetImageAsBackGround(IPublishedContent image)
        {
            string style = "";
            if (image != null)
            {
                string backgroundImageURL = image.Url;
                if (HttpContext.Current.Request.Browser.IsMobileDevice)
                {
                    backgroundImageURL = image.Url + string.Format("?width={0} & height={1}&mode=crop", 1100, 372);
                }
                style = string.Format("style=\"background-image : url('{0}')\"", backgroundImageURL);
            }
            return style;
        }
        public static string TruncateAtWord(this string input, int length, string trailingString = "...")
        {
            if (input == null || input.Length < length)
                return input;
            int iNextSpace = input.LastIndexOf(" ", length);
            return string.Format("{0}" + trailingString + "", input.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim());
        }
        public static string ToStringWithOrdinal(this DateTime d)
        {
            var sb = new StringBuilder(d.Day);
            switch (d.Day)
            {
                case 1:
                case 21:
                case 31:
                    return d.ToString("MMMM d'st', yyyy");
                case 2:
                case 22:
                    return d.ToString("MMMM d'nd', yyyy");
                case 3:
                case 23:
                    return d.ToString("MMMM d'rd', yyyy");
                default:
                    return d.ToString("MMMM d'th', yyyy");
            }
        }
        public static string getPredefinedColorSetClass(string colorString, bool isBody = false)
        {
            switch (colorString)
            {
                case "crif blue":
                    return "crif-blue";
                case "crif orange":
                    return "crif-orange";
                case "dark gray":
                    if (isBody)
                        return "dark-gray-bdy";
                    else
                        return "dark-gray-tit";
                default:
                    return "crif-white";
            }
        }
        public static IEnumerable<SelectListItem> GetIndustries(IEnumerable<IPublishedContent> IndustryNodes)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (IPublishedContent node in IndustryNodes)
            {
                SelectListItem listitem = new SelectListItem() { Text = node.Name.ToSafeString(), Value = node.Id.ToSafeString() };
                list.Add(listitem);
            }
            return list;
        }
        public static string GetHyperLink(string pageUrl, string pageName)
        {
            if (!string.IsNullOrEmpty(pageUrl))
            {
                if (string.IsNullOrWhiteSpace(pageName))
                {
                    pageName = pageUrl;
                }
                return string.Format("<a href=\"{0}\">{1}</a>", pageUrl, pageName);
            }
            return "";
        }
        public static IPublishedContent getTranslatedPage(IPublishedContent page)
        {
            var Translated_Page = page;
            var request = HttpContext.Current.Request;
            if (request.RawUrl.ToLower().Contains("/en/"))
            {
                var Translation_Type = page.DocumentTypeAlias + "Translation";
                var Translation_Folder_Type = page.DocumentTypeAlias + "TranslationFolder";
                var Translation_Folder_Page = page.Children.Where(x => x.DocumentTypeAlias == Translation_Folder_Type).FirstOrDefault();
                if(Translation_Folder_Page!=null)
                {
                    Translated_Page = Translation_Folder_Page.Children.Where(x => x.DocumentTypeAlias == Translation_Type).FirstOrDefault();
                }
            }
            return Translated_Page;
        }
        public static string getTranslatedUrl(string RawUrl)
        {
            var TranslatedUrl = RawUrl;
            if (RawUrl.ToLower().Contains("/translations/en/"))
            {
                int startIndex = RawUrl.IndexOf("/translations/en/");
                RawUrl = RawUrl.Remove(startIndex);
                if(string.IsNullOrEmpty(RawUrl))
                {
                    TranslatedUrl = "/en/";
                }
                else
                {
                    TranslatedUrl = "/en" + RawUrl;
                }
            }
            return TranslatedUrl;
        }
    }
}