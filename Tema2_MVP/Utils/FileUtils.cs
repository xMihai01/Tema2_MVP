using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MVP.Models;

namespace Tema2_MVP.Utils
{
    internal class FileUtils
    {
        public const string fileExtension = ".txt";

        public const string databaseDirectory = "databases";

        public static void CreateDirectories()
        {

            if (!Directory.Exists("databases"))
            {
                Directory.CreateDirectory("databases");
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
                    Node node = new Node() { Text = data, Children = new System.Collections.ObjectModel.ObservableCollection<Node>() };
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
                    Console.WriteLine(currentDirectory);
                    if (File.Exists(currentDirectory + currentTDL + "/todoList" + fileExtension))
                    {
                        dbdata = System.IO.File.ReadAllLines(currentDirectory + currentTDL + "/todoList" + fileExtension);
                        foreach (string data in dbdata)
                        {
                            Node newNode = new Node() { Text = data, Children = new System.Collections.ObjectModel.ObservableCollection<Node>() };
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
    }
}
