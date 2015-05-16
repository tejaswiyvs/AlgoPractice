using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode;
using LeetCode.DataStructures;

namespace LeetCode.Driver
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph();

            GraphNode n1 = new GraphNode();
            n1.Data = 1;

            GraphNode n2 = new GraphNode();
            n2.Data = 2;

            GraphNode n3 = new GraphNode();
            n3.Data = 3;

            GraphNode n4 = new GraphNode();
            n4.Data = 4;

            GraphNode n5 = new GraphNode();
            n5.Data = 5;

            GraphNode n6 = new GraphNode();
            n6.Data = 6;

            n1.Neighbors = new GraphNode[] { n2, n3, n4};
            n2.Neighbors = new GraphNode[] { n1, n5, n6 };
            n3.Neighbors = new GraphNode[] { n1, n6};
            n4.Neighbors = new GraphNode[] { n1, n6 };
            n5.Neighbors = new GraphNode[] { n2 };
            n6.Neighbors = new GraphNode[] { n2, n3, n4 };

            g.DfsRecurse(n1, 6, new HashSet<GraphNode>());
        }
    }
}
