namespace LinearStructures.Structures;

public class Item
{
    public int value; // Значение элемента;

    public Item? next; // Указатель на следующий элемент.
    public Item? previous; // Указатель на предыдущий элемент.

    public Item(int value = 0, Item? next = null, Item? previous = null)
    {
        this.value = value;
        this.next = next;
        this.previous = previous;
    }
}
