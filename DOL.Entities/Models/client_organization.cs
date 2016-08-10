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
    public class client_organization : Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 group_id { get; set; }

        [Required]
        public int business_type { get; set; }

        [Required]
        public Int64? client_id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Required]
        public Int64 parent_group_id { get; set; }

        [Required]
        public int? office_id { get; set; }
             
        [Required]
        [MaxLength(1)]
        public string custome_role_defn { get; set; }

        [Required]
        [MaxLength(50)]
        public string client_org_name { get; set; }

        [Required]
        public int client_org_type { get; set; }

        [Required]
        [MaxLength(50)]
        public string address1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string address2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string city { get; set; }

        [Required]
        [MaxLength(50)]
        public string pincode { get; set; }

        [Required]
        [MaxLength(50)]
        public string state { get; set; }

        [Required]
        [MaxLength(50)]
        public string country { get; set; }

        [Required]
        [MaxLength(50)]
        public string phone1 { get; set; }

        [Required]
        [MaxLength(50)]
        public string phone2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string fax { get; set; }


        [Required]
        [MaxLength(50)]
        public string contact_first_name { get; set; }

        [Required]
        [MaxLength(50)]
        public string contact_last_name { get; set; }


        [Required]
        [MaxLength(50)]
        public string contact_phone { get; set; }

        [Required]
        [MaxLength(50)]
        public string contact_fax { get; set; }


        [Required]
        public DateTime? created_on { get; set; }


        [Required]
        public DateTime? modified_on { get; set; }


        [Required]
        [MaxLength(50)]
        public string link_string { get; set; }

        [Required]
        public int? heirarchy_level { get; set; }
     
        [Required]
        [MaxLength(50)]
        public string ft_client_id { get; set; }


    }

  

}
