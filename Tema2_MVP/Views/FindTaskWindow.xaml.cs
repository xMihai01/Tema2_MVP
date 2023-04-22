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
    /// Interaction logic for FindTaskWindow.xaml
    /// </summary>
    public partial class FindTaskWindow : Window
    {
        public FindTasksVM findTasksVM { get; set; }
        public FindTaskWindow()
        {
            InitializeComponent();
            findTasksVM = new FindTasksVM();
            DataContext = findTasksVM;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FindButtonClick(object sender, RoutedEventArgs e)
        {
            findTasksVM.UpdateItems();
        }
    }
}
