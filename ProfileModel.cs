using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class ProfileModel
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public UserRole1Model Role { get; set; }
    }
}