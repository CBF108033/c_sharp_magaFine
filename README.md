# C#版本的MagaFine網站

由MagaFine的原版本[(node.js版本)](https://github.com/CBF108033/MagaFine_Web)翻製成C#版本的網頁，該版本僅重製主要的CRUD功能。

## 技術
以ASP.NET MVC framework建立網頁，搭配MSSQL(Azure雲端資料庫)作為DBMS，並加入JWT驗證以及基本的資安防禦。</br>
＊另有搭配MySQL資料庫的版本[(branch - mysql_version)](https://github.com/CBF108033/c_sharp_magaFine/tree/mysql_version)

- ### **ASP.NET MVC Framework**
   ASP.NET MVC 是微軟基於MVC設計模式的Web框架，將應用分為模型、視圖、控制器三部分，提升了程式碼的可維護性、可測試性，適合構建動態Web應用。

- ### **MSSQL**
  MSSQL（Microsoft SQL Server）是微軟推出的關聯式資料庫管理系統，提供資料存儲、查詢、分析和管理功能，適用於企業級應用。

- ### **資安防禦**
   - CSRF：使用AntiForgeryToken
   - XSS：Razor語法自動編碼、ValidateRequest請求驗證
   - SQL injection：使用ORM框架 Entity Framework(EF)

- ### **身分驗證**
   - JWT
<br><br>

## 參考資源
1. [How To Deploy ASP.NET Application with Azure SQL Database on Microsoft Azure Cloud](https://www.youtube.com/watch?v=jT8eA9A7qXE&t=442s)

   Azure sql server建立資訊 (測試用資料庫):
   - 區域：japaneast
   - 資料庫名稱：magaFine
   - 伺服器：magafine
   - 定價層：一般目的-無伺服器：標準系列 (第 5 代), 1 vCore，32 GB 儲存體, 區域備援
   - 備份儲存體備援：異地備援備份儲存體
   - 自動暫停延遲：1hr
     </br></br><img src="https://i.imgur.com/2xg1fPu.png" alt="資源使用率" style="width: 30%; height: auto;">

3. [First MVC Project With Oracle Database using Entity Frame Work - Part 2/2](https://www.youtube.com/watch?v=tk_EDjTzZCE)
4. [認識VS開發環境 - NuGet套件管理員](https://ithelp.ithome.com.tw/articles/10158563):套件工具
5. [How to Implement JWT Token Authentication in Asp Net MVC](https://rutube.ru/video/70509d6db26c1c1365ee8026a0dda35b/):JWT
