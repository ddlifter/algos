﻿namespace LinearStructures.Structures;

// Линейная структура "Дэк"
public class Deque
{
    public Item? head;
    public Item? tail;
    long len;
    
    public bool isEmpty() //Провера на пустоту
    {
        Program.N_OP++;
        return head == null; //1
    }

    public void Print() //Вывод элементов дэка
    {
        Item? current = head;
        while (current != null)
        {
            Console.Write($"{current.value.ToString()} ");  //Проходим до конца дэка то есть до нулевого элемена
            current = current.next;
        }
        Console.WriteLine();
    }

    public void PushBack(Item newitem) //Добавить в конец
    {
        Program.N_OP++;
        if (head == null) //1
        {
            Program.N_OP++;
            head = newitem; //1
        }
        else
        {                                        //Если дэк пустой то добавляем элемент который будет являться и головой и хвостом
            Program.N_OP += 4;
            tail.next = newitem;     //2            //Иначе новый элемент теперь следующий для прежнего хвоста а хвост теперь предыдущий для нового элемента
            newitem.previous = tail; //2     
        }
        Program.N_OP += 2;
        tail = newitem; //1
        len++; //1
    }

    public void PushFront(Item newitem) //Добавить в начало
    {
        Program.N_OP += 5;
        Item? temp = head; //1
        newitem.next = temp;  //2                 //Создаем промежуточную переменную temp которой присваиваем значение головы для обмена значениями между головой и следующего для головы элемента
        head = newitem; //1
        if (head == null)   //1                   //Если дэк пустой то добавляем элемент который теперь является и головой и хвостом
        {
            Program.N_OP++;
            tail = head; //1
        }
        else
        {                                     //Иначе новый элемент это предыдущий для temp которая является бывшей головой
            Program.N_OP += 2;
            temp.previous = newitem; //2
        }
        Program.N_OP++;
        len++; //1
    }

    public Item PopBack() //Удаление с конца
    {
        Program.N_OP++;
        if (head == null) //1
            throw new InvalidOperationException("Deque is empty");  //Ошибка если дэк пустой
        Program.N_OP+=2;
        Item? output = tail; //1
        if(len == 1)    //1    //Если в дэке один элемент то присваиваем output значение этого элемента и указываем что голова и хвост теперь null
        {
            Program.N_OP += 2;
            head = tail = null; //2
        }
        else                       //Иначе хвостом теперь будет предыдущий для хвоста элемент
        {
            Program.N_OP += 4;
            tail = tail.previous; //2
            tail.next = null; //2
        }
        Program.N_OP++;
        len--; //1
        return output;
    }

    public Item PopFront() //Удаление с начала
    {
        Program.N_OP += 3;
        if (this.isEmpty()) //3
            throw new InvalidOperationException("Deque is empty"); //Ошибка если дэк пустой
        Program.N_OP += 6;
        Item? output = head; //1
        head = head.next;   //2                   //Присваиваем для головы новое значение(следующий в дэке элемент)
        output.next = null; //2
        len--;//1
        return output;
    }

