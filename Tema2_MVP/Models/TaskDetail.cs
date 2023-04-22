using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MVP.Models
{
    public class TaskDetail
    {

        public Task task { get; set; }
        public string name { get; set; }
        public string location { get; set; }

        public TaskDetail(Task task, string location)
        {
            this.task = task;
            this.location = location;
            this.name = task.name;
        }
    }
}
