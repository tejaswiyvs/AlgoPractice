using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class CareerCup
    {
        public void Solve()
        {
            TreeNode n1 = new TreeNode();
            n1.Data = 1;

            TreeNode n2 = new TreeNode();
            n2.Data = 2;

            TreeNode n3 = new TreeNode();
            n3.Data = 2;

            TreeNode n4 = new TreeNode();
            n4.Data = 3;

            TreeNode n5 = new TreeNode();
            n5.Data = 4;

            TreeNode n6 = new TreeNode();
            n6.Data = 4;

            TreeNode n7 = new TreeNode();
            n7.Data = 3;

            TreeNode n8 = new TreeNode();
            n8.Data = 5;

            TreeNode n9 = new TreeNode();
            n9.Data = 5;

            n1.Left = n2;
            n1.Right = n3;
            n2.Left = n4;
            n2.Right = n5;
            n4.Left = n8;
            n4.Right = n9;

            Console.WriteLine(this.IsFoldable(n1));
        }

        public bool IsFoldable(TreeNode node)
        {
            if (node == null) { return false; }
            Queue<Tuple<int, IList<TreeNode>>> queue = new Queue<Tuple<int, IList<TreeNode>>>();
            var lvl0 = new List<TreeNode>();
            lvl0.Add(node);
            queue.Enqueue(new Tuple<int, IList<TreeNode>>(0, lvl0));
            while (queue.Count > 0)
            {
                var tuple = queue.Dequeue();
                var lvl = tuple.Item1;
                IList<TreeNode> nodes = tuple.Item2;
                for (int i = 0; i < nodes.Count / 2; i++)
                {
                    var n1 = nodes[i];
                    var n2 = nodes[nodes.Count - 1 - i];
                    if (n1.Data != n2.Data)
                    {
                        return false;
                    }
                }

                var lvlList = new List<TreeNode>();
                for (int i = 0; i < nodes.Count; i++)
                {
                    var n = nodes[i];
                    if (n.Left != null) {
                        lvlList.Add(n.Left);
                    }
                    if (n.Right != null) {
                        lvlList.Add(n.Right);
                    }
                }
                var lvlTuple = new Tuple<int, IList<TreeNode>>(lvl + 1, lvlList);
                queue.Enqueue(lvlTuple);
            }

            return true;
        }
    }

    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode()
        {
        
        }

        public TreeNode(int data, TreeNode left, TreeNode right)
        {
            this.Data = data;
            this.Left = left;
            this.Right = right;
        }
    }
}
