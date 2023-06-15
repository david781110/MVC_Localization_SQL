using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MVC_Localization_SQL.Models;
using System.Reflection.Metadata.Ecma335;
using System.Resources;
using System.Resources.NetStandard;

namespace MVC_Localization_SQL.Controllers
{
    public class BaseController : Controller
    {
        private readonly TeachContext _db;
        string CookieKey = "CultureInfo";
        public BaseController(TeachContext context)
        {
            _db = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string CultureName = "";
            if (Request.Cookies.ContainsKey(CookieKey))
            {
                CultureName = Request.Cookies[CookieKey];
            }
            else
            {
                CultureName = Request.Headers["accept-language"][0].Split(',')[0];
            }

            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(CultureName);
            base.OnActionExecuting(context);
        }

        public IActionResult SetLanguage(string CultureName)
        {
            SetCookie(CultureName);
            string Referer = Request.Headers["referer"];
            return Redirect(Referer);
        }

        private void SetCookie(string CultureName)
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddYears(1);
            options.Secure = true;
            options.HttpOnly = true;
            Response.Cookies.Delete(CookieKey);
            Response.Cookies.Append(CookieKey, CultureName, options);
        }

        /// <summary>
        /// 同步更新資料庫語系檔至 Resources
        /// </summary>
        /// <returns></returns>
        /// 
        //public IActionResult UpdateLang()
        //{
        //    ResXResourceWriter WriterTW = new ResXResourceWriter(Server.MapPath("~/Resources/WebResource.resx"));
        //    ResourceWriter WriterUS = new ResourceWriter("~/Resources/WebResource.resx");
        //    var List = from m in _db.Language
        //               select m;

        //    foreach (var item in List)
        //    {
        //        WriterTW.AddResource(item.LangKey, item.LangZhTw);
        //        WriterUS.AddResource(item.LangKey, item.LangEnUs);
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

    }
}
