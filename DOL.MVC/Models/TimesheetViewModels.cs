using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class TimesheetViewModels
    {
      
        [Display(Name = "Timesheet_Number")]
        public Int64 Timesheet_Number { get; set; }


        [Display(Name = "Order_Start_Date")]
        public DateTime Order_Start_Date { get; set; }

        [Display(Name = "Order_Finish_Date")]
        public DateTime Order_Finish_Date { get; set; }


        [Display(Name = "Contractor_FullName")]
        public string Contractor_FullName { get; set; }

        [Display(Name = "Organization_Level")]
        public string Organization_Level { get; set; }

        [Display(Name = "Contractor_Title")]
        public string Contractor_Title { get; set; }

        [Display(Name = "Approval_Status")]
        public string Approval_Status { get; set; }

        [Display(Name = "Approval_Status_Description")]
        public string Approval_Status_Description { get; set; }

        [Display(Name = "No_of_Hours")]
        public double? No_of_Hours { get; set; }
    }


}