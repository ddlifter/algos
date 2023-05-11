using System.Xml.Linq;

namespace LinearStructures.Structures;

// Линейная структура "Дэк"
public class Deque
{
    public Item? head;
    public Item? tail;
    
    public int Length
    {
        get
        {
            int count = 0;
            Item? current = head;
            while (current != null)
            {
                count++;
                current = current.next;
            }
            return count;
        }
    }

    public bool isEmpty()
    {
        return this.head == null && this.tail == null;
    }

    public void Print()
    {
        Item? current = this.head;
        while (current != null)
        {
            Console.Write($"{current.value.ToString()} ");
            current = current.next;
        }
        Console.WriteLine();
    }

    public void PushBack(Item newitem)
    {
        if (head == null)
        {
            head = newitem;
        }
        else
        {
            tail.next = newitem;
            newitem.previous = tail;
        }
        tail = newitem;
    }

    public void PushFront(Item newitem)
    {
        Item? temp = head;
        newitem.next = temp;
        head = newitem;
        if (head == null)
        {
            tail = head;
        }
        else
        {
            temp.previous = newitem;
        }
    }

    public Item PopBack()
    {
        if (head == null)
            throw new InvalidOperationException("Deque is empty");
        Item? output = tail;
        if(this.Length == 1)
        {
            head = tail = null;
        }
        else
        {
            tail = tail.previous;
            tail.next = null;
        }
        return output;
    }

    public Item PopFront()
    {
        if (this.isEmpty())
            throw new InvalidOperationException("Deque is empty");
        Item? output = head;
        head = head.next;     
        output.next = null;
        return output;
    }

    public int Get(int index)
    {
        int result = 0;
        if (isEmpty())
            throw new InvalidOperationException("Deque is empty");

        if (index > this.Length)
        {
            throw new Exception("Out of range");
        }

        if (index > (this.Length / 2) - 1)
        {
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushFront(PopBack());
            }
            result = tail.value;
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushBack(PopFront());
            }
            return result;
        }
        else
        {
            for (int i = 0; i < index; i++)
            {
                PushBack(PopFront());
            }
            result = head.value;
            for (int i = 0; i < index; i++)
            {
                PushFront(PopBack());
            }
            return result;
        }
    }

    public void Set(int index, int value)
    {
        if (isEmpty())
            throw new InvalidOperationException("Deque is empty");

        if (index > this.Length)
        {
            throw new Exception("Out of range");
        }

        if (index > (this.Length / 2) - 1)
        {
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushFront(PopBack());
            }
            tail.value = value;
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushBack(PopFront());
            }
        }
        else
        {
            for (int i = 0; i < index; i++)
            {
                PushBack(PopFront());
            }
            head.value = value;
            for (int i = 0; i < index; i++)
            {
                PushFront(PopBack());
            }
        }
    }


    public int this[int index]
    {
        get => Get(index);
        set => Set(index, value);
    }


    public void BinaryInsertSort()
    {
        for (int i = 1; i < this.Length; i++)
        {
            int key = this[i];  // Ключ (сортируемый элемент);
            int left = 0;       // Левая граница отсортированной части;
            int right = i - 1;  // Правая граница отсортированной части.

            // Бинарный поиск в отсортированной части стэка.
            while (left <= right)
            {
                int middle = (left + right) / 2;

                if (this[middle] > key)
                {
                    right = middle - 1;
                }
                else
                {
                    left = middle + 1;
                }
            }

            // Сдвиг необходимых элементов вправо на 1.
            for (int j = i - 1; j >= left; j--)
            {
                this[j + 1] = this[j];
            }

            this[left] = key; // Вставка ключа на его место в отсортированной части.

            Console.Write($"Этап сортировки {i,4}:      ");
            this.Print();     // Вывод на экран промежуточного состояния стэка.
        }
    }

}