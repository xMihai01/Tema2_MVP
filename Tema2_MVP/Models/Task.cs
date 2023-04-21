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
        public string finishDate { get; set; } // could be DateTime
        public DateTime deadline { get; set; }
        public bool isDone { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string status { get; set; }

        public Task(string name, string description, string category, string status, string priority, DateTime deadline, string finishDate = "N/A")
        {
            this.name = name;
            this.description = description;
            this.priority = priority;
            this.category = category;
            this.status = status;
            this.deadline = deadline;
            this.finishDate= finishDate;
            if (finishDate != "N/A")
                this.finishDate = Convert.ToString(Convert.ToDateTime(finishDate));
                //this.finishDate = Convert.ToDateTime(finishDate);
            if (status == "Done")
                this.isDone = true;
            else
                this.isDone = false;
        }
    }
}
