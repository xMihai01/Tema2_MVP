using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema2_MVP.Models;

namespace Tema2_MVP.Utils
{
    internal class TreeUtils
    {
        public string GetPathFromTreeNode(Node fromNode)
        {
            string path = "";
            while (fromNode != null)
            {
                path += fromNode.Text + "/";
                fromNode = fromNode.lastNode;
            }
            return path;
        }
    }
}
