using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class Timesheet_orders : Entity
        {
            [Key]
            public Int64 Timesheet_Number { get; set; }

        

            [Required]
            public System.Int32 Order_Id { get; set; }

            //[Required]
            public Int64? Processed_In_Timesheet { get; set; }

            //[Required]
            public double? No_of_Hours { get; set; }

          
            public string Status { get; set; }


         
            public string Approved_by { get; set; }


         
            public DateTime? Approved_on { get; set; }

            //[Required]
            public string Processed_by { get; set; }


            //[Required]
            public DateTime? Processed_on { get; set; }

            //[Required]
            public DateTime? Cont_Approved_on { get; set; }


            //[Required]
            public string Comment_Supervisor { get; set; }

     
            //[Required]
            public string Comment_Payroll { get; set; }

            //[Required]
            public double? No_of_Hours_with_gaps { get; set; }


       
    }

}
