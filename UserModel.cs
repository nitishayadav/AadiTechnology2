using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public  UserRole1Model UserRole1 { get; set; }
    }
}