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
        public string description { get; set; }
        public string priority { get; set; }
        public string dueDate { get; set; }
        public string type { get; set; }
        bool isDone { get; set; }

        public Task(string name, string description, string priority, string dueDate, string type, bool isDone)
        {
            this.name = name;
            this.description = description;
            this.priority = priority;
            this.dueDate = dueDate;
            this.type = type;
            this.isDone = isDone;
        }
    }
}
