using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema2_MVP.Models
{
    public class Database
    {
        public string name { get; set; }
        public ObservableCollection<Node> nodes { get; set; }
        public Database() { 
            nodes = new ObservableCollection<Node>();
        }
    }
}
