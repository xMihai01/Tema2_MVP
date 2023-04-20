using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Xml.Linq;
using Tema2_MVP.Models;
using Tema2_MVP.ViewModels;

namespace Tema2_MVP.Utils
{
    internal class TreeUtils
    {
        public const string fileExtension = ".txt";
        private static string GetPathFromTreeNode(Node fromNode)
        {
            string path = "databases/" + ContainerVM.database.name + "/";
            Stack<string> stack = new Stack<string>();
            while (fromNode != null)
            {
                stack.Push(fromNode.Text);
                fromNode = fromNode.lastNode;
            }
            while (stack.Any())
                path += stack.Pop() + "/";
            Console.WriteLine(path);
            return path;
        }

        public static void AddRootTDL(string name)
        {
            string dbName = ContainerVM.database.name;
            string[] tdls = System.IO.File.ReadAllLines("databases/" + dbName + "/todoList.txt");

            if (CheckForDuplicateTDL(tdls, name))
            {
                MessageBox.Show("There is already a RootTDL with the given name!");
                return;
            }

            using (StreamWriter file = new StreamWriter("databases/" + dbName + "/todoList.txt", true))
            {
                file.WriteLine(name);
            }
            Directory.CreateDirectory("databases/" + dbName + "/" + name);
            using (FileStream fs3 = File.Create("databases/" + dbName + "/" + name + "/todoList" + fileExtension)) ;
            using (FileStream fs3 = File.Create("databases/" + dbName + "/" + name + "/tasksList" + fileExtension)) ;
            MessageBox.Show("Success!");
        }
        public static void AddTDL(Node selectedNode, string name)
        {
            string path = GetPathFromTreeNode(selectedNode);
            string dbName = ContainerVM.database.name;
            string[] tdls = System.IO.File.ReadAllLines(path + "/todoList.txt");

            if (CheckForDuplicateTDL(tdls, name))
            {
                MessageBox.Show("There is already a TDL with the given name!");
                return;
            }

            using (StreamWriter file = new StreamWriter(path + "todoList.txt", true))
            {
                file.WriteLine(name);
            }
            Directory.CreateDirectory(path + name);
            using (FileStream fs3 = File.Create(path + name + "/todoList" + fileExtension)) ;
            using (FileStream fs3 = File.Create(path + name + "/tasksList" + fileExtension)) ;
            MessageBox.Show("Success!");
        }

        public static void DeleteTDL(Node selectedNode, bool deleteFiles)
        {
            string path = GetPathFromTreeNode(selectedNode);
            if (deleteFiles)
                Directory.Delete(path, true);
            string[] data = System.IO.File.ReadAllLines(GetPathFromTreeNode(selectedNode.lastNode) + "todoList.txt");
            using (StreamWriter file = new StreamWriter(GetPathFromTreeNode(selectedNode.lastNode) + "todoList.txt", false))
            {
                foreach (string str in data)
                {
                    if (str != selectedNode.Text)
                    {
                        file.WriteLine(str);
                    }
                }
            }
            MessageBox.Show("Success!");
        }
        public static void EditTDL(Node selectedNode, string newName)
        {
            string path = GetPathFromTreeNode(selectedNode);
            string[] data = System.IO.File.ReadAllLines(GetPathFromTreeNode(selectedNode.lastNode) + "todoList.txt");
            using (StreamWriter file = new StreamWriter(GetPathFromTreeNode(selectedNode.lastNode) + "todoList.txt", false))
            {
                foreach (string str in data)
                {
                    if (str != selectedNode.Text)
                    {
                        file.WriteLine(str);
                    } else
                    {
                        file.WriteLine(newName);
                    }
                }
            }
            Directory.Move(path, GetPathFromTreeNode(selectedNode.lastNode) + newName);
            MessageBox.Show("Success!");
        }

        public static void MoveTDL(Node selectedNode, bool moveUp)
        {
            string path = GetPathFromTreeNode(selectedNode.lastNode);
            string[] data = System.IO.File.ReadAllLines(path + "todoList.txt");
            for (int index = 0; index < data.Length; index++)
            {
                if (data[index] == selectedNode.Text)
                    if ((index == 0 && moveUp) || (index == data.Length-1 && !moveUp))
                    {
                        Console.WriteLine(index + "|" + (data.Length - 1) + "|" + moveUp + "|" + data[index]);
                        MessageBox.Show("TDL can't be moved further");
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
                        } else
                        {
                            string temp = data[index];
                            data[index] = data[index + 1];
                            data[index + 1] = temp;
                            break;
                        }

                    }
            }
            using (StreamWriter file = new StreamWriter(path + "todoList.txt", false))
            {
                foreach (string str in data)
                {
                    file.WriteLine(str);
                }
            }
            MessageBox.Show("Success!");
        }

        public static void ChangePathRoot(Node selectedNode)
        {
            string path = GetPathFromTreeNode(selectedNode.lastNode);
            string dbName = ContainerVM.database.name;
            using (StreamWriter file = new StreamWriter("databases/" + dbName + "/todoList.txt", true))
            {
                file.WriteLine(selectedNode.Text);
            }
            Directory.Move(GetPathFromTreeNode(selectedNode), "databases/" + dbName +"/" + selectedNode.Text);
            DeleteTDL(selectedNode, false);

        }

        private static bool CheckForDuplicateTDL(string[] list, string name)
        {
            foreach (string tdl in list)
                if (tdl == name)
                    return true;
            return false;
        }
    }
}
