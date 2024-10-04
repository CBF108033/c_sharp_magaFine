using HtmlAgilityPack;
using magaFine.Models;
using magaFine.SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace magaFine.Services
{
    public class ArticleService : BaseLibrary
    {
        private magaFineEntities db = new magaFineEntities();
        /**
         * 取得全部文章
         * @param Boolean isTagContent 內文是否移除Html標記
         */
        public List<articles> GetAllArticles(Boolean isTagContent = false)
        {
            var res = db.articles.ToList();
            if (isTagContent)
            {
                foreach (articles article in res)
                {
                    article.content = article.content != null ? stripHtmlUsingHtmlAgilityPack(article.content) : "";
                    article.content = Regex.Replace(article.content, @"\s+|&nbsp;", "").Trim();
                }
            }

            return res;
        }

        /**
         * 取得文章單一文章
         */
        public articles GetArticlesById(int id)
        {
            return db.articles.FirstOrDefault(m => m.Id == id);
        }

        /**
         * 新增文章
        * @param articles createData 新增的文章資料
         */
        public OperationResult CreateArticle(articles createData)
        {
            try
            {
                createData.createdAt = DateTime.Now;
                createData.updatedAt= DateTime.Now;
                db.articles.Add(createData);
                db.SaveChanges();

                return StatusMessage(true, "Article created successfully!");
            }
            catch (Exception ex)
            {
                return StatusMessage(false, ex.Message);
            }
        }

        /**
        * 更新文章
        * @param int id 更新文章Id
        * @param articles updateData 更新資料
        * @param string action 儲存動作(更新不發布/發布)
        */
        public OperationResult UpdateArticle(int id, articles updateData, string action)
        {
            try
            {
                articles article = db.articles.Where(m => m.Id == id).FirstOrDefault();

                //switch (action)
                //{
                //    case "save":
                //        break;
                //    case "publish":
                //        break;
                //    default:
                //        break;
                //}
                db.Entry(article).CurrentValues.SetValues(updateData);
                updateData.updatedAt = DateTime.Now;
                db.SaveChanges();

                return StatusMessage(true, "Article updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusMessage(false, ex.Message);
            }
        }

        /**
        * 更新文章是否發布
        * @param int id 更新文章Id
        * @param int disploy 是否發布
        */
        public OperationResult UpdateArticleDisploy(int id, int disploy)
        {
            try
            {
                articles article = db.articles.Where(m => m.Id == id).FirstOrDefault();

                article.disploy = disploy;
                article.updatedAt = DateTime.Now;
                db.SaveChanges();

                return StatusMessage(true, "Article updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusMessage(false, ex.Message);
            }
        }

        /**
         * 刪除單一文章
         * @param int id 刪除的Id
         */
        public OperationResult DeleteArticleById(int id)
        {
            var article = db.articles.FirstOrDefault(m => m.Id == id);
            if (article != null)
            {
                try
                {
                    db.articles.Remove(article);
                    db.SaveChanges();

                    return StatusMessage(true, "Article deleted successfully!");
                }
                catch (Exception ex)
                {
                    return StatusMessage(false, ex.Message);
                }
            }

            return StatusMessage(false, "No This Article!");
        }

        /**
        * 去除 HTML 標籤
        */
        public string stripHtmlUsingHtmlAgilityPack(string input)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(input);
            var editContent = doc.DocumentNode.InnerText;
            editContent = HtmlEntity.DeEntitize(editContent);
            return editContent;
        }
    }
}