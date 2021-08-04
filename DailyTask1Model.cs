using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginTask.Models
{
    public class DailyTask1Model
    {
        public int Id { get; set; }
        public int SubTopicId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int EstimatedHr { get; set; }

        public  SubTopicModel SubTopic { get; set; }
    }
}