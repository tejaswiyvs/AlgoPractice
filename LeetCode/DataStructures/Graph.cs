using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataStructures
{
    public class Graph
    {
        public void bfs(GraphNode source, int Data) {
            if (source == null) { return; }

            var visited = new HashSet<GraphNode>();
            var queue = new Queue<GraphNode>();
            queue.Enqueue(source);
            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                visited.Add(node);
                // Process
                if (node.Data == Data) {
                    Console.WriteLine("Found!");
                    return;
                }
                foreach (var n in node.Neighbors)
                {
                    if (!visited.Contains(n)) {
                        queue.Enqueue(n);
                    }
                }
            }
            Console.WriteLine("Not Found!");
        }

        public void dfs(GraphNode source, int Data)
        {
            if (source == null) { return; }

            var visited = new HashSet<GraphNode>();
            var queue = new Stack<GraphNode>();
            queue.Push(source);
            while (queue.Count != 0)
            {
                var node = queue.Pop();
                visited.Add(node);
                // Process
                if (node.Data == Data)
                {
                    Console.WriteLine("Found!");
                    return;
                }
                foreach (var n in node.Neighbors)
                {
                    if (!visited.Contains(n))
                    {
                        queue.Push(n);
                    }
                }
            }
            Console.WriteLine("Not Found!");
        }

        public bool DfsRecurse(GraphNode source, int Data, HashSet<GraphNode> visited) {
            if (source == null) { return false; }
            if (source.Data == Data) { Console.WriteLine("Found!\n\n"); return true; }
            visited.Add(source);
            Console.Write(source.Data + " ");
            foreach (var n in source.Neighbors) {
                if (!visited.Contains(n)) {
                    bool flag = this.DfsRecurse(n, Data, visited);
                    if (flag) { return flag; }
                }
            }

            return false;
        }
    }

    public class GraphNode 
    {
        public int Data { get; set; }
        public IList<GraphNode> Neighbors { get; set; }
    }
}
