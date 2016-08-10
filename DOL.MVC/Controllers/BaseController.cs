using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DOL.MVC.Helpers;

namespace DOL.MVC.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }


        public void Success(string message, bool dismissable = false)
        {
            AddAlert(MvcExtensionHelper.AlertStyles.Success, message, dismissable);
        }


        public void Information(string message, bool dismissable = false)
        {
            AddAlert(MvcExtensionHelper.AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(MvcExtensionHelper.AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(MvcExtensionHelper.AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(MvcExtensionHelper.Alert.TempDataKey)
                ? (List<MvcExtensionHelper.Alert>)TempData[MvcExtensionHelper.Alert.TempDataKey]
                : new List<MvcExtensionHelper.Alert>();

            alerts.Add(new MvcExtensionHelper.Alert
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[MvcExtensionHelper.Alert.TempDataKey] = alerts;
        }

    }

    
}