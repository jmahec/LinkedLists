namespace DoubleList;

public class Node<T>
{
    public Node(T data)
    {
        Data = data;
        Next = null;
        Previous = null;
    }

    public T? Data { get; set; }
    public Node<T>? Next { get; set; }
    public Node<T>? Previous { get; set; }
}