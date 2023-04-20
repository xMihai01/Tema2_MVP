using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tema2_MVP.Models;
using static Tema2_MVP.Utils.FileUtils;

namespace Tema2_MVP.ViewModels
{
    public class TableVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Tema2_MVP.Models.Task> _items;
        public ObservableCollection<Tema2_MVP.Models.Task> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
        }

        public TableVM()
        {

            Items = ContainerVM.database.nodes.ElementAt(1).Children.ElementAt(0).tasks;
    
        }
    }
}
