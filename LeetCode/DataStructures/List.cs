using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataStructures
{
    public class List
    {
        private Node head;

        public List()
        { 
        
        }

        public void Add(int data) {
            if (head == null) {
                head = new Node(data, null);
                return;
            }

            var node = new Node(data, head);
            head = node;
        }

        public bool Remove(int data) {
            if (head.Data == data) {
                head = head.Next;
                return false;
            }

            var node = head;
            while (node.Next != null) {
                if (node.Next.Data == data) {
                    node.Next = node.Next.Next;
                    return true;
                }
                node = node.Next;
            }

            return false;
        }

        public void Print() {
            var node = head;
            if (node == null) { Console.WriteLine("Empty list"); }
            while (node != null) {
                Console.Write(node.Data + " ");
                node = node.Next;
            }
        }

        public void Reverse() {
            var curr = head;
            Node prev = null;
            var next = head.Next;

            while (curr != null) {
                curr.Next = prev;
                prev = curr;
                curr = next;
                if (curr != null) {
                    next = curr.Next;
                }
            }

            head = prev;
        }

        public void RecursiveReverse() {
            this.RecursiveReverse(head);
        }

        private void RecursiveReverse(Node currentNode)
        {
            //check for empty list 
            if (currentNode == null)
                return;

            if (currentNode.Next == null) {
                head = currentNode;
                return;
            }

            RecursiveReverse(currentNode.Next);
            currentNode.Next.Next = currentNode;
            currentNode.Next = null;
        }


        private Node findTail() {
            if (head == null) { return null; }

            var node = head;
            while (node.Next != null) {
                node = node.Next;
            }
            return node;
        }
    }

    class Node {
        public int Data { get; set; }
        public Node Next { get; set; }

        public Node(int Data, Node next) {
            this.Data = Data;
            this.Next = next;
        }
    }
}
