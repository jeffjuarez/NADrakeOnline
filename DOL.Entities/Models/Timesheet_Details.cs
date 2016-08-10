using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class Timesheet_Details : Entity
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Int64 Timesheet_Number { get; set; }


            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            [Required]
            public Int64 Timesheet_Serial_Number { get; set; }

            [Required]
            public System.Int32 Order_Id { get; set; }


            [Required]
            public DateTime? Shift_Date { get; set; }

          
            public DateTime? Actual_start_time { get; set; }

 
            public DateTime? Actual_end_time { get; set; }


            public DateTime? Actual_break_start { get; set; }

            public DateTime? Actual_break_end { get; set; }


            [Required]
            public string Status { get; set; }

            [Required]
            public string Contractor_Confirm { get; set; }
        
            [Required]
            public string Client_Confirm { get; set; }

            [Required]
            public int? leave_reason { get; set; }



            [Required]
            public DateTime? Created_On { get; set; }

       
       
    }

}
