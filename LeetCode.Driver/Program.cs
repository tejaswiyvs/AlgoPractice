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
            ArrayProblems p = new ArrayProblems();
            var result = new int[] { 0, 0, 0, 0 };
            while (result != null)
            {
                result = p.NextSparse(result);
            }
        }
    }
}
