using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class TimeclockTransformDetailsModels
    {
      
        [Display(Name = "Timesheet_Number")]
        public Int64 Timesheet_Number { get; set; }

        [Display(Name = "Entry_id")]
        public Int64 Entry_id { get; set; }


        [Display(Name = "TS_start_date")]
        public string TS_start_date { get; set; }


        [Display(Name = "TS_end_date")]
        public string TS_end_date { get; set; }



        [Display(Name = "contractor_id")]
        public string contractor_id { get; set; }



        [Display(Name = "Shift_date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime? Shift_date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        [Display(Name = "Actual_start_time")]
        public string Actual_start_time { get; set; }
      
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        [Display(Name = "Actual_end_time")]
        public string Actual_end_time { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")] 
        [Display(Name = "Actual_break_start")]
        public string Actual_break_start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")] 
        [Display(Name = "Actual_break_end")]
        public string Actual_break_end { get; set; }

        [Display(Name = "Total_Hours")]
        public double? Total_Hours { get; set; }



        [Display(Name = "leave_reason")]
        public int? leave_reason { get; set; }



        [Display(Name = "to_status")]
        public string to_status { get; set; }


        [Display(Name = "comment_supervisor")]
        public string comment_supervisor { get; set; }


        
    }



    


}