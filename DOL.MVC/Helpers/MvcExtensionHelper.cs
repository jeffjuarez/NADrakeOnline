using System.Web.Mvc;
using System.Globalization;
using DOL.MVC.Models;

namespace DOL.MVC.Helpers
{
    public static class MvcExtensionHelper
    {
        public static MvcHtmlString If(this MvcHtmlString value, bool evaluation, MvcHtmlString falseValue = default(MvcHtmlString))
        {
            return evaluation ? value : falseValue;
        }


        public class Alert
        {
            public const string TempDataKey = "TempDataAlerts";

            public string AlertStyle { get; set; }
            public string Message { get; set; }
            public bool Dismissable { get; set; }
        }

        public static class AlertStyles
        {
            public const string Success = "success";
            public const string Information = "info";
            public const string Warning = "warning";
            public const string Danger = "danger";
        }

        public static int GetWeekOfYear(System.DateTime dateReceived)
        {


            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            int weekNum = ciCurr.Calendar.GetWeekOfYear(dateReceived, CalendarWeekRule.FirstFourDayWeek,  System.DayOfWeek.Monday);
            return weekNum;
        }

        public static bool IsFinite(double value)
        {
            return double.IsNaN(value) || double.IsInfinity(value);
        }

    }
}