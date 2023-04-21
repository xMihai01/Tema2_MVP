using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tema2_MVP.Commands;
using Tema2_MVP.Models;

namespace Tema2_MVP.ViewModels
{
    public class TaskVM : INotifyPropertyChanged
    {
        public string _priority;

        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }
        public List<string> priorityList { get; set; } = new List<string> { "High", "Medium", "Low" };
        public string _category;

        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
            }
        }
        public List<string> categoryList { get; set; } = new List<string> { "School", "Work", "Sport", "Other" };

        public string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        public string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public List<string> statusList { get; set; } = new List<string> { "Created", "In Progress", "Done" };
        public TaskVM()
        {
            Description = "No description provided.";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool CheckInformations()
        {
            if (Name == "" || Name == null)
            {
                MessageBox.Show("Name cannot be empty");
                return false;
            }
            if (Description == "" || Description == null)
            {
                MessageBox.Show("Description cannot be empty");
                return false;
            }
            if (Priority == null)
            {
                MessageBox.Show("Priority must be chosen");
                return false;
            }
            if (Category == null)
            {
                MessageBox.Show("Category must be chosen");
                return false;
            }
            return true;
        }

    }
}
