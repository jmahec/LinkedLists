namespace SimpleList;

public class Node<T>
{
    public Node(T data)
    {
        Data = data;
        Next = null;
    }

    public T? Data { get; set; }
    public Node<T>? Next { get; set; }
}