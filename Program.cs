using LinearStructures.Structures;
using System;
using System.Security.Cryptography.X509Certificates;

internal class Program
{
    public static long N_OP = 0;
    static void Main(string[] args)
    {

        int n = 1000; //длина дэка
        Deque deque = new Deque();
        Random rnd = new Random();

        for (int i = 0; i < n; i++)
        {
            deque.PushBack(new Item(value: rnd.Next(0, 100)));  //Заполнение дэка
        }

        //deque.Print(); //Вывод первоначального состояния дэка

        deque.BinaryInsertSort(); //Сортировка дэка
        Console.WriteLine(N_OP);

        //deque.Print(); //Вывод конечного состояния

    }
}
