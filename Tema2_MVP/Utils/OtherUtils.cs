using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MVP.Models;
using Tema2_MVP.ViewModels;

namespace Tema2_MVP.Utils
{
    public class OtherUtils
    {
        public static ObservableCollection<TaskDetail> SearchTasksAndConvertToTaskDetail(string query)
        {
            ObservableCollection<TaskDetail> foundTasks = new ObservableCollection<TaskDetail>();
            ObservableCollection<Node> nodes = ContainerVM.database.nodes;
            Queue<Node> queue = new Queue<Node>();

            foreach (Node node in nodes)
            {
                queue.Enqueue(node);
            }

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();
                foreach (Node node in currentNode.Children)
                {
                    queue.Enqueue(node);
                }
                foreach(Models.Task task in currentNode.tasks)
                {
                    if (task.name == query || task.deadline.ToString("MM/dd/yyyy") == query)
                    {
                        foundTasks.Add(new TaskDetail(task, ReadablePathWithoutBase(TreeUtils.GetPathFromTreeNode(currentNode))));
                    }
                }
            }
            return foundTasks;
        }

        public static string MakeStatisticsMessage()
        {
            int dueToday = 0; int dueTomorrow = 0; int overdue = 0; int done = 0; int toBeDone = 0; int allTasks = 0;
            ObservableCollection<Node> nodes = ContainerVM.database.nodes;
            Queue<Node> queue = new Queue<Node>();

            foreach (Node node in nodes)
            {
                queue.Enqueue(node);
            }

            while (queue.Count > 0)
            {
                Node currentNode = queue.Dequeue();
                foreach (Node node in currentNode.Children)
                {
                    queue.Enqueue(node);
                }
                foreach (Models.Task task in currentNode.tasks)
                {
                    if (task.status != "Done" && DateTime.Compare(task.deadline, DateTime.Today) == 0)
                        dueToday++;
                    if (task.status != "Done" && DateTime.Compare(task.deadline, DateTime.Today.AddDays(1)) == 0)
                        dueTomorrow++;
                    if (task.status != "Done" && DateTime.Compare(task.deadline, DateTime.Today) < 0)
                        overdue++;
                    if (task.status == "Done")
                        done++;
                    if (task.status != "Done")
                        toBeDone++;
                    allTasks++;
                }
            }
            return "Tasks due today: " + dueToday + "\nTasks due tomorrow: " + dueToday
                + "\nTasks overdue: " + overdue + "\n\nTasks done: " + done + "\nTasks to be done: " + toBeDone + "\nTotal tasks: " + allTasks;
        }

        public static string ReadablePathWithoutBase(string path)
        {
            string[] locations = path.Split('/');
            string readablePath = "";
            for (int i = 2; i<locations.Length; i++)
            {
                readablePath = readablePath + locations[i] + " > ";
            }
            readablePath = readablePath.Remove(readablePath.Length - 5);
            return readablePath;
        }

        public static ObservableCollection<Models.Task> SortByPriority(Node node)
        {
            for (int i = 0; i<node.tasks.Count-1; i++) { 
                for (int j = 0; j<node.tasks.Count-i-1; j++)
                {
                    if (GetPriorityValueFromString(node.tasks[j].priority) > GetPriorityValueFromString(node.tasks[j+1].priority))
                    {
                        Models.Task temp = node.tasks[j];
                        node.tasks[j] = node.tasks[j + 1];
                        node.tasks[j + 1] = temp;
                    }
                }
            }
            return node.tasks;
        }
        public static ObservableCollection<Models.Task> SortByDeadline(Node node)
        {
            for (int i = 0; i < node.tasks.Count - 1; i++)
            {
                for (int j = 0; j < node.tasks.Count - i - 1; j++)
                {
                    if (DateTime.Compare(node.tasks[j].deadline, node.tasks[j+1].deadline) > 0)
                    {
                        Models.Task temp = node.tasks[j];
                        node.tasks[j] = node.tasks[j + 1];
                        node.tasks[j + 1] = temp;
                    }
                }
            }
            return node.tasks;
        }

        public static ObservableCollection<Models.Task> FilterByCategory(Node node, string category)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in node.tasks)
            {
                if (category == task.category)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        public static ObservableCollection<Models.Task> FilterByFinishedTask(Node node)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in node.tasks)
            {
                if (task.status == "Done")
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        public static ObservableCollection<Models.Task> FilterByOverdueTask(Node node)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in node.tasks)
            {
                if (task.status == "Done" && DateTime.Compare(task.deadline, DateTime.Now) < 0)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        public static ObservableCollection<Models.Task> FilterByOverdueNotFinished(Node node)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in node.tasks)
            {
                if (task.status != "Done" && DateTime.Compare(task.deadline, DateTime.Now) < 0)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }
        public static ObservableCollection<Models.Task> FilterByFutureDeadlineNotFinished(Node node)
        {
            ObservableCollection<Models.Task> tasks = new ObservableCollection<Models.Task>();
            foreach (Models.Task task in node.tasks)
            {
                if (task.status != "Done" && DateTime.Compare(task.deadline, DateTime.Now) > 0)
                {
                    tasks.Add(task);
                }
            }
            return tasks;
        }

        private static int GetPriorityValueFromString(string priority)
        {
            switch(priority)
            {
                case "High":
                    return 3;
                case "Medium":
                    return 2;
                case "Low":
                    return 1;
            }
            return 0;
        }
    }
}
