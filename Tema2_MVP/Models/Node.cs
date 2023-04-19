using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MVP.Models
{
    public class Node
    {
        public string Text { get; set; }
        public ObservableCollection<Node> Children { get; set; }
        public ObservableCollection<Task> tasks { get; set; }

        public Node()
        {
            Children = new ObservableCollection<Node>();
        }
    }
}
