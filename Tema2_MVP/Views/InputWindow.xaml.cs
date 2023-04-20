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

namespace Tema2_MVP.Views
{
    /// <summary>
    /// Interaction logic for InputWindow.xaml
    /// </summary>
    public partial class InputWindow : Window
    {
        string[] list;
        public InputWindow(string question, string defaultAnswer = "", string[] list = null)
        {
            InitializeComponent();
            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
            this.list = list;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }
        private void btnListClick(object sender, RoutedEventArgs e)
        {
            string listAsMessage = "";
            foreach (string item in list)
            {
                listAsMessage = listAsMessage + item + "\n";
            }
            MessageBox.Show(listAsMessage);
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
