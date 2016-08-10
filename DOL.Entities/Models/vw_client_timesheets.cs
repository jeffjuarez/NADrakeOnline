using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class vw_client_timesheets : Entity
        {
            [Key]
            public Int64 Timesheet_Number { get; set; }


            public Int64 Timesheet_Serial_Number { get; set; }


            [Required]
            public DateTime? TS_start_date { get; set; }

            [Required]
            public DateTime? TS_end_date { get; set; }


            [Required]
            public string contractor_id { get; set; }



            [Required]
            public DateTime? Shift_date { get; set; }


            [DisplayFormat(DataFormatString = "{0:t}")]
            [Required]
            public DateTime? Actual_start_time { get; set; }


            [Required]
            public DateTime? Actual_end_time { get; set; }



            [Required]
            public DateTime? Actual_break_start { get; set; }


            [Required]
            public DateTime? Actual_break_end { get; set; }
         
            [Required]
            public double? Total_Hours { get; set; }



           
            public int? leave_reason { get; set; }


            [Required]
            public string to_status { get; set; }

            [DataType(DataType.MultilineText)]
            public string comment_supervisor { get; set; }

    }

}
