using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using magaFine.Models;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;
using magaFine.Helpers;

namespace magaFine.Controllers
{
    public class HomeController : Controller
    {
        magaFineEntities db = new magaFineEntities();

        public ActionResult Index()
        {
            var articles = db.articles.Where(m => m.disploy == 1 && m.readAccess != "admin").ToList();
            foreach (articles article in articles)
            {
                article.content = stripHtmlUsingHtmlAgilityPack(article.content);
                //內文移除空白
                article.content = Regex.Replace(article.content, @"\s+", " ").Trim();
            }

            var userData = Session_Helper.GetUserData();
            var user = userData!=null ? userData as users : null;

            ViewBag.userName = user != null ? user.username : null;

            return View(articles);
        }

        /**
         * 去除 HTML 標籤
         */
        public static string stripHtmlUsingHtmlAgilityPack(string input)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(input);
            var editContent = doc.DocumentNode.InnerText;
            editContent = HtmlEntity.DeEntitize(editContent);
            return editContent; 
        }
    }
}