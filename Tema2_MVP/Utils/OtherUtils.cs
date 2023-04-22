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
    }
}
