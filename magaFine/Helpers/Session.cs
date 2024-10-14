using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;

namespace magaFine.Helpers
{
    public class Session_Helper
    {

        /**
         * 生成token
         * @param string username 使用者名稱
         * @param int expireMinutes 有效時間
         */
        private static object GetSessionValue(string key)
        {
            return HttpContext.Current.Session[key];
        }

        /**
         * 取得使用者登入資料
         */
        public static object GetUserData()
        {
            if (GetSessionValue("User") != null)
            {
            return GetSessionValue("User");
            }
            else
            {
                return null;
            }
        }
    }
}