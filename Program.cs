using LinearStructures.Structures;
using System.Diagnostics;

internal class Program
{
    public static long N_OP = 0;
    static void Main(string[] args)
    {
        var watch = new System.Diagnostics.Stopwatch();
        Deque deque = new Deque();
        Random rnd = new Random();

        for (int n = 1; n <= 10; n++)
        {
            for (int i = 0; i < n*75; i++)
            {
                deque.PushBack(new Item(value: rnd.Next(0, 100)));  //Заполнение дэка
            }
            N_OP = 0;
            watch.Start();
            deque.BinaryInsertSort();
            watch.Stop();
            Console.WriteLine($"Номер сортировки: {n} Количество отсортированных элементов: {n * 100} Время сортировки (ms): {watch.ElapsedMilliseconds} Количество операций (N_Op): {N_OP}");

        }
    }
}
