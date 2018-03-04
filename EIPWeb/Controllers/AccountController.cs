using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EIPWeb.Models;
using EIPWeb.Models.ViewModel;
using System.Web.Security;

namespace EIPWeb.Controllers
{
    public class AccountController : BaseController
    {
        EIPWebEntities db = new EIPWebEntities();
        
        // GET: Accounts/Create
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        // POST: Accounts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel user)
        {
            //驗證資料庫登入
            //這邊請使用自行驗證摟
            //var query = db.Account.Where(a => a.UserID == user.UserID && a.Password == a.Password);
            if (!IsAuthenticated(user))
            {
                ModelState.AddModelError(string.Empty, "帳號或密碼錯誤登入失敗");
                return View("Login", user);
            }

            try
            {
                //query[0].LoginIP = PublicFunction.GetIpAddress();
                //query[0].LoginDate = DateTime.Now;
                //db.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message.ToString());
                return View("Index", user);
            }

            bool isPersistent = false;//如果票證將存放於持續性 Cookie 中 (跨瀏覽器工作階段儲存)，則為 true，否則為 false。 如果票證是存放於 URL 中，則忽略這個值。
            string userData = "";//可放使用者自訂的內容
            string mAccountID = "";

            var emp = db.Employee.Where(e => e.ADLoginID.ToLower() == user.UserID.ToLower() && e.ResignDate == null).SingleOrDefault();
            if (emp != null)
            {
                userData = emp.DepartmentID;//可放使用者自訂的內容
                mAccountID = emp.Name;
            }
            else
            {
                mAccountID = user.UserID;
            }

            //寫cookie
            //使用 Cookie 名稱、版本、到期日、核發日期、永續性和使用者特定的資料，初始化 FormsAuthenticationTicket 類別的新執行個體。 此 Cookie 路徑設定為在應用程式的組態檔中建立的預設值。
            //使用 Cookie 名稱、版本、目錄路徑、核發日期、到期日期、永續性和使用者定義的資料，初始化 FormsAuthenticationTicket 類別的新執行個體。
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
              mAccountID,//使用者ID
              DateTime.Now,//核發日期
              DateTime.Now.AddMinutes(1800),//到期日期 30分鐘 
              isPersistent,//永續性
              userData,//使用者定義的資料
              FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            //cookie.Expires = ticket.Expiration;
            //Response.Cookies.Add(cookie);

            //FormsAuthentication.RedirectFromLoginPage(strUsername, isPersistent);

            if (user.ReturnUrl != null)
            {
                return Redirect(user.ReturnUrl.ToString());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //return RedirectToAction("Index", "Login", null);
            //return View("Index", user);       

            /*
            if (ModelState.IsValid)
            {
                db.Account.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(account);
            */
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}