using LinearStructures.Structures;
using System;

internal class Program
{
    static void Main(string[] args)
    {
        int n = 7;
        Deque deque = new Deque();
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            deque.PushBack(new Item(value: rnd.Next(0, 100)));
        }

        deque.Print();

        deque.BinaryInsertSort();

    }
}
