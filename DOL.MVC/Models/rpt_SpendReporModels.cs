using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class rpt_SpendReportModels
    {


        public string Client_Name { get; set; }

        public string Dept_Name { get; set; }

        public Int32 client_id { get; set; }


        public string invoice_number { get; set; }

        public DateTime? invoice_date { get; set; }

        public Int64 order_number { get; set; }

        [Key]
        public string timesheet_number { get; set; }

        public string line_description { get; set; }

        public Double hours_worked { get; set; }

        public decimal hourly_bill_rate { get; set; }

        public decimal billing_amt { get; set; }

    }



    


}