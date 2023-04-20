using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tema2_MVP.Models;
using static Tema2_MVP.Utils.FileUtils;

namespace Tema2_MVP.ViewModels
{
    public class ContainerVM
    {
        public static Database database = GetDatabaseDetailsFromFile("mihai");
        public TableVM table { get; set; }
        public TreeVM tree { get; set; }

        public ContainerVM()
        {
            table = new TableVM();
            tree = new TreeVM();
        }
    }
}
