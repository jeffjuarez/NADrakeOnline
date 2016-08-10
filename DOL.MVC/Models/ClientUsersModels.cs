using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class ClientUsersModels
    {

        [Display(Name = "user_id")]
        public string user_id { get; set; }
       
        [Display(Name = "client_id")]
        public Int32 client_id { get; set; }

        [Display(Name = "group_id")]
        public Int32 group_id { get; set; }

        [Display(Name = "access_level")]
        public Int64 access_level { get; set; }

        [Display(Name = "created_on")]
        public DateTime? created_on { get; set; }

        [Display(Name = "modified_on")]
        public DateTime? modified_on { get; set; }

    }



    


}