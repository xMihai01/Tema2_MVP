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
using Tema2_MVP.ViewModels;

namespace Tema2_MVP.Views
{
    /// <summary>
    /// Interaction logic for InputTaskWindow.xaml
    /// </summary>
    public partial class InputTaskWindow : Window
    {
        public TaskVM taskVM { get; set; }
        public InputTaskWindow()
        {
            InitializeComponent();
            taskVM= new TaskVM();
            DataContext = taskVM;
        }
        private void SaveTask(object sender, RoutedEventArgs e)
        {
            if (taskVM.CheckInformations())
                this.DialogResult = true;
        }

    }
}
