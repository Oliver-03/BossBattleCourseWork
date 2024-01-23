using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BossBattleCourseWork
{
    public class Edge
    {
        static int _nextID = 0;
        public int From { get; private set; }
        public int To { get; private set; }

        public int ID { get; private set; }

        public Edge(int pFrom, int pTo)
        {
            From = pFrom;
            To = pTo;
            ID = _nextID;
            _nextID++;
        }
    }
}
