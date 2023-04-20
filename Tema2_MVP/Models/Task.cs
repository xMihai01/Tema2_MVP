using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MVP.Models
{
    public class Task
    {
        public string name { get; set; }
        public string priority { get; set; }
        public DateTime dueDate { get; set; }
        public DateTime deadline { get; set; }
        public string type { get; set; }
        public bool isDone { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string status { get; set; }

        public Task(string name, string description, string category, string status, string priority, string type, bool isDone, DateTime dueDate, DateTime deadline)
        {
            this.name = name;
            this.description = description;
            this.priority = priority;
            this.dueDate = dueDate;
            this.type = type;
            this.isDone = isDone;
            this.category = category;
            this.status = status;
            this.deadline = deadline;
        }
    }
}
