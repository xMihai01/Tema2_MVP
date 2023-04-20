using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tema2_MVP.Models;
using Tema2_MVP.ViewModels;
using static Tema2_MVP.Utils.FileUtils;

namespace Tema2_MVP.Views
{
    /// <summary>
    /// Interaction logic for TasksWindow.xaml
    /// </summary>
    public partial class TasksWindow : Window
    {
        ContainerVM container;
        public TasksWindow()
        {
            InitializeComponent();
            FirstTimeSetUp();
            container = new ContainerVM();
            DataContext = container;
        }

        private void TreeViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeViewItem treeViewItem && treeViewItem.DataContext is Node dataModel)
            {
                container.table.Items = dataModel.tasks;

            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
