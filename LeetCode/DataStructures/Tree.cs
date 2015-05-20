using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataStructures
{
    public class Tree
    {
        private TreeNode root { get; set; }

        public void Add(int Data) {
            if (root == null) {
                root = new TreeNode(Data, null, null);
                return;
            }
            this.Add(root, Data);
        }

        // Builds a BST
        private void Add(TreeNode node, int Data)
        {
            if (node == null) { return; }
            if (node.Data >= Data) {
                if (node.Left == null)
                {
                    node.Left = new TreeNode(Data, null, null);
                }
                else {
                    this.Add(node.Left, Data);
                }
            }
            else if (node.Data < Data)
            {
                if (node.Right == null)
                {
                    node.Right = new TreeNode(Data, null, null);
                }
                else
                {
                    this.Add(node.Right, Data);
                }
            }
        }

        // Assumes BST and removes
        public void Remove(int Data) {
            this.Remove(root, Data);
        }

        private TreeNode Remove(TreeNode node, int Data) {
            if (node == null) {
                return null;
            }

            if (node.Data < Data)
            {
                node.Right = this.Remove(node.Right, Data);
                return node;
            }
            else if (node.Data > Data) {
                node.Left = this.Remove(node.Left, Data);
                return node;
            }
            else {
                if (node.isLeaf()) {
                    return null;
                }
                else if (node.Left == null) {
                    node = node.Right;
                    return node;
                }
                else if (node.Right == null)
                {
                    node = node.Left;
                    return node;
                }
                else 
                {
                    node.Data = findMax(node.Left);
                    node.Left =  this.Remove(node.Left, node.Data);
                    return node;
                }
            }
        }

        private int findMax(TreeNode node)
        {
            if (node == null) { throw new ArgumentNullException(); }
            else if (node.Right == null) { return node.Data; }
            return findMax(node.Right);
        }

        // Should probably be an enum instead, but whatever
        public void traverse(string type) {
            if (type == "inorder") {
                this.printInOrderTraversal(root);
            }
            else if (type == "preorder") {
                this.printPreOrderTraversal(root);
            }
            else if (type == "postorder") {
                this.printPostOrderTraversal(root);
            }
            return;
        }

        private void printInOrderTraversal(TreeNode node) {
            if (node == null) { return;  }

            this.printInOrderTraversal(node.Left);
            Console.Write(node.Data + " ");
            this.printInOrderTraversal(node.Right);
        }

        private void printPreOrderTraversal(TreeNode node)
        {
            if (node == null) { return; }

            Console.Write(node.Data + " ");
            this.printInOrderTraversal(node.Left);
            this.printInOrderTraversal(node.Right);
        }

        private void printPostOrderTraversal(TreeNode node)
        {
            if (node == null) { return; }

            this.printInOrderTraversal(node.Left);
            this.printInOrderTraversal(node.Right);
            Console.Write(node.Data + " ");
        }
    }

    class TreeNode
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

        public bool isLeaf() 
        { 
            return (this.Left == null && this.Right == null);
        }
    }
}
