using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class LookupMasterModels
    {

        [Display(Name = "lookup_id")]
        public Int32 lookup_id { get; set; }

     
        [Display(Name = "lookup_type")]
        public string lookup_type { get; set; }

        [Display(Name = "lookup_value")]
        public string lookup_value { get; set; }

        [Display(Name = "lookup_desc")]
        public string lookup_desc { get; set; }


        [Display(Name = "created_on")]
        public DateTime? created_on { get; set; }

        [Display(Name = "modified_on")]
        public DateTime? modified_on { get; set; }


        
    }



    


}