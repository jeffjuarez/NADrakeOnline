using System.Web.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.Helpers;
using DOL.Entities.Models;
//using DOL.MVC.Utilities;

namespace DOL.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        public ActionResult CreateBar()
        {
            //Create bar chart
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "column",
                            xValue: new[] { "10 ", "50", "30 ", "70" },
                            yValues: new[] { "50", "70", "90", "110" })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult CreatePie()
        {
            //Create bar chart
            var chart = new Chart(width: 300, height: 200)
            .AddSeries(chartType: "pie",
                            xValue: new[] { "10 ", "50", "30 ", "70" },
                            yValues: new[] { "50", "70", "90", "110" })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }

        public ActionResult CreateLine()
        {
            //Create bar chart
            var chart = new Chart(width: 600, height: 200)
            .AddSeries(chartType: "line",
                            xValue: new[] { "10 ", "50", "30 ", "70" },
                            yValues: new[] { "50", "70", "90", "110" })
                            .GetBytes("png");
            return File(chart, "image/bytes");
        }

        //public ActionResult Index()
        //{
        //    return View("Index");
        //}


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Chart()
        {

            ViewBag.Message = "Charting Here";

            return View();
        }


        public ActionResult About()
        {
            return View();
        }


        public ActionResult AboutOI()
        {
          
            ViewBag.Message = "Your application description page.";

            return View();
        }




        public ActionResult Contact()
        {
            FormsAuthentication.SignOut();
            ViewBag.Message = "Your contact page.";
            return PartialView("Contact");

        }


        //public ActionResult PassCreator(string p)
        //{
        //    var newPassword = AccountHelpers.HashPassword(p);
        //    ViewBag.Salt = newPassword.Salt;
        //    ViewBag.Password = newPassword.HashPassword;
        //    return View();
        //}


    
       // [AllowAnonymous]
       // public ActionResult ContactForm()
       // {
       //     ViewBag.EmailError = "";
       //     ViewBag.Success = false;
       //   //  ContactForm cform = new ContactForm();
       //  //   cform.Message = "";
       ////     return PartialView("_contactForm", cform);
       // }

        //[HttpPost]
        //public ActionResult ContactForm(ContactForm model)
        //{
        //    //ViewBag.Success = false;
        //    //ViewBag.EmailError = "";
        //    //if (ModelState.IsValid)
        //    //{
        //    //    var mailer = new AppMailer();
        //    //    var welcomeEmail = mailer.ContactForm(model);
        //    //    try
        //    //    {
        //    //        welcomeEmail.Send();
        //    //        ViewBag.Success = true;
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        ViewBag.EmailError = "There was an error while sending your message, please try again later.";
        //    //    }

        //    //}
        //    return PartialView("_contactForm", model);
        //}









    }
}