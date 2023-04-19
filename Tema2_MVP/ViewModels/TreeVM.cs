using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MVP.Models;
using static Tema2_MVP.Utils.FileUtils;

namespace Tema2_MVP.ViewModels
{
    public class TreeVM : INotifyPropertyChanged
    {
        public Node _rootNode;

        public Node RootNode
        {
            get { return _rootNode; }
            set
            {
                _rootNode = value;
                OnPropertyChanged(nameof(RootNode));
            }
        }

        public TreeVM()
        {
            Database db = GetDatabaseDetailsFromFile("mihai");
            Console.WriteLine(db.nodes.ElementAt(0).Children.ElementAt(0).Text);
            Node node = new Node
            {
                Text = "Database",
                Children = db.nodes
            };
            RootNode = node;
                /* Children = new ObservableCollection<Node>
             {
                 new Node { Text = "Child 1" ,
                 Children = new ObservableCollection<Node> {
                 new Node {Text="Child7318"} }},
                 new Node { Text = "Child 2" }
             }*/
       
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
