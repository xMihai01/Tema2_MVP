using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema2_MVP.Commands;
using Tema2_MVP.Models;
using Tema2_MVP.Utils;
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
        public ICommand EditTaskItemCommand => new RelayCommand(EditTaskItem);
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
                TaskUtils.SaveTaskToFile(tree.SelectedItem, new Models.Task(input.taskVM.Name, input.taskVM.Description, input.taskVM.Category,
                    "Created", input.taskVM.Priority, input.taskVM.Date));
                table.UpdateTable(tree.SelectedItem); MessageBox.Show("Succes!");
            }
            
        }
        public void EditTaskItem()
        {
            if (table.SelectedTask == null || tree.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            InputTaskWindow input = new InputTaskWindow(table.SelectedTask);
            if (input.ShowDialog() == true)
            {
                Models.Task task;
                if (table.SelectedTask.name != input.taskVM.Name)
                {
                    TaskUtils.RenameTask(tree.SelectedItem, table.SelectedTask, input.taskVM.Name);
                }
                if (table.SelectedTask.status != "Done" && input.taskVM.Status == "Done")
                {
                    task = new Models.Task(input.taskVM.Name, input.taskVM.Description, input.taskVM.Category,
                    "Done", input.taskVM.Priority, input.taskVM.Date, DateTime.Now.ToString("MM/dd/yyyy"));
                }
                else if (table.SelectedTask.status == "Done" && input.taskVM.Status != "Done")
                {
                    task = new Models.Task(input.taskVM.Name, input.taskVM.Description, input.taskVM.Category,
                    input.taskVM.Status, input.taskVM.Priority, input.taskVM.Date);
                }
                else if (table.SelectedTask.status == "Done" && input.taskVM.Status == "Done")
                {
                    task = new Models.Task(input.taskVM.Name, input.taskVM.Description, input.taskVM.Category,
                    "Done", input.taskVM.Priority, input.taskVM.Date, table.SelectedTask.finishDate);
                }
                else
                {
                    task = new Models.Task(input.taskVM.Name, input.taskVM.Description, input.taskVM.Category,
                    input.taskVM.Status, input.taskVM.Priority, input.taskVM.Date);
                }
                TaskUtils.SaveTaskToFile(tree.SelectedItem, task, false);
                tree.SelectedItem.tasks[tree.SelectedItem.tasks.IndexOf(table.SelectedTask)] = task;
                table.SelectedTask = task;
                table.UpdateTable(tree.SelectedItem); MessageBox.Show("Succes!");
            }
        }
    }
}
