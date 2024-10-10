using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using magaFine.Helpers;
using magaFine.Models;

namespace magaFine.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[Route("auth/login")]
        public ActionResult Login(users usersModel)
        {
            if (IsValidUser(usersModel.username, usersModel.password))
            {
                var token = Jwt_Helper.GenerateToken(usersModel.username);
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
    }
}