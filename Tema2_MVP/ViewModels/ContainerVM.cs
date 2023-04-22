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

namespace Tema2_MVP.ViewModels
{
    public class ContainerVM
    {
        public static Database database = FileUtils.GetDatabaseDetailsFromFile(FileUtils.GetCurrentDB());
        public TableVM table { get; set; }
        public TreeVM tree { get; set; }

        public ContainerVM()
        {
            table = new TableVM();
            tree = new TreeVM();
        }

        public ICommand AddTaskItemCommand => new RelayCommand(AddTaskItem);
        public ICommand EditTaskItemCommand => new RelayCommand(EditTaskItem);
        public ICommand DeleteTaskItemCommand => new RelayCommand(DeleteTaskItem);
        public ICommand SetDoneTaskItemCommand => new RelayCommand(SetDoneTaskItem);
        public ICommand MoveTaskUpItemCommand => new RelayCommand(MoveTaskUpItem);
       
        public ICommand MoveTaskDownItemCommand => new RelayCommand(MoveTaskDownItem);
        public ICommand ManageCategoryAddItemCommand => new RelayCommand(ManageCategoryAddItem);
        public ICommand ManageCategoryRemoveItemCommand => new RelayCommand(ManageCategoryRemoveItem);
        public ICommand FindTasksItemCommand => new RelayCommand(FindTasksItem);
        public ICommand SortTasksDeadlineItemCommand => new RelayCommand(SortTasksDeadlineItem);
        public ICommand SortTasksPriorityItemCommand => new RelayCommand(SortTasksPriorityItem);
        public ICommand FilterByCategoryItemCommand => new RelayCommand(FilterByCategoryItem);
        public ICommand FilterByFinishedItemCommand => new RelayCommand(FilterByFinishedItem);
        public ICommand FilterByOverdueItemCommand => new RelayCommand(FilterByOverdueItem);
        public ICommand FilterByOverdueNotFinishedItemCommand => new RelayCommand(FilterByOverdueNotFinishedItem);
        public ICommand FilterByFutureDeadlineItemCommand => new RelayCommand(FilterByFutureDeadlineItem);
        public ICommand ShowStatisticsItemCommand => new RelayCommand(ShowStatisticsItem);
        public ICommand ShowAboutItemCommand => new RelayCommand(ShowAboutItem);
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
                UpdateTaskFromFile(input);
                table.UpdateTable(tree.SelectedItem); MessageBox.Show("Succes!");
            }
        }
        public void DeleteTaskItem()
        {
            if (table.SelectedTask == null || tree.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            InputTaskWindow input = new InputTaskWindow(table.SelectedTask, true, "Delete");
            if (input.ShowDialog() == true)
            {
                TaskUtils.DeleteTask(tree.SelectedItem, table.SelectedTask);
                table.UpdateTable(tree.SelectedItem); MessageBox.Show("Succes!");
            }
        }
        public void SetDoneTaskItem()
        {
            if (table.SelectedTask == null || tree.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            InputTaskWindow input;
            if (table.SelectedTask.status != "Done")
            {
                input = new InputTaskWindow(table.SelectedTask, true, "Set as done");
                input.taskVM.Status = "Done";
            } else
            {
                input = new InputTaskWindow(table.SelectedTask, true, "Set as UNdone");
                input.taskVM.Status = "In Progress";
            }
            if (input.ShowDialog() == true)
            {
                UpdateTaskFromFile(input);
                table.UpdateTable(tree.SelectedItem); MessageBox.Show("Succes!");
            }
        }
        public void MoveTaskUpItem() {
            if (table.SelectedTask == null || tree.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            TaskUtils.MoveTask(tree.SelectedItem, table.SelectedTask, true);
            tree.SelectedItem.tasks = TaskUtils.GetTasksForTDL(TreeUtils.GetPathFromTreeNode(tree.SelectedItem.lastNode), tree.SelectedItem.Text);
            table.UpdateTable(tree.SelectedItem);
        }
        public void MoveTaskDownItem()
        {
            if (table.SelectedTask == null || tree.SelectedItem == null)
            {
                MessageBox.Show("Select a task first.");
                return;
            }
            TaskUtils.MoveTask(tree.SelectedItem, table.SelectedTask, false);
            tree.SelectedItem.tasks = TaskUtils.GetTasksForTDL(TreeUtils.GetPathFromTreeNode(tree.SelectedItem.lastNode), tree.SelectedItem.Text);
            table.UpdateTable(tree.SelectedItem);
        }
        public void ManageCategoryAddItem()
        {
            InputWindow input = new InputWindow("Please enter category name:", "category", FileUtils.GetCategoryList());
            if (input.ShowDialog() == true)
            {
                FileUtils.AddCategory(input.Answer);
            }
        }
        public void ManageCategoryRemoveItem()
        {
            InputWindow input = new InputWindow("Please enter category name:", "category", FileUtils.GetCategoryList());
            if (input.ShowDialog() == true)
            {
                FileUtils.RemoveCategory(input.Answer);
            }
        }

        public void FindTasksItem()
        {
            FindTaskWindow window = new FindTaskWindow();
            window.ShowDialog();
        }
        

        public void SortTasksPriorityItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            tree.SelectedItem.tasks = OtherUtils.SortByPriority(tree.SelectedItem);
            table.UpdateTable(tree.SelectedItem);

        }
        public void SortTasksDeadlineItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            tree.SelectedItem.tasks = OtherUtils.SortByDeadline(tree.SelectedItem);
            table.UpdateTable(tree.SelectedItem);
        }

        public void FilterByCategoryItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            InputWindow input = new InputWindow("Enter category name", "category", FileUtils.GetCategoryList());
            if (input.ShowDialog() == true)
                table.UpdateTable(tree.SelectedItem, OtherUtils.FilterByCategory(tree.SelectedItem, input.Answer));
        }
        public void FilterByFinishedItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            table.UpdateTable(tree.SelectedItem, OtherUtils.FilterByFinishedTask(tree.SelectedItem));
        }
        public void FilterByOverdueItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            table.UpdateTable(tree.SelectedItem, OtherUtils.FilterByOverdueTask(tree.SelectedItem));
        }
        public void FilterByOverdueNotFinishedItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            table.UpdateTable(tree.SelectedItem, OtherUtils.FilterByOverdueNotFinished(tree.SelectedItem));
        }
        public void FilterByFutureDeadlineItem()
        {
            if (tree.SelectedItem == null)
            {
                MessageBox.Show("Select a TDL first.");
                return;
            }
            table.UpdateTable(tree.SelectedItem, OtherUtils.FilterByFutureDeadlineNotFinished(tree.SelectedItem));
        }

        public void ShowStatisticsItem()
        {
            table.StatisticsBox = OtherUtils.MakeStatisticsMessage();
        }

        private void UpdateTaskFromFile(InputTaskWindow input)
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
        }
        public void ShowAboutItem()
        {
            MessageBox.Show("Doloiu Mihai-Alexandru\n10LF311\nmihai.doloiu@student.unitbv.ro");
        }
    }
}
