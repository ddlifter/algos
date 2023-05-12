namespace LinearStructures.Structures;

// Линейная структура "Дэк"
public class Deque
{
    public Item? head;
    public Item? tail;
    
    public int Length //Свойство длины
    {
        get
        {
            int count = 0;
            Item? current = head;
            while (current != null)
            {
                count++;
                current = current.next;  //Увеличваем счетчик пока не дойдем до конца то есть до нулевого элемента
            }
            return count;
        }
    }

    public bool isEmpty() //Провера на пустоту
    {
        return this.head == null;
    }

    public void Print() //Вывод элементов дэка
    {
        Item? current = this.head;
        while (current != null)
        {
            Console.Write($"{current.value.ToString()} ");  //Проходим до конца дэка то есть до нулевого элемена
            current = current.next;
        }
        Console.WriteLine();
    }

    public void PushBack(Item newitem) //Добавить в конец
    {
        if (head == null)
        {
            head = newitem;
        }
        else
        {                                        //Если дэк пустой то добавляем элемент который будет являться и головой и хвостом
            tail.next = newitem;                 //Иначе новый элемент теперь следующий для прежнего хвоста а хвост теперь предыдущий для нового элемента
            newitem.previous = tail;      
        }
        tail = newitem;
    }

    public void PushFront(Item newitem) //Добавить в начало
    {
        Item? temp = head;
        newitem.next = temp;                   //Создаем промежуточную переменную temp которой присваиваем значение головы для обмена значениями между головой и следующего для головы элемента
        head = newitem;
        if (head == null)                      //Если дэк пустой то добавляем элемент который теперь является и головой и хвостом
        {                                      
            tail = head;
        }
        else
        {                                     //Иначе новый элемент это предыдущий для temp которая является бывшей головой
            temp.previous = newitem;
        }
    }

    public Item PopBack() //Удаление с конца
    {
        if (head == null)
            throw new InvalidOperationException("Deque is empty");  //Ошибка если дэк пустой
        Item? output = tail;
        if(this.Length == 1)        //Если в дэке один элемент то присваиваем output значение этого элемента и указываем что голова и хвост теперь null
        {
            head = tail = null;
        }
        else                       //Иначе хвостом теперь будет предыдущий для хвоста элемент
        {
            tail = tail.previous;
            tail.next = null;
        }
        return output;
    }

    public Item PopFront() //Удаление с начала
    {
        if (this.isEmpty())
            throw new InvalidOperationException("Deque is empty"); //Ошибка если дэк пустой
        Item? output = head;
        head = head.next;                      //Присваиваем для головы новое значение(следующий в дэке элемент)
        output.next = null;
        return output;
    }

    public int Get(int index) //Чтение элемента по индексу
    {
        int result = 0;
        if (isEmpty())
            throw new InvalidOperationException("Deque is empty"); //Ошибка если дэк пустой

        if (index > this.Length)
        {
            throw new Exception("Out of range");      //Ошибка при выходе за границы дэка
        }

        if (index > (this.Length / 2) - 1)            //Если индекс находится правее середины то быстрее будет начать перебор дэка с конца(1)
        {
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushFront(PopBack()); //(1)    Чтобы дойти до нужного элемента с конца мы удаляем элемент с конца и добавляем его в начало
            }
            result = tail.value; //Дойдя до нужного элемента запоминаем его
            for (int i = 0; i < (this.Length - index - 1); i++)
            {
                PushBack(PopFront()); //Теперь возвращаем дэк в исходное состояние удаляя элементы из начала и добавляя их в конец пока не придем к исходному состоянию дэка
            }
            return result;
        }
        else //Если же элемент левее середины то делаем все наоборот. Сначала удаляем элементы из начала и добавляем их в конец пока не дойдем до нужного элемента
        {    //После чего возвращаем дэк в исходное состояние удаляя элементы с конца и добавляя их в начало
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

    public void Set(int index, int value) // Изменение элемента по индексу
    {                                      //Аналогично Get только с изменением значения
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


    public int this[int index] //Перегрузка оператора индексирования в []
    {
        get => Get(index);
        set => Set(index, value);
    }


    public void BinaryInsertSort() //Бинарная вставка
    {
        for (int i = 1; i < this.Length; i++)
        {
            int key = this[i];  // Ключ (сортируемый элемент);
            int left = 0;       // Левая граница отсортированной части;
            int right = i - 1;  // Правая граница отсортированной части.

            // Бинарный поиск в отсортированной части дэка.
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

            Console.Write($"{i}: ");
            this.Print();     // Вывод на экран промежуточного состояния дэка.
        }
    }

}