using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class HeapStructure<T> where T : IHeapItem<T>
{
    //Parent = (n-1)/2
    //LeftChild = 2n+1
    //RightChild = 2n+2


    private T[] items;
    private int currentItemCount;


    public int Count => currentItemCount;

    public HeapStructure(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }


    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }

    void SortUp(T item)
    {
        int parentIndex = (item.HeapIndex - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else break;

            parentIndex = (item.HeapIndex - 1) / 2;
        }
    }

    void SortDown(T item)
    {
        while (true)
        {
            int leftChildIndex = 2 * item.HeapIndex + 1;
            int rightChildIndex = 2 * item.HeapIndex + 2;
            int swapIndex = 0;

            if (leftChildIndex < currentItemCount)
            {
                swapIndex = leftChildIndex;

                if (rightChildIndex < currentItemCount && items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                {
                    swapIndex = rightChildIndex;
                }

                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else return;
            }
            else return;
        }
    }

    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;

        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex { get; set; }
}