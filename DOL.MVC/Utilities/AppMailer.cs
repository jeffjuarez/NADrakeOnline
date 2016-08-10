//using Mvc.Mailer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//using DOL.Entities.Models;

//namespace DOL.MVC.Utilities
//{
//    public class AppMailer : MailerBase, IAppMailer
//    {
//        public AppMailer()
//        {
//            MasterName = "_Layout";
//        }

//        public MvcMailMessage ContactForm(ContactForm form)
//        {
//            ViewBag.ContactForm = form;
//            return Populate(x =>
//            {
//                x.Subject = String.Concat("An online inquiry from DOL.drakeintl.com");
//                x.ViewName = "ContactForm";
//                x.To.Add("jjuarez@drakeintl.co.uk");
//                x.Bcc.Add("rjuarez@na.drakeintl.com");
//            });
//        }
//    }
//}