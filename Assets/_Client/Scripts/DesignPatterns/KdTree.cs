using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrowdT
{
    public class KdTree<T> : IEnumerable<T> where T : Component
    {
        protected KdNode Root;
        protected KdNode Last;
        private int _count;
        protected readonly bool Just2D;
        protected float LastUpdate = -1f;
        protected KdNode[] Open;

        public int Count => _count;

        public bool IsReadOnly => false;

        public float AverageSearchLength { protected set; get; }
        public float AverageSearchDeep { protected set; get; }

        /// <summary>
        /// create a tree
        /// </summary>
        /// <param name="just2D">just use x/z</param>
        public KdTree(bool just2D = false)
        {
            Just2D = just2D;
        }

        public T this[int key]
        {
            get
            {
                if (key >= _count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                KdNode current = Root;

                for (int i = 0; i < key; i++)
                {
                    current = current.next;
                }

                return current.component;
            }
        }

        /// <summary>
        /// add item
        /// </summary>
        /// <param name="item">item</param>
        public void Add(T item)
        {
            Add(new KdNode() {component = item});
        }

        /// <summary>
        /// batch add items
        /// </summary>
        /// <param name="items">items</param>
        public void AddAll(List<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// find all objects that matches the given predicate
        /// </summary>
        /// <param name="match">lamda expression</param>
        public KdTree<T> FindAll(Predicate<T> match)
        {
            var list = new KdTree<T>(Just2D);

            foreach (T node in this)
            {
                if (match(node))
                {
                    list.Add(node);
                }
            }

            return list;
        }

        /// <summary>
        /// find first object that matches the given predicate
        /// </summary>
        /// <param name="match">lamda expression</param>
        public T Find(Predicate<T> match)
        {
            KdNode current = Root;

            while (current != null)
            {
                if (match(current.component))
                {
                    return current.component;
                }

                current = current.next;
            }

            return null;
        }

        public void Remove(int i)
        {
            var list = new List<KdNode>(GetNodes());
            list.RemoveAt(i);
            Clear();

            foreach (KdNode node in list)
            {
                node._oldRef = null;
                node.next = null;
            }

            foreach (KdNode node in list)
            {
                Add(node);
            }
        }

        /// <summary>
        /// Remove at position i (position in list or loop)
        /// </summary>
        public void RemoveAt(int i)
        {
            var list = new List<KdNode>(GetNodes());
            list.RemoveAt(i);
            Clear();

            foreach (KdNode node in list)
            {
                node._oldRef = null;
                node.next = null;
            }

            foreach (KdNode node in list)
            {
                Add(node);
            }
        }

        /// <summary>
        /// remove all objects that matches the given predicate
        /// </summary>
        /// <param name="match">lamda expression</param>
        public void RemoveAll(Predicate<T> match)
        {
            var list = new List<KdNode>(GetNodes());
            list.RemoveAll(n => match(n.component));
            Clear();

            foreach (KdNode node in list)
            {
                node._oldRef = null;
                node.next = null;
            }

            foreach (KdNode node in list)
            {
                Add(node);
            }
        }

        /// <summary>
        /// count all objects that matches the given predicate
        /// </summary>
        /// <param name="match">lamda expression</param>
        /// <returns>matching object count</returns>
        public int CountAll(Predicate<T> match)
        {
            int count = 0;

            foreach (T node in this)
            {
                if (match(node))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// clear tree
        /// </summary>
        public void Clear()
        {
            //rest for the garbage collection
            Root = null;
            Last = null;
            _count = 0;
        }

        /// <summary>
        /// Update positions (if objects moved)
        /// </summary>
        /// <param name="rate">Updates per second</param>
        public void UpdatePositions(float rate)
        {
            if (Time.timeSinceLevelLoad - LastUpdate < 1f / rate)
            {
                return;
            }

            LastUpdate = Time.timeSinceLevelLoad;

            UpdatePositions();
        }

        /// <summary>
        /// Update positions (if objects moved)
        /// </summary>
        public void UpdatePositions()
        {
            //save old traverse
            KdNode current = Root;

            while (current != null)
            {
                current._oldRef = current.next;
                current = current.next;
            }

            //save root
            current = Root;

            //reset values
            Clear();

            //readd
            while (current != null)
            {
                Add(current);
                current = current._oldRef;
            }
        }

        /// <summary>
        /// Method to enable foreach-loops
        /// </summary>
        /// <returns>Enumberator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            KdNode current = Root;

            while (current != null)
            {
                yield return current.component;
                current = current.next;
            }
        }

        /// <summary>
        /// Convert to list
        /// </summary>
        /// <returns>list</returns>
        public List<T> ToList()
        {
            var list = new List<T>();

            foreach (T node in this)
            {
                list.Add(node);
            }

            return list;
        }

        /// <summary>
        /// Method to enable foreach-loops
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected float Distance(Vector3 a, Vector3 b)
        {
            if (Just2D)
            {
                return (a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z);
            }
            else
            {
                return (a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y) + (a.z - b.z) * (a.z - b.z);
            }
        }

        protected float GetSplitValue(int level, Vector3 position)
        {
            if (Just2D)
            {
                return level % 2 == 0 ? position.x : position.z;
            }
            else
            {
                return level % 3 == 0 ? position.x : level % 3 == 1 ? position.y : position.z;
            }
        }

        private void Add(KdNode newNode)
        {
            _count++;
            newNode.left = null;
            newNode.right = null;
            newNode.level = 0;
            KdNode parent = FindParent(newNode.component.transform.position);

            //set last
            if (Last != null)
            {
                Last.next = newNode;
            }

            Last = newNode;

            //set root
            if (parent == null)
            {
                Root = newNode;

                return;
            }

            float splitParent = GetSplitValueInternal(parent);
            float splitNew = GetSplitValue(parent.level, newNode.component.transform.position);

            newNode.level = parent.level + 1;

            if (splitNew < splitParent)
            {
                parent.left = newNode; //go left
            }
            else
            {
                parent.right = newNode; //go right
            }
        }

        private KdNode FindParent(Vector3 position)
        {
            //travers from root to bottom and check every node
            KdNode current = Root;
            KdNode parent = Root;

            while (current != null)
            {
                float splitCurrent = GetSplitValueInternal(current);
                float splitSearch = GetSplitValue(current.level, position);

                parent = current;

                if (splitSearch < splitCurrent)
                {
                    current = current.left; //go left
                }
                else
                {
                    current = current.right; //go right
                }
            }

            return parent;
        }

        /// <summary>
        /// Find closest object to given position
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>closest object</returns>
        public T FindClosest(Vector3 position)
        {
            return FindClosestInternal(position);
        }

        /// <summary>
        /// Find close objects to given position
        /// </summary>
        /// <param name="position">position</param>
        /// <returns>close object</returns>
        public IEnumerable<T> FindClose(Vector3 position)
        {
            var output = new List<T>();
            FindClosestInternal(position, output);

            return output;
        }

        public T FindClosestEnemy(SingleUnit unit, List<T> traversed = null)
        {
            if (Root == null)
            {
                return null;
            }

            float nearestDist = float.MaxValue;
            KdNode nearest = null;

            if (Open == null || Open.Length < Count)
            {
                Open = new KdNode[Count];
            }

            for (int i = 0; i < Open.Length; i++)
            {
                Open[i] = null;
            }

            int openAdd = 0;
            int openCur = 0;

            if (Root != null)
            {
                Open[openAdd++] = Root;
            }

            while (openCur < Open.Length && Open[openCur] != null)
            {
                KdNode current = Open[openCur++];

                if (traversed != null)
                {
                    traversed.Add(current.component);
                }

                float nodeDist = Distance(unit.transform.position, current.component.transform.position);

                if (nodeDist < nearestDist)
                {
                    if (!unit.attackedBy)
                    {
                        // SHIT FIX
                        if (current.component is SingleUnit {isDead: false})
                        {
                            nearestDist = nodeDist;
                            nearest = current;
                        }
                    }
                }

                float splitCurrent = GetSplitValueInternal(current);
                float splitSearch = GetSplitValue(current.level, unit.transform.position);

                if (splitSearch < splitCurrent)
                {
                    if (current.left != null)
                    {
                        Open[openAdd++] = current.left; //go left
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist &&
                        current.right != null)
                    {
                        Open[openAdd++] = current.right; //go right
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        Open[openAdd++] = current.right; //go right
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist &&
                        current.left != null)
                    {
                        Open[openAdd++] = current.left; //go left
                    }
                }
            }

            AverageSearchLength = (99f * AverageSearchLength + openCur) / 100f;

            if (nearest != null)
            {
                AverageSearchDeep = (99f * AverageSearchDeep + nearest.level) / 100f;

                return nearest.component;
            }

            return null;
        }

        protected T FindClosestInternal(Vector3 position, List<T> traversed = null)
        {
            if (Root == null)
            {
                return null;
            }

            float nearestDist = float.MaxValue;
            KdNode nearest = null;

            if (Open == null || Open.Length < Count)
            {
                Open = new KdNode[Count];
            }

            for (int i = 0; i < Open.Length; i++)
            {
                Open[i] = null;
            }

            int openAdd = 0;
            int openCur = 0;

            if (Root != null)
            {
                Open[openAdd++] = Root;
            }

            while (openCur < Open.Length && Open[openCur] != null)
            {
                KdNode current = Open[openCur++];

                if (traversed != null)
                {
                    traversed.Add(current.component);
                }

                float nodeDist = Distance(position, current.component.transform.position);

                if (nodeDist < nearestDist)
                {
                    //todo: Quick fix
                    if (current.component is SingleUnit {isDead: false})
                    {
                        nearestDist = nodeDist;
                        nearest = current;
                    }
                }

                float splitCurrent = GetSplitValueInternal(current);
                float splitSearch = GetSplitValue(current.level, position);

                if (splitSearch < splitCurrent)
                {
                    if (current.left != null)
                    {
                        Open[openAdd++] = current.left; //go left
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist &&
                        current.right != null)
                    {
                        Open[openAdd++] = current.right; //go right
                    }
                }
                else
                {
                    if (current.right != null)
                    {
                        Open[openAdd++] = current.right; //go right
                    }

                    if (Mathf.Abs(splitCurrent - splitSearch) * Mathf.Abs(splitCurrent - splitSearch) < nearestDist &&
                        current.left != null)
                    {
                        Open[openAdd++] = current.left; //go left
                    }
                }
            }

            AverageSearchLength = (99f * AverageSearchLength + openCur) / 100f;

            if (nearest != null)
            {
                AverageSearchDeep = (99f * AverageSearchDeep + nearest.level) / 100f;

                return nearest.component;
            }

            return null;
        }

        private float GetSplitValueInternal(KdNode node)
        {
            return GetSplitValue(node.level, node.component.transform.position);
        }

        private IEnumerable<KdNode> GetNodes()
        {
            KdNode current = Root;

            while (current != null)
            {
                yield return current;
                current = current.next;
            }
        }

        protected class KdNode
        {
            internal T component;
            internal int level;
            internal KdNode left;
            internal KdNode right;
            internal KdNode next;
            internal KdNode _oldRef;
        }
    }
}