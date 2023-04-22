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
        public static void DeleteTask(Node node, Models.Task task)
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
                    }
                }
            }
            File.Delete(path + task.name + fileExtension);
            node.tasks.Remove(task);
        }
        public static void MoveTask(Node selectedNode, Models.Task task, bool moveUp)
        {
            string path = TreeUtils.GetPathFromTreeNode(selectedNode);
            string[] data = System.IO.File.ReadAllLines(path + "tasksList.txt");
            for (int index = 0; index < data.Length; index++)
            {
                if (data[index] == task.name)
                    if ((index == 0 && moveUp) || (index == data.Length - 1 && !moveUp))
                    {
                        MessageBox.Show("Task can't be moved further");
                        return;
                    }
                    else
                    {
                        if (moveUp)
                        {
                            string temp = data[index];
                            data[index] = data[index - 1];
                            data[index - 1] = temp;
                            break;
                        }
                        else
                        {
                            string temp = data[index];
                            data[index] = data[index + 1];
                            data[index + 1] = temp;
                            break;
                        }

                    }
            }
            using (StreamWriter file = new StreamWriter(path + "tasksList.txt", false))
            {
                foreach (string str in data)
                {
                    file.WriteLine(str);
                }
            }
            MessageBox.Show("Success!");
        }
    }
}
