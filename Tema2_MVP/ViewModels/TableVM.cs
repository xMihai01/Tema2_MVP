using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Tema2_MVP.Commands;
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
        private Tema2_MVP.Models.Task _selectedTask;
        public Tema2_MVP.Models.Task SelectedTask { get { return _selectedTask; } set { _selectedTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTask)));
                if (_selectedTask != null)
                    Description = _selectedTask.description;
            } }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        public TableVM()
        {

        }
        public void UpdateTable(Node atNode)
        {
            Items = atNode.tasks;
        }

    }
}
