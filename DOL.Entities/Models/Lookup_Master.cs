using System;
using System.ComponentModel.DataAnnotations;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
      public class Lookup_Master : Entity
        {
            [Key]
            public Int32 lookup_id { get; set; }

            [Required]
            public string lookup_type { get; set; }

            [Required]
            public string lookup_value { get; set; }

            [Required]
            public string lookup_desc { get; set; }

           [Required]
           public DateTime created_on { get; set; }

           [Required]
           public DateTime modified_on { get; set; }

         

       
    }

}
