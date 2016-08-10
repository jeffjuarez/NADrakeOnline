using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class TimesheetOrdersModels
    {
      
        [Display(Name = "Timesheet_Number")]
        public Int64 Timesheet_Number { get; set; }

         
        [Display(Name = "Order_Id")]
        public int? Order_Id { get; set; }


        [Display(Name = "Processed_In_Timesheet")]
        public Int64? Processed_In_Timesheet { get; set; }

      
        [Display(Name = "No_of_Hours")]
        public Double? No_of_Hours { get; set; }


        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Approved_by")]
        public string Approved_by { get; set; }

        [Display(Name = "Approved_on")]
        public DateTime? Approved_on { get; set; }

        [Display(Name = "Processed_by")]
        public string Processed_by { get; set; }

        [Display(Name = "Processed_on")]
        public DateTime? Processed_on { get; set; }

        [Display(Name = "Approved_on")]
        public DateTime? Cont_Approved_on { get; set; }

        [Display(Name = "Comment_Supervisor")]
        public string Comment_Supervisor { get; set; }

      
        [Display(Name = "Comment_Payroll")]
        public string Comment_Payroll { get; set; }


        [Display(Name = "No_of_Hours_with_gaps")]
        public Double? No_of_Hours_with_gaps { get; set; }



        
    }



    


}