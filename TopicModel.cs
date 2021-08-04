using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class TopicModel
    {
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int CourseId { get; set; }

        public  CourseModel Course { get; set; }
        public  List<SubTopicModel> SubTopic { get; set; }
    }
}