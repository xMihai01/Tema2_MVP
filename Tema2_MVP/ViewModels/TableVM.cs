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

        private string _aboveTableText;
        public string AboveTableText
        {
            get { return _aboveTableText; }
            set
            {
                if (_aboveTableText != value)
                {
                    _aboveTableText = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AboveTableText)));
                }
            }
        }

        private string _statisticsBox;
        public string StatisticsBox
        {
            get { return _statisticsBox; }
            set
            {
                if (_statisticsBox != value)
                {
                    _statisticsBox = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatisticsBox)));
                }
            }
        }

        public TableVM()
        {
            StatisticsBox = "Stats not loaded yet!\nGo to View -> Statistics to load them!";
        }
        public void UpdateTable(Node atNode, ObservableCollection<Models.Task> tasks = null)
        {
            if (tasks == null)
                Items = atNode.tasks;
            else Items = tasks;

            AboveTableText = "Viewing \"" + atNode.Text + "\" to-do list. " + Items.Count + " tasks shown";
            StatisticsBox = "Stats unloaded!\nGo to View -> Statistics to load them!";
        }

    }
}
