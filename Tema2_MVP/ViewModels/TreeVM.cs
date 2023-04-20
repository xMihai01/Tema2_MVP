using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema2_MVP.Commands;
using Tema2_MVP.Models;
using Tema2_MVP.Views;
using static Tema2_MVP.Utils.FileUtils;
using static Tema2_MVP.Utils.TreeUtils;

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
        public Node SelectedItem { get; set; }

        public TreeVM()
        {
            Node node = new Node
            {
                Text = ContainerVM.database.name,
                Children = ContainerVM.database.nodes

            };
            RootNode = node;

        }

        private void UpdateTree()
        {
            ContainerVM.database = GetDatabaseDetailsFromFile(GetCurrentDB());
            Node node = new Node
            {
                Text = ContainerVM.database.name,
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
        public ICommand CreateDatabaseItemCommand => new RelayCommand(CreateDatabaseItem);
        public ICommand AddRootTDLItemCommand => new RelayCommand(AddRootTDLItem);
        public ICommand AddTDLItemCommand => new RelayCommand(AddTDLItem);
        public ICommand DeleteTDLItemCommand => new RelayCommand(DeleteTDLItem);
        public ICommand EditTDLItemCommand => new RelayCommand(EditTDLItem);
        public ICommand MoveUpTDLItemCommand => new RelayCommand(MoveUpTDLItem);
        public ICommand MoveDownTDLItemCommand => new RelayCommand(MoveDownTDLItem);
        public ICommand ChangePathRootItemCommand => new RelayCommand(ChangePathRootItem);

        public void OpenDatabaseItem()
        {
            InputWindow inputDialog = new InputWindow("Please enter database name:", "mihai", GetDatabaseList());
            if (inputDialog.ShowDialog() == true)
                ContainerVM.database = GetDatabaseDetailsFromFile(inputDialog.Answer);
            UpdateTree();
        }
        public void CreateDatabaseItem()
        {
            InputWindow inputDialog = new InputWindow("Please enter new database name:", "some_database", GetDatabaseList());
            if (inputDialog.ShowDialog() == true)
                CreateDatabase(inputDialog.Answer);
        }
        public void AddRootTDLItem()
        {
            InputWindow inputDialog = new InputWindow("Please enter new Root-TDL name:", "name");

            if (inputDialog.ShowDialog() == true)
                AddRootTDL(inputDialog.Answer);
            UpdateTree();
        }
        public void AddTDLItem()
        {
            if (SelectedItem == null) {
                MessageBox.Show("Select a TDL first.");
                return; }

            InputWindow inputDialog = new InputWindow("Selected TDL: " + SelectedItem.Text, "name");
            if (inputDialog.ShowDialog() == true)
                AddTDL(SelectedItem, inputDialog.Answer);
            UpdateTree();
        }

        public void DeleteTDLItem()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            DeleteTDL(SelectedItem, true);
            UpdateTree();
        }
        public void EditTDLItem()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }

            InputWindow inputDialog = new InputWindow("Selected TDL: " + SelectedItem.Text, "name");
            if (inputDialog.ShowDialog() == true)
                EditTDL(SelectedItem, inputDialog.Answer);
            UpdateTree();
        }
        public void MoveUpTDLItem()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            MoveTDL(SelectedItem, true); UpdateTree();
        }
        public void MoveDownTDLItem()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            MoveTDL(SelectedItem, false); UpdateTree();
        }
        public void ChangePathRootItem()
        {
            if (SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            ChangePathRoot(SelectedItem); UpdateTree();
        }

    }
}
