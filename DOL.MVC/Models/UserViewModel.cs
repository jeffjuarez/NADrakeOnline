using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOL.MVC.Models
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Login_Name { get; set; }
        public string Fullname { get; set; }
        public int Role { get; set; }
    }
}