using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelRPGProject
{
    class Stack
    {
        protected int TopPointer = -1;//starts the top of the stack at -1 as there are no items in the stack
        protected List<int> StackList = new List<int>();//this is where the items are added and removed
        public void Push(int value)//pushes the passed in integer value onto the the top of the stack and increments the top pointer
        {
            TopPointer++;
            StackList.Add(value);
        }
        public void Pop()//pops off the top item of the stack and decrements the top pointer
        {
            StackList.RemoveAt(TopPointer);
            TopPointer--;
        }
        public int Peak()//peaks at the top item of the stack and returns the item 
        {
            return StackList[TopPointer];
        }
        public bool IsEmpty()//checks if the stack is empty and returns a boolean value that is true or false 
        {
            bool Empty=false;
            if(StackList.Count==0)
            {
                Empty = true;
            }
            return Empty;
        }
    }
}
