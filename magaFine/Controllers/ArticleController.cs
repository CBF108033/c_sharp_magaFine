using HtmlAgilityPack;
using magaFine.Models;
using magaFine.Services;
using magaFine.SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magaFine.Controllers
{
    public class ArticleController : Controller
    {
        magaFineEntities db = new magaFineEntities();
        private ArticleService articleServices = new ArticleService();

        [HttpGet]
        public ActionResult index(string id)
        {
            Boolean isValidId = int.TryParse(id, out int validId);
            articles article = null;
            string[] hashtags = new string[0];
            if (isValidId)
            {
                article = articleServices.GetArticlesById(validId);
                if (article != null)
                {
                    if (!string.IsNullOrEmpty(article.hashtags))
                    {
                        hashtags = article.hashtags.Split(',');
                    }
                }
            }
            else
            {
                ViewBag.msg = "ERROR: Wrong Id";
            }
            ViewBag.hashtags = hashtags;

            return View(article);
        }

        public ActionResult Edit(string id)
        {
            articles article = null;
            string[] hashtags = new string[0];
            Boolean isValidId = int.TryParse(id, out int validId);
            if (isValidId)
            {
                article = articleServices.GetArticlesById(validId);
                if (article != null)
                {
                    article.content = articleServices.stripHtmlUsingHtmlAgilityPack(article.content);
                    if (!string.IsNullOrEmpty(article.hashtags))
                    {
                        hashtags = article.hashtags.Split(',');
                    }
                }
            }
            else
            {
                ViewBag.msg = "";
            }

            ViewBag.hashtags = hashtags;
            ViewBag.success = TempData["success"] == null ? false : true;
            ViewBag.msg = TempData["message"] == null ? "" : TempData["message"];
            return View(article);
        }

        [HttpPost]
        public ActionResult Edit()
        {
            articles article = new articles();
            string action = "save";

            int id = int.Parse(Request.Form["id"]);
            foreach (string key in Request.Form.AllKeys)
            {
                var property = article.GetType().GetProperty(key);
                switch (key)
                {
                    case "content":
                        property.SetValue(article, Request.Unvalidated[key]);
                        break;
                    case "action":
                        action = Request.Form["action"];
                        break;
                    default:
                        if(property != null && property.CanWrite)
                        {
                            if(property.PropertyType == typeof(int))
                            {
                                if (int.TryParse(Request.Form[key], out int intValue))
                                {
                                    property.SetValue(article, intValue); 
                                }
                            }else if(property.PropertyType == typeof(string))
                            {
                                property.SetValue(article, Request.Form[key]);
                            }
                        }
                        break;
                }
            }

            OperationResult result = articleServices.UpdateArticle(id, article, action);
            TempData["success"] = result.Success;
            TempData["message"] = result.Message;

            return Json(new { success = result.Success, msg = result.Message });
        }

        [HttpPost]
        public ActionResult UpdateDisploy()
        {
            int id = int.Parse(Request.Form["Id"]);
            int disploy = int.Parse(Request.Form["disploy"]);
            OperationResult result = articleServices.UpdateArticleDisploy(id, disploy);

            return Json(new { success = result.Success, msg = result.Message });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(articles model)
        {
            articles article = new articles();
            article.title = Request.Form["title"];
            article.type = Request.Form["type"];
            article.category = Request.Form["category"];
            article.hashtags = Request.Form["hashtags"];
            article.cover = Request.Form["cover"];
            article.content = Request.Unvalidated["content"]; //跳過驗證機制1.[ValidateInput(false)]2.[AllowHtml]3.Request.Unvalidated

            switch (Request.Form["action"])
            {
                case "save":
                    article.disploy = 0;
                    break;
                case "publish":
                    article.disploy = 1;
                    break;
            }
            OperationResult result = articleServices.CreateArticle(article);
            TempData["success"] = result.Success;
            TempData["message"] = result.Message;

            return Json(new { success = result.Success, msg = result.Message });
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            int deleteId = int.Parse(id);
            OperationResult result = articleServices.DeleteArticleById(deleteId);
            TempData["success"] = result.Success;
            TempData["message"] = result.Message;

            return Json(new { success = result.Success, msg = result.Message });
        }
    }
}