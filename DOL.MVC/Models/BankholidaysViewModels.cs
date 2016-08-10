using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class BankholidaysViewModels
    {
        public int Id { get; set; }

        [Display(Name = "description")]
        public string description { get; set; }

        [Display(Name = "status")]
        public string status { get; set; }

        [Display(Name = "holiday_date")]
        public DateTime holiday_date { get; set; }

        [Display(Name = "createdby")]
        public string createdby { get; set; }

        [Display(Name = "date_created")]
        public DateTime date_created { get; set; }


        [Display(Name = "modified_on")]
        public DateTime modified_on { get; set; }

    }

    


}