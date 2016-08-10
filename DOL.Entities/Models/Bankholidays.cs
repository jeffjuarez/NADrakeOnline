using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
      public class Bankholidays : Entity
        {
            [Key]
            public int id { get; set; }

            [Required]
            [MaxLength(25)]
            public string description { get; set; }

            [Required]
            public string status { get; set; }

        
            [Required]
            public DateTime holiday_date { get; set; }


            [Required]
            public string createdby { get; set; }

            [Required]
            public DateTime date_created { get; set; }

            [Required]
            public DateTime modified_on { get; set; }


       
    }

}
