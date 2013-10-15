using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Project03
{
    class NodeComparer:IComparer
    {
        public NodeComparer()
        {
            
        }
        
        public int Compare(object x, object y)
        {
            return ((Node)x).totalCost - ((Node)y).totalCost;
        }


    }
}
