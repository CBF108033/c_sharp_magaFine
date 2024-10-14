using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using magaFine.Helpers;
using magaFine.Models;
using magaFine.Services;
using magaFine.SharedLibrary;

namespace magaFine.Controllers
{
    public class AuthController : Controller
    {
        private magaFineEntities db = new magaFineEntities();

        public ActionResult Login()
        {
            ViewBag.success = TempData["success"] != null ? (bool)TempData["success"] : false;
            ViewBag.msg = string.IsNullOrEmpty((string)TempData["message"]) ? "" : TempData["message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("auth/login")]
        public ActionResult Login(users usersModel)
        {
            if (IsValidUser(usersModel.username, usersModel.password))
            {
                var token = Jwt_Helper.GenerateToken(usersModel.username);

                // user資料存到session
                Session["User"] = usersModel;

                // 返回給前端，用 JSON 方式
                return Json(new { token = token, message = "Login successful" });
            }
            else
            {
                return Json(new { message = "Login failed" }); ;
            }
        }

        private bool IsValidUser(string username, string password)
        {
            // 驗證使用者邏輯(測試用)
            return (username == "fiona" && password == "123");
        }

        public ActionResult Register()
        {
            ViewBag.success = TempData["success"] == null ? false : true;
            ViewBag.msg = TempData["message"] == null ? "" : TempData["message"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(users data)
        {
            AuthService authService = new AuthService();
            OperationResult result = authService.CreateUser(data);
            TempData["success"] = result.Success;
            TempData["message"] = result.Message;
            if (result.Success)
            {
                return RedirectToAction("Login");
            }

            return View();
        }
    }
}