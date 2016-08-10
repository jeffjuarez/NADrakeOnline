using System.ComponentModel.DataAnnotations;
using System;
namespace DOL.MVC.Models
{
    public class ClientOrganizationModels
    {

        [Display(Name = "group_id")]
        public Int32 group_id { get; set; }
       
        [Display(Name = "client_id")]
        public Int32 client_id { get; set; }

        [Display(Name = "parent_group_id")]
        public Int64 parent_group_id { get; set; }

        [Display(Name = "client_org_name")]
        public string client_org_name { get; set; }

    }



    


}