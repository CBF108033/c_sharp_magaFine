using magaFine.Models;
using magaFine.SharedLibrary;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace magaFine.Services
{
    public class AuthService : BaseLibrary
    {
        private magaFineEntities db = new magaFineEntities();

        /**
         * 新增文章
        * @param articles createData 新增的文章資料
         */
        public OperationResult CreateUser(users data)
        {
            try
            {
                data.createdAt = DateTime.Now;
                data.updatedAt = DateTime.Now;
                db.users.Add(data);
                db.SaveChanges();

                return StatusMessage(true, "User created successfully!");
            }
            catch (Exception ex)
            {
                return StatusMessage(false, ex.Message);
            }
        }
    }
}