    public int Get(int index) //Чтение элемента по индексу
    {
        Program.N_OP += 3;
        int result = 0; //1
        if (isEmpty()) //2
            throw new InvalidOperationException("Deque is empty"); //Ошибка если дэк пустой

        Program.N_OP++;
        if (index > len) //1
        {
            throw new Exception("Out of range");      //Ошибка при выходе за границы дэка
        }


        Program.N_OP += 3;
        if (index > (len / 2) - 1)   //3         //Если индекс находится правее середины то быстрее будет начать перебор дэка с конца(1)
        {
            Program.N_OP += 4;
            for (int i = 0; i < (len - index - 1); i++)//4    //14 + 6*(n-1) = 
            {
                PushFront(PopBack());//2 //(1)    Чтобы дойти до нужного элемента с конца мы удаляем элемент с конца и добавляем его в начало
                Program.N_OP += 6;
            }

            result = tail.value;//2 //Дойдя до нужного элемента запоминаем его
            Program.N_OP += 2;

            Program.N_OP += 4;
            for (int i = 0; i < (len - index - 1); i++)//4
            {
                PushBack(PopFront()); //2 //Теперь возвращаем дэк в исходное состояние удаляя элементы из начала и добавляя их в конец пока не придем к исходному состоянию дэка
                Program.N_OP += 6;
            }
            return result;
        }
        else //Если же элемент левее середины то делаем все наоборот. Сначала удаляем элементы из начала и добавляем их в конец пока не дойдем до нужного элемента
        {    //После чего возвращаем дэк в исходное состояние удаляя элементы с конца и добавляя их в начало

            Program.N_OP += 2;
            for (int i = 0; i < index; i++) // 2
            {
                PushBack(PopFront()); //2
                Program.N_OP += 4;
            }

            result = head.value;
            Program.N_OP += 2;

            Program.N_OP += 2;
            for (int i = 0; i < index; i++)//2
            {
                PushFront(PopBack());//2
                Program.N_OP += 4;
            }
            return result;
        }
    }

    public void Set(int index, int value) // Изменение элемента по индексу
    {                                      //Аналогично Get только с изменением значения
        Program.N_OP += 2;
        if (isEmpty()) //2
            throw new InvalidOperationException("Deque is empty");

        Program.N_OP++;
        if (index > len) //1
        {
            throw new Exception("Out of range");
        }

        Program.N_OP += 3;
        if (index > (len / 2) - 1) //3
        {
            Program.N_OP += 4;
            for (int i = 0; i < (len - index - 1); i++) //4
            {
                PushFront(PopBack());//2
                Program.N_OP += 6;
            }

            tail.value = value;//2
            Program.N_OP += 2;

            Program.N_OP += 4;
            for (int i = 0; i < (len - index - 1); i++) //4
            {
                PushBack(PopFront());//2
                Program.N_OP += 6;
            }
            return;
        }
        else
        {
            Program.N_OP += 2;
            for (int i = 0; i < index; i++) //2
            {
                PushBack(PopFront()); //2
                Program.N_OP += 4;
            }

            head.value = value;//2
            Program.N_OP += 2;

            Program.N_OP += 2;
            for (int i = 0; i < index; i++) //2
            {
                PushFront(PopBack()); //2
                Program.N_OP += 4;
            }
        }
    }


    public int this[int index] //Перегрузка оператора индексирования в []
    {
        get
        {
            Program.N_OP += 2;
            return Get(index); //2
        }
        set
        {
            Program.N_OP += 3;
            Set(index, value); //3
        }
    }


    public void BinaryInsertSort() //Бинарная вставка
    {
        Program.N_OP += 2;
        for (int i = 1; i < len; i++) //2
        {
            Program.N_OP += 2;
            int key = this[i]; //2 // Ключ (сортируемый элемент);
            Program.N_OP += 1;
            int left = 0;   //1    // Левая граница отсортированной части;
            Program.N_OP += 2;
            int right = i - 1; //2  // Правая граница отсортированной части.

            // Бинарный поиск в отсортированной части дэка.
            // 2
            Program.N_OP += 1;
            while (left <= right)
            {
                Program.N_OP += 3;
                int middle = (left + right) / 2; //3

                //2
                Program.N_OP += 2;
                if (this[middle] > key)
                {
                    Program.N_OP += 2;
                    right = middle - 1; //2
                }
                else
                {
                    Program.N_OP += 2;
                    left = middle + 1; //2
                }
            }

            // Сдвиг необходимых элементов вправо на 1.
            Program.N_OP += 3;
            for (int j = i - 1; j >= left; j--) //3
            {
                Program.N_OP += 6;
                this[j + 1] = this[j]; //4
            }

            Program.N_OP += 2;
            this[left] = key; //2 // Вставка ключа на его место в отсортированной части.
        }
    }

}