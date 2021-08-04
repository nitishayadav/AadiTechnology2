using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public  List<TopicModel> Topic { get; set; }
        public int count { get; set; }
    }
}