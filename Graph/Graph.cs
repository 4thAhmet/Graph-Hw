using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    internal class Graph
    {
        public string label;
        public int counter;
        public int plaka;
        public Graph[] Komsular=new Graph[100];
        public Graph next;
    }
}
