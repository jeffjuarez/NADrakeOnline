using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class timeclock_transform : Entity
        {
            [Key]
            public Int64 Timesheet_Number { get; set; }

            public Int64 Entry_Id { get; set; }


            [Required]
            public string contractor_id { get; set; }

            [Required]
            public DateTime Clock_Date { get; set; }

            [Required]
            public Int32 Clock_id { get; set; }

     
            [Required]
            public string Time_In { get; set; }

            [Required]
            public string Time_Out{ get; set; }

            [Required]
            public string Break_In { get; set; }
           
            [Required]
            public string Break_Out { get; set; }


            [Required]
            public double Daily_Hours { get; set; }

            [Required]
            public DateTime Created { get; set; }


       
    }

}
