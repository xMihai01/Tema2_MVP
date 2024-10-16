﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Xml.Linq;
using Tema2_MVP.Models;
using Tema2_MVP.ViewModels;
using static Tema2_MVP.Utils.TaskUtils;

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
            if (!File.Exists("databases/databaseList.txt"))
            {
                using (FileStream fs3 = File.Create("databases/databaseList.txt")) { }
                CreateDatabase("default");
            }
            if (!File.Exists("databases/currentDatabase" + fileExtension))
            {
                using (FileStream fs3 = File.Create("databases/currentDatabase" + fileExtension)) { }
                using (StreamWriter file = new StreamWriter("databases/currentDatabase" + fileExtension, true))
                {
                    file.WriteLine("default");
                }
            }
            if (!File.Exists("databases/categoryList" + fileExtension))
            {
                using (FileStream fs3 = File.Create("databases/categoryList" + fileExtension)) { }
                using (StreamWriter file = new StreamWriter("databases/categoryList" + fileExtension, true))
                {
                    file.WriteLine("School");
                    file.WriteLine("Work");
                    file.WriteLine("Other");
                }
            }
        }
        public static Database GetDatabaseDetailsFromFile(string databaseName)
        {
            Database database = new Database();
            string initialDirectory = databaseDirectory + "/" + databaseName + "/";

            if (File.Exists(databaseDirectory + "/" + databaseName + "/todoList" + fileExtension))
            {
                ChangeCurrentDB(databaseName);
                database.name = databaseName;
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

        public static void CreateDatabase(string name)
        {
            string[] dbs = System.IO.File.ReadAllLines("databases/databaseList.txt");
            foreach (string db in dbs)
            {
                if (db == name)
                {
                    MessageBox.Show("There is already a database with the given name!");
                    return;
                }
            }
            using (StreamWriter file = new StreamWriter(databaseDirectory + "/databaseList" + fileExtension, true))
            {
                file.WriteLine(name);
            }
            MessageBox.Show("Success!");
            Directory.CreateDirectory("databases/" + name);
            using (FileStream fs3 = File.Create("databases/" + name + "/todoList" + fileExtension)) ;
        }
        public static void DeleteDatabase(string databaseName)
        {
            string[] dbs = System.IO.File.ReadAllLines("databases/databaseList.txt");
            if (databaseName == GetCurrentDB())
            {
                MessageBox.Show("Switch to another database before deleting the current one.");
                return;
            }
            using (StreamWriter file = new StreamWriter(databaseDirectory + "/databaseList" + fileExtension, false))
            {
                foreach (string db in dbs)
                {
                    if (db != databaseName)
                    {
                        file.WriteLine(db);
                    }
                }
            }
            if (Directory.Exists("databases/" + databaseName))
                Directory.Delete("databases/" + databaseName, true);
            else
            {
                MessageBox.Show("Database not found with the given name");
                return;
            }
            MessageBox.Show("Success!");
        }
    


        public static string[] GetDatabaseList()
        {
            if (File.Exists("databases/databaseList.txt"))
            {
                return System.IO.File.ReadAllLines("databases/databaseList.txt");
            }
            return null;
        }

        private static void ChangeCurrentDB(string name)
        {
            
            using (StreamWriter file = new StreamWriter(databaseDirectory + "/currentDatabase" + fileExtension, false))
            {
                file.WriteLine(name);
            }
        }

        public static string GetCurrentDB()
        {
            string[] data = System.IO.File.ReadAllLines("databases/currentDatabase.txt");
            if (data.Count() > 0)
                return data[0];
            return "";
        }
        public static void AddCategory(string name)
        {
            string[] categories = System.IO.File.ReadAllLines("databases/categoryList.txt");
            foreach (string cate in categories)
            {
                if (cate == name)
                {
                    MessageBox.Show("There is already a category with the given name!");
                    return;
                }
            }
            using (StreamWriter file = new StreamWriter(databaseDirectory + "/categoryList" + fileExtension, true))
            {
                file.WriteLine(name);
            }
        }

        public static void RemoveCategory(string name)
        {
            string[] categories = System.IO.File.ReadAllLines("databases/categoryList.txt");
      
            using (StreamWriter file = new StreamWriter(databaseDirectory + "/categoryList" + fileExtension, false))
            {
                foreach (string cate in categories)
                {
                    if (cate != name) 
                        file.WriteLine(cate);
                }
            }
        }
        public static string[] GetCategoryList()
        {
            if (File.Exists("databases/categoryList.txt"))
            {
                return System.IO.File.ReadAllLines("databases/categoryList.txt");
            }
            return null;
        }
    }
}
