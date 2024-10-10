using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web.Mvc;
using System.Configuration;

namespace magaFine.Attributes
{
    public class JwtAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // 1. 取得header Authorization
            var token = filterContext.HttpContext.Request.Headers["Authorization"];

            // 2. 沒token回傳401
            if (string.IsNullOrEmpty(token))
            {
                filterContext.Result = new JsonResult
                {
                    Data = new { message = "請重新登入" }
                };
                //filterContext.Result = new HttpStatusCodeResult(401); // 未經授權，直接輸出401錯誤
                filterContext.HttpContext.Response.StatusCode = 401; //未經授權，錯誤訊息由前端顯示
                return;
            }

            // 3. 解析token
            try
            {
                var secretKey = Encoding.ASCII.GetBytes(ConfigurationManager.AppSettings["JWT_SECRET_KEY"]);
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateIssuer = false, 
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero  // 移除默認的 5 分鐘時間差
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token.Replace("Bearer ", ""), validationParameters, out validatedToken);

                // 4. 設置用戶身份，讓接下來的流程可以獲取身份(controller直接使用User.identity.name)
                filterContext.HttpContext.User = principal;
            }
            catch (Exception)
            {
                //filterContext.Result = new HttpStatusCodeResult(401); // 未經授權，直接輸出401錯誤
                filterContext.Result = new JsonResult
                {
                    Data = new { message = "請重新登入" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.HttpContext.Response.StatusCode = 401; //未經授權，錯誤訊息由前端顯示
            }
        }
    }
}