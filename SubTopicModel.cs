using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class SubTopicModel
    {
        public int Id { get; set; }
        public string SubTopicName { get; set; }
        public int TopicId { get; set; }
        public  List<DailyTask1Model> DailyTask1 { get; set; }
        public  TopicModel Topic { get; set; }
    }
}