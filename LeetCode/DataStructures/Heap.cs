using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataStructures
{
    public class Heap
    {
        private System.Collections.ArrayList holder = new System.Collections.ArrayList();

        public Heap()
        {
            holder = new System.Collections.ArrayList();
        }

        public Heap(int[] data)
        {
            holder = this.heapify(data);
        }

        private System.Collections.ArrayList heapify(int[] data)
        {
            return null;
        }

        public void Insert(int data)
        {
            holder.Add(data);
            this.siftUp(holder.Count - 1, holder);
        }

        public int RemoveMax()
        {
            if (this.holder.Count == 0) throw new IndexOutOfRangeException("Tried to remove from an empty heap");
            if (this.holder.Count == 1)
            {
                int ret = (int)this.holder[0];
                this.holder.RemoveAt(0);
                return ret;
            }
            else 
            {
                int max = (int)this.holder[0];
                this.swap(this.holder, this.holder.Count - 1, 0);
                this.siftDown(0, this.holder);
                return max;
            }
        }

        private void siftUp(int idx, System.Collections.ArrayList list)
        {
            if (idx <= 0) { return; }
            int data = (int) list[idx];
            int parentIdx = (int) Math.Floor(idx / 2.0);
            int parentData = (int)list[parentIdx];

            if (parentData < data) {
                swap(list, idx, parentIdx);
            }

            this.siftUp(parentIdx, list);
        }

        private void siftDown(int idx, System.Collections.ArrayList list)
        {
            if (idx >= list.Count - 1) { return; }
            int data = (int)list[idx];
            int childIdx1 = 2 * idx;
            int childIdx2 = 2 * idx + 1;
            int child1Data = (int)list[childIdx1];
            int child2Data = (int)list[childIdx2];

            if (data > child1Data && data > child2Data) {
                return;
            }

            if (data < child1Data) {
                this.swap(list, idx, childIdx1);
                this.siftDown(childIdx1, list);
            }

            else if (data < child2Data) {
                this.swap(list, idx, childIdx2);
                this.siftDown(childIdx2, list);
            }
        }

        private void swap(System.Collections.ArrayList list, int idx1, int idx2)
        {
            var tmp = list[idx1];
            list[idx1] = list[idx2];
            list[idx2] = tmp;
        }
    }
}
