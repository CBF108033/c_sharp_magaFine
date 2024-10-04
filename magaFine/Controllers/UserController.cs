using magaFine.Models;
using magaFine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace magaFine.Controllers
{
    public class UserController : Controller
    {
        ArticleService articleServices = new ArticleService();

        public ActionResult index()
        {
            return View();
        }
        public ActionResult home()
        {
            List<articles> allArticles = articleServices.GetAllArticles(true);
            ViewBag.success = TempData["success"] == null ? false : true;
            ViewBag.msg = TempData["message"] == null ? "" : TempData["message"];

            return View(allArticles);
        }

    }
}