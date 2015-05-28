using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ListProblems
    {
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null || n < 0) { return null; }

            var runner = head;
            var trailer = head;

            while (n != 0) {
                if (runner == null)
                {
                    return null;
                }
                else 
                {
                    runner = runner.next;
                }
                n--;
            }

            while (runner != null)
            {
                trailer = trailer.next;
                runner = runner.next;
            }

            return trailer;
        }

        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            var holder = new ListNode(-1);
            this.MergeLists(holder, l1, l2);
            return holder.next;
        }

        private void MergeLists(ListNode previous, ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null) { return; }
            if (l1 == null)
            {
                previous.next = l2;
                return;
            }
            if (l2 == null)
            { 
                previous.next = l1;
                return;
            }

            if (l1.val <= l2.val)
            {
                previous.next = l1;
                l1 = l1.next;
            }
            else
            {
                previous.next = l2;
                l2 = l2.next;
            }

            previous = previous.next;
            previous.next = null;
            this.MergeLists(previous, l1, l2);
        }

        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null) { return head; }
            ListNode previous = null;
            ListNode current = head;
            ListNode next = current.next;
            head = next;
            int count = 1;
            while (next != null)
            {
                if (count % 2 != 0)
                {
                    if (previous != null) { previous.next = next; };
                    current.next = next.next;
                    next.next = current; 
                    previous = next;
                    next = current.next;
                }
                else
                {
                    previous = current;
                    current = current.next;
                    next = next.next;
                }
                count++;
            }

            return head;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            this.val = x;
        }
    }
}
