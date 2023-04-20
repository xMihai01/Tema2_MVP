using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MVP.Utils
{
    internal class TaskUtils
    {
        public const string fileExtension = ".txt";
        public static ObservableCollection<Models.Task> GetTasksForTDL(string currentDir, string tdl)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            if (File.Exists(currentDir + tdl + "/tasksList" + fileExtension))
            {
                string[] dbdata = System.IO.File.ReadAllLines(currentDir + tdl + "/tasksList" + fileExtension);
                foreach (string data in dbdata)
                {
                    if (File.Exists(currentDir + tdl + "/" + data + fileExtension))
                    {
                        string[] taskData = System.IO.File.ReadAllLines(currentDir + tdl + "/" + data + fileExtension);
                        tasks.Add(new Models.Task(data, taskData[0], taskData[1], taskData[2], taskData[3], taskData[4],
                            false, Convert.ToDateTime(taskData[5]), Convert.ToDateTime(taskData[6])));
                    }
                }
            }
            return tasks;
        }
    }
}
