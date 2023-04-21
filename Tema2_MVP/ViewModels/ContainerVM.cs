using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema2_MVP.Commands;
using Tema2_MVP.Models;
using Tema2_MVP.Views;
using static Tema2_MVP.Utils.FileUtils;

namespace Tema2_MVP.ViewModels
{
    public class ContainerVM
    {
        public static Database database = GetDatabaseDetailsFromFile(GetCurrentDB());
        public TableVM table { get; set; }
        public TreeVM tree { get; set; }

        public ContainerVM()
        {
            table = new TableVM();
            tree = new TreeVM();
        }

        public ICommand AddTaskItemCommand => new RelayCommand(AddTaskItem);
        public void AddTaskItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            InputTaskWindow input = new InputTaskWindow();
            if (input.ShowDialog() == true)
            {
                Console.WriteLine(input.taskVM.Name);
            }
        }
    }
}
