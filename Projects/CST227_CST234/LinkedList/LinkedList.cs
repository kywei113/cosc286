using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedList
{
    public class LinkedList<T> : A_List<T> where T : IComparable<T>
    {
        #region Attributes
        private Node head;

        #endregion

        public override void Add(T data)
        {
            head = RecAdd(head, data);     //Passes in head node to recursively add
        }

        private Node RecAdd(Node current, T data)
        {
            //Base case
            if (current == null)     //In this function, we're operating on an empty memory address, not checking the forward pointer
            {
                //Create a new node
                current = new Node(data);
            }

            //Recursive case
            else
            {
                current.next = RecAdd(current.next, data);      //Setting current node's forward pointer to the next node
            }

            return current;
        }

        public override void Clear()
        {
            head = null;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        #region Insert Implementations


        //public override void Insert(int index, T data)
        //{
        //    if(index > this.Count || index < 0)
        //    {
        //        throw new IndexOutOfRangeException("Index out of bounds");
        //    }

        //    head = RecInsert(index, head, data);
        //}

        //private Node RecInsert(int index, Node current, T data)
        //{
        //    if(index != 0)
        //    {
        //        index--;
        //        current.next = RecInsert(index, current.next, data);
        //    }
        //    else
        //    {
        //        Node newNode = new Node(data);
        //        newNode.next = current;
        //        current = newNode;
        //    }

        //    return current;
        //}

        //This implementation looks forward one node to where we want to do the work
        public override void Insert(int index, T data)
        {
            if (index > this.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }

            if (index == 0)
            {
                Node newNode = new Node(data, head);
                head = newNode;
            }
            else
            {
                RecInsert(index, head, data);
            }
        }

        private void RecInsert(int index, Node current, T data)
        {
            //If we are one node before where we want to insert
            if (index == 1)
            {
                current.next = new Node(data, current.next);
            }
            else
            {
                index--;
                RecInsert(index, current.next, data);
            }
        }

        //public override void Insert(int index, T data)
        //{
        //    if (index > this.Count || index < 0)
        //    {
        //        throw new IndexOutOfRangeException("Index out of bounds");
        //    }
        //}

        //private void RecInsert(int index, Node current, T data)
        //{

        //}
        #endregion

        public override bool Remove(T data)
        {
            return RecRemove(ref head, data);
        }

        private bool RecRemove(ref Node current, T data)
        {
            bool bFound = false;

            //If we are not at the end of the list
            if (current != null)
            {
                //If the current node contains the data to remove
                if (current.data.CompareTo(data) == 0)
                {
                    //Set bFonud to true
                    bFound = true;

                    //Bypass the current node
                    current = current.next;
                }
                else
                {
                    bFound = RecRemove(ref current.next, data);
                }
            }

            return bFound;

        }

        public override T RemoveAt(int index)
        {
            if (index > this.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }

            return RecRemoveAt(index, ref head);
        }


        private T RecRemoveAt(int index, ref Node current)
        {
            Node newNode = new Node(current.data);

            if (index > 0)
            {
                index--;
                RecRemoveAt(index, ref current.next);
            }
            else
            {
                if (current.next != null)
                {
                    newNode = current.next;
                }
                else
                {
                    current = null;
                }
            }

            return newNode.data;

        }

        public override T ReplaceAt(int index, T data)
        {
            if (index > this.Count || index < 0)
            {
                throw new IndexOutOfRangeException("Index out of bounds");
            }

            return RecReplaceAt(index, ref head, data);
        }

        private T RecReplaceAt(int index, ref Node current, T data)
        {
            if (index > 0)
            {
                index--;
                RecReplaceAt(index, ref current.next, data);
            }
            else
            {
                Node newNode = new Node(data, current.next);
                current = newNode;
            }

            return current.data;


        }

        #region Enumerator Implementation
        private class Enumerator : IEnumerator<T>
        {
            //A reference to the linked list
            private LinkedList<T> parentList;

            //A reference to the current node we're visiting
            private Node lastVisited;

            //The next node we want to visit
            private Node scout;

            public Enumerator(LinkedList<T> parentList)
            {
                this.parentList = parentList;
                Reset();
            }

            //Access the curent data item that our internal pointer is pointed at
            public T Current
            {
                get
                {
                    return lastVisited.data;
                }
            }


            object IEnumerator.Current
            {
                get
                {
                    return lastVisited.data;
                }
            }


            //Clean up any resources used by the enumerator. 
            public void Dispose()
            {
                parentList = null;
                scout = null;
                lastVisited = null;
            }


            //Moves to the next data item if possible.
            //If we moved, return true, else return false
            public bool MoveNext()
            {
                bool result = false;

                if (scout != null)
                {
                    //Indicates a move can be done/was done
                    result = true;

                    //Moves current node to the next node
                    lastVisited = scout;

                    //Move scout to the next node
                    scout = scout.next;

                }

                return result;

            }

            public void Reset()
            {
                //Set the node currently looked at to null
                lastVisited = null;

                //Set the scout to the head
                scout = parentList.head;
            }
        }
        #endregion

        #region Node Class
        //A class that represnets the data and a reference to the nodes neighbour to the right
        private class Node
        {
            #region Attributes
            public T data;
            public Node next;
            #endregion

            //Constructor chaining
            public Node(T data) : this(data, null)  //Calling the other constructor, passing in data for data and null for next node address
            {
                //Still need a body but don't need code within it
            }
            public Node(T data, Node next)
            {
                this.data = data;
                this.next = next;
            }
        }

        #endregion
    }
}
