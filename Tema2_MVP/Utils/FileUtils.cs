using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tema2_MVP.Models;

namespace Tema2_MVP.Utils
{
    internal class FileUtils
    {
        public const string fileExtension = ".txt";

        public const string databaseDirectory = "databases";

        public static void FirstTimeSetUp()
        {

            if (!Directory.Exists("databases"))
            {
                Directory.CreateDirectory("databases");
            }
            if (!File.Exists("databases/databaseList.txt")) {
                File.Create("databases/databaseList.txt");
            }

        }
        public static Database GetDatabaseDetailsFromFile(string databaseName)
        {
            Database database = new Database();
            string initialDirectory = databaseDirectory + "/" + databaseName + "/";

            if (File.Exists(databaseDirectory + "/" + databaseName + "/todoList" + fileExtension))
            {
                Queue<string> queue = new Queue<string>();
                Queue<string> dirQueue = new Queue<string>();
                Queue<Node> nodeQueue = new Queue<Node>();
                string[] dbdata = System.IO.File.ReadAllLines(databaseDirectory + "/" + databaseName + "/todoList" + fileExtension);
                foreach (var data in dbdata)
                {
                    Node node = new Node() { Text = data, Children = new System.Collections.ObjectModel.ObservableCollection<Node>(), lastNode = null };
                    database.nodes.Add(node);
                    nodeQueue.Enqueue(node);
                    queue.Enqueue(data);
                    dirQueue.Enqueue(initialDirectory);
                }

                while (queue.Any())
                {
                    string currentTDL = queue.Dequeue();
                    string currentDirectory = dirQueue.Dequeue();
                    Node node = nodeQueue.Dequeue();
                    node.tasks = GetTasksForTDL(currentDirectory, currentTDL);

                    if (File.Exists(currentDirectory + currentTDL + "/todoList" + fileExtension))
                    {
                        dbdata = System.IO.File.ReadAllLines(currentDirectory + currentTDL + "/todoList" + fileExtension);
                        foreach (string data in dbdata)
                        {
                            Node newNode = new Node() { Text = data, Children = new System.Collections.ObjectModel.ObservableCollection<Node>(), lastNode = node };
                            node.Children.Add(newNode);

                            nodeQueue.Enqueue(newNode);
                            queue.Enqueue(data);
                            dirQueue.Enqueue(currentDirectory + currentTDL + "/");
                        }
                    }
                }
            }
            return database;
        }

        private static ObservableCollection<Models.Task> GetTasksForTDL(string currentDir, string tdl)
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


        public static string[] GetDatabaseList()
        {
            if (File.Exists("databases/databaseList.txt"))
            {
                return System.IO.File.ReadAllLines("databases/databaseList.txt");
            }
            return null;
        }

    }
}
