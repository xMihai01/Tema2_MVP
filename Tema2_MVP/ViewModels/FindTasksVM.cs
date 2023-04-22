using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MVP.Utils;

namespace Tema2_MVP.ViewModels
{
    public class FindTasksVM : INotifyPropertyChanged
    {
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
        public string _findName;

        public string FindName
        {
            get { return _findName; }
            set
            {
                _findName = value;
                OnPropertyChanged(nameof(FindName));
            }
        }
        
        public string _foundTasksNumber;

        public string FoundTasksNumber
        {
            get { return _foundTasksNumber; }
            set
            {
                _foundTasksNumber = value;
                _foundTasksNumber += " found tasks";
                OnPropertyChanged(nameof(FoundTasksNumber));
   
            }
        }

        public string _findBy;

        public string FindBy
        {
            get { return _findBy; }
            set
            {
                _findBy = value;
                OnPropertyChanged(nameof(FindBy));
                if (_findBy == "Name")
                {
                    Console.WriteLine(FindBy);
                    IsTextBoxVisible = true;
                    IsDateVisible = false;
                }
                else
                {
                    Console.WriteLine(FindBy);
                    IsTextBoxVisible = false;
                    IsDateVisible = true;
                }
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
        public List<string> findByList { get; set; } = new List<string> { "Name", "Deadline"};

        private ObservableCollection<Tema2_MVP.Models.TaskDetail> _items;
        public ObservableCollection<Tema2_MVP.Models.TaskDetail> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }
        private bool _isTextBoxVisible;
        public bool IsTextBoxVisible
        {
            get { return _isTextBoxVisible; }
            set
            {
                _isTextBoxVisible = value;
                OnPropertyChanged(nameof(IsTextBoxVisible));
            }
        }
        private bool _isDateVisible;
        public bool IsDateVisible
        {
            get { return _isDateVisible; }
            set
            {
                _isDateVisible = value;
                OnPropertyChanged(nameof(IsDateVisible));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public FindTasksVM()
        {
            IsDateVisible = false;
            IsTextBoxVisible = true;
            FindBy = "Name";
            FoundTasksNumber = "0";
        }
        public void UpdateItems()
        {
            if (FindBy == "Name")
            {
                Items = OtherUtils.SearchTasksAndConvertToTaskDetail(FindName);
            } else
            {
                Items = OtherUtils.SearchTasksAndConvertToTaskDetail(Date.ToString("MM/dd/yyyy"));
            }
            FoundTasksNumber = Items.Count.ToString();
        }
    }
}
