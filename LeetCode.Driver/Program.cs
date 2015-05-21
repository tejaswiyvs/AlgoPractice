using System;
using System.Collections;
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
			TreeNode node1 = new TreeNode ();
			node1.Data = 1;

			TreeNode node2 = new TreeNode ();
			node2.Data = 2;

			TreeNode node3 = new TreeNode ();
			node3.Data = 3;

			TreeNode node4 = new TreeNode ();
			node4.Data = 4;

			TreeNode node5 = new TreeNode ();
			node5.Data = 5;

			TreeNode node6 = new TreeNode ();
			node6.Data = 6;

			TreeNode node7 = new TreeNode ();
			node7.Data = 7;

			TreeNode node8 = new TreeNode ();
			node8.Data = 8;

			TreeNode node9 = new TreeNode ();
			node9.Data = 9;

			node1.Left = node2;
			node1.Right = node3;
			node2.Left = node4;
			node2.Right = node5;
			node3.Left = node6;
			node3.Right = node7;
			node4.Left = node8;
			node4.Right = node9;

			zigZag (node1);
        }

		private static void zigZag(TreeNode node)
		{
			if (node == null) {
				return;
			}

			var list = new ArrayList ();
			list.Add (node);
			var tuple = new Tuple<int, ArrayList> (0, list);
			var q = new Queue<Tuple<int, ArrayList>> ();
			q.Enqueue (tuple);

			while (q.Count > 0) {
				var t = q.Dequeue();
				var lvl = t.Item1;
				var l = t.Item2;

				var holder = new ArrayList ();
				if (lvl % 2 != 0) {
					for (int i = 0; i < l.Count; i++) {
						var n = (TreeNode)l [i];
						Console.Write (n.Data + " ");
						if (n.Left != null) {
							holder.Add (n.Left);
						} 
						if (n.Right != null) {
							holder.Add (n.Right);
						}
					}
				} 
				else {
					for (int i = l.Count - 1; i >= 0; i--) {
						var n = (TreeNode)l [i];
						Console.Write (n.Data + " ");
						if (n.Left != null) {
							holder.Add (n.Left);
						} 
						if (n.Right != null) {
							holder.Add (n.Right);
						}
					}
				}
				q.Enqueue(new Tuple<int, ArrayList>(lvl + 1, holder));
			}
		}
    }
}
