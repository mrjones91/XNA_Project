using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Project03
{
    public class Node:IComparable
    {
        public int g;
        public int h;
        public int x;
        public int y;
        public int xpo;
        public int ypo;

        public Node parentNode;
        private Node _goalNode;
        private int gCost;

        public int totalCost
        {
            get
            {
                return g + h;
            }
            set
            {
                totalCost = value;
            }
        }

        public Node()
        {
            this.parentNode = null;
            this._goalNode = null;
            InitNode();
        }
        public Node(Node parentNode, Node goalNode, int gCost, int x, int y)
        {
            this.parentNode = parentNode;
            this._goalNode = goalNode;
            this.gCost = gCost;
            this.x = x;
            this.y = y;
            InitNode();
            xpo = 32 * this.x;
            ypo = 32 * this.y;
        }

        private void InitNode()
        {
            this.g = (parentNode != null) ? this.parentNode.g + gCost : gCost;
            this.h = (_goalNode != null) ? (int)Euclidean_H() : 0;
        }

        private double Euclidean_H()
        {
            double xd = this.x - this._goalNode.x;
            double yd = this.y - this._goalNode.y;

            return Math.Sqrt((xd * xd) + (yd * yd));
        }

        public int CompareTo(object obj)
        {
            Node n = (Node)obj;
            int cFactor = this.totalCost - n.totalCost;
            return cFactor;
        }

        public bool isMatch(Node n)
        {
            if (n != null)
                return (x == n.x && y == n.y);
            else
                return false;
        }

        public bool roomNode(int x, int y)
        {
            if (x == 5 && y == 35)
                return true;
            else if (x == 5 && y == 5)
                return true;
            else if (x == 30 && y == 5)
                return true;
            else if (x == 35 && y == 32)
                return true;
            else
                return false;

        }

        public bool isCardinal(int xp, int yp)
        {
            // return false if it is a corner node
            //return true otherwise
            if (xp == x + 1 && yp == y + 1)
                return false;
            else if (xp == x + 1 && yp == y - 1)
                return false;
            else if (xp == x - 1 && yp == y + 1)
                return false;
            else if (xp == x - 1 && yp == y - 1)
                return false;
            else
                return true;

            //if (node_successor.x == node_current.x + 1 && node_successor.y == node_current.y)
            //    OPEN.push(node_successor);
            //else if (node_successor.x == node_current.x - 1 && node_successor.y == node_current.y)
            //    OPEN.push(node_successor);
            //else if (node_successor.x == node_current.x && node_successor.y == node_current.y + 1)
            //    OPEN.push(node_successor);
            //else if (node_successor.x == node_current.x && node_successor.y == node_current.y - 1)
            //    OPEN.push(node_successor);
            //else
            //    continue;
        }

        public ArrayList GetSuccessors()
        {
            ArrayList successors = new ArrayList();

            for(int xd =-1; xd <= 1; xd++)
            {
                for (int yd = -1; yd <= 1; yd++)
                {
                    if (Map.getMap (x + xd, y + yd) != -1 && isCardinal(x+xd, y+yd))
                    {
                        Node n = new Node(this, this._goalNode, Map.getMap(x + xd, y + yd), x + xd, y + yd);
                        if (!n.isMatch(this.parentNode) && !n.isMatch(this))
                            successors.Add(n);
                    }
                }// for yd
            }// for xd
            return successors;
        }// ArrayList GetSuccessors()
    }//Class
}//namespace
