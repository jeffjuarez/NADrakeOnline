using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using DataAnnotationsExtensions;
using Repository.Pattern.Ef6;

namespace DOL.Entities.Models
{
    public class client_users : Entity
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string user_id { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int32 client_id { get; set; }

        [Required]
        public Int32 group_id { get; set; }

        [Required]
        public int? access_level { get; set; }


        [Required]
        public DateTime created_on { get; set; }


        [Required]
        public DateTime modified_on { get; set; }

        [Required]
        public int? drake_position_title { get; set; }


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
