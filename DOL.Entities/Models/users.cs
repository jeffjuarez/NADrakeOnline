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
    public class users : Entity
    {
        [Key]
        public string user_id { get; set; }

        [Required]
        [MaxLength(25)]
        public string login_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string password { get; set; }

        [Required]
        public string status { get; set; }

        [Required]
        public int user_role { get; set; }

        [Required]
        public int? last_session_id { get; set; }


        [Required]
        public DateTime created_on { get; set; }


        [Required]
        public DateTime modified_on { get; set; }


    }

    //public class User_Profile: Entity
    //{
    //    [Key]
    //    public string user_id { get; set; }

    //    [Required]
    //    [MaxLength(25)]
    //    public string first_name { get; set; }

    //    [Required]
    //    [MaxLength(50)]
    //    public string last_name { get; set; }

     

    //}


}
