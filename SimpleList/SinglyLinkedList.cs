using Shared;

namespace SimpleList;

public class SinglyLinkedList<T> : ILinkedList<T>
{
    private Node<T>? _head;

    public SinglyLinkedList()
    {
        _head = null;
    }

    public override string ToString()
    {
        var current = _head;
        var result = string.Empty;
        while (current != null)
        {
            result += $"{current.Data} -> ";
            current = current.Next;
        }
        result += "null";
        return result;
    }

    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        newNode.Next = _head;
        _head = newNode;
    }

    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null) { _head = newNode; return; }
        var current = _head;
        while (current.Next != null) current = current.Next;
        current.Next = newNode;
    }

    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data != null && current.Data.Equals(data)) return true;
            current = current.Next;
        }
        return false;
    }

    public void Remove(T data) => RemoveFirst(data);

    public void Reverse()
    {
        Node<T>? previous = null;
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = previous;
            previous = current;
            current = next;
        }
        _head = previous;
    }

    public void InsertOrdered(T data) => throw new NotImplementedException();
    public void Sort() => throw new NotImplementedException();

    // ── Taller #5 — stub implementations (not used by SimpleList) ──
    public string ToStringForward() => ToString();
    public string ToStringBackward() => throw new NotImplementedException();
    public void SortDescending() => Reverse();
    public List<T> GetModes() => throw new NotImplementedException();
    public string GetChart() => throw new NotImplementedException();
    public bool Exists(T data) => Contains(data);
    public bool RemoveFirst(T data)
    {
        if (_head == null) return false;
        if (_head.Data != null && _head.Data.Equals(data)) { _head = _head.Next; return true; }
        var current = _head;
        while (current.Next != null)
        {
            if (current.Next.Data != null && current.Next.Data.Equals(data))
            {
                current.Next = current.Next.Next;
                return true;
            }
            current = current.Next;
        }
        return false;
    }
    public int RemoveAll(T data)
    {
        int count = 0;
        while (RemoveFirst(data)) count++;
        return count;
    }
}
