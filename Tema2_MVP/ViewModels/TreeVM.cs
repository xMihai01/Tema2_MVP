using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema2_MVP.Commands;
using Tema2_MVP.Models;
using Tema2_MVP.Views;
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
        public object SelectedItem { get; set; }

        public TreeVM()
        {
            Node node = new Node
            {
                Text = "Database",
                Children = ContainerVM.database.nodes

            };
            RootNode = node;

        }

        private void UpdateTree()
        {
            Node node = new Node
            {
                Text = "Database",
                Children = ContainerVM.database.nodes

            };
            RootNode = node;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand OpenDatabaseItemCommand => new RelayCommand(OpenDatabaseItem);

        public void OpenDatabaseItem()
        {
            InputWindow inputDialog = new InputWindow("Please enter database name:", "mihai", GetDatabaseList());
            if (inputDialog.ShowDialog() == true)
                ContainerVM.database = GetDatabaseDetailsFromFile(inputDialog.Answer);
            UpdateTree();

            // Change currentDatabase.txt

        }

    }
}
