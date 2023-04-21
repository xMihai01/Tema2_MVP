using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using Tema2_MVP.Models;
using Tema2_MVP.Utils;

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
                        if (taskData.Length == 5)
                            tasks.Add(new Models.Task(data, taskData[0], taskData[1], taskData[2], taskData[3],
                                Convert.ToDateTime(taskData[4])));
                        else if (taskData.Length == 6)
                            tasks.Add(new Models.Task(data, taskData[0], taskData[1], taskData[2], taskData[3],
                                Convert.ToDateTime(taskData[4]), taskData[5]));
                    }
                }
            }
            return tasks;
        }

        public static void SaveTaskToFile(Node node, Models.Task task, bool isTaskNew = true)
        {
            string path = TreeUtils.GetPathFromTreeNode(node);
            if (isTaskNew)
            {
                using (StreamWriter file = new StreamWriter(path + "tasksList.txt", true))
                {
                    file.WriteLine(task.name);
                }
                using (FileStream fs3 = File.Create(path + task.name + fileExtension)) ;
                node.tasks.Add(task);
            }
            using (StreamWriter file = new StreamWriter(path + task.name + fileExtension, false))
            {
                file.WriteLine(task.description);
                file.WriteLine(task.category);
                file.WriteLine(task.status);
                file.WriteLine(task.priority);
                file.WriteLine(task.deadline.ToString("MM/dd/yyyy"));
                if (task.finishDate != "N/A")
                    file.WriteLine(task.finishDate);
            }

        }

        public static void RenameTask(Node node, Models.Task task, string newName)
        {
            string path = TreeUtils.GetPathFromTreeNode(node);
            string[] data = System.IO.File.ReadAllLines(path + "tasksList.txt");
            using (StreamWriter file = new StreamWriter(path + "tasksList.txt", false))
            {
                foreach (string name in data)
                {
                    if (name != task.name)
                    {
                        file.WriteLine(name);
                    } else
                    {
                        file.WriteLine(newName);
                    }
                }
            }
            File.Move(path + task.name + fileExtension, path + newName + fileExtension);
            task.name = newName;
        }
    }
}
