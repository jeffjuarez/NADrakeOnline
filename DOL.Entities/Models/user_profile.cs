using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAnnotationsExtensions;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class user_profile : Entity
    {
        [Key]
        public string user_id { get; set; }

        [Required]
        [MaxLength(25)]
        public string first_name { get; set; }

        [Required]
        [MaxLength(25)]
        public string last_name { get; set; }

        [Required]
        public string address1 { get; set; }

        [Required]
        public string address2 { get; set; }

        [Required]
        public string city { get; set; }

        [Required]
        public string pincode { get; set; }

        [Required]
        public string state { get; set; }

        [Required]
        public string email_id { get; set; }

        [Required]
        public string home_phone { get; set; }

        [Required]
        public string work_phone { get; set; }

        [Required]
        public string mobile_phone { get; set; }

        [Required]
        public string fax { get; set; }


        [Required]
        public DateTime created_on { get; set; }


        [Required]
        public DateTime modified_on { get; set; }


    }

  


}
