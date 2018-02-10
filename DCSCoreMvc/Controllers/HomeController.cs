using DCSCoreMvc.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace DCSCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private IStringLocalizer<HomeController> _stringLocalizer;

        public HomeController(IStringLocalizer<HomeController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            var culture = Request.HttpContext.Session.GetString("culture");
            ViewBag.Language = culture;
            return View();
        }

        [EnableCors("OptiosPolicy")]
        public IActionResult Appointment()
        {
            Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "http://kapper.optios.net");
            //Request.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return View();
        }

        //public IActionResult About()
        //{
        //    ViewData["Message"] = "Your application description page.";

        //    return View();
        //}

        //public IActionResult Contact()
        //{
        //    ViewData["Message"] = "Your contact page.";

        //    return View();
        //}

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetCulture(string culture, string returnUrl = null)
        {
            Request.HttpContext.Session.SetString("culture", culture);
            if (returnUrl != null)
            {
                return RedirectToAction(returnUrl);
            }
            string referer = Request.Headers["Referer"].ToString();
            if (referer != null)
            {
                return Redirect(referer);
            }
            return RedirectToAction("Index");
        }
    }
}
