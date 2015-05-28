using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class TreeProblems
    {
        public void ZigZag(TreeNode node)
        { 
            
        }

        public int Count(TreeNode n, int maxParentValue)
        {
            if (n == null) { return 0; }

            int result = 0;
            if (maxParentValue > n.Data)
            {
                result++;
            }
            else {
                maxParentValue = n.Data;
            }

            return result + this.Count(n.Left, maxParentValue) + this.Count(n.Right, maxParentValue);
        }
    }
}
