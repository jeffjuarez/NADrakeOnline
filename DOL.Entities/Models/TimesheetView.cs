using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
      public class TimeSheetView : Entity
        {
            [Key]
            public Int64 Timesheet_Number { get; set; }

            [Required]
            public DateTime Order_Start_Date { get; set; }

            [Required]
            public DateTime Order_Finish_Date { get; set; }

            [Required]
            public string Contractor_FullName { get; set; }

            [Required]
            public string Organization_Level { get; set; }

            [Required]
            public string Contractor_Title { get; set; }

     
            [Required]
            public string Approval_Status { get; set; }

            [Required]
            public double No_of_Hours { get; set; }

         

       
    }

}
