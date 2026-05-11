using Shared;

namespace DoubleList;

public class DoubleLinkedList<T> : ILinkedList<T> where T : IComparable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;

    public DoubleLinkedList()
    {
        _head = null;
        _tail = null;
    }

    // ─────────────────────────────────────────────────────────────
    // INSERT AT BEGINNING
    // ─────────────────────────────────────────────────────────────
    public void InsertAtBeginning(T data)
    {
        var newNode = new Node<T>(data);
        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // INSERT AT END
    // ─────────────────────────────────────────────────────────────
    public void InsertAtEnding(T data)
    {
        var newNode = new Node<T>(data);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
        }
    }

    // ─────────────────────────────────────────────────────────────
    // 1. INSERT ORDERED (ascending) — used by menu option 1
    // ─────────────────────────────────────────────────────────────
    public void InsertOrdered(T data)
    {
        var newNode = new Node<T>(data);

        if (_head == null)
        {
            _head = newNode;
            _tail = newNode;
            return;
        }

        if (data.CompareTo(_head.Data) <= 0)
        {
            newNode.Next = _head;
            _head.Previous = newNode;
            _head = newNode;
            return;
        }

        if (data.CompareTo(_tail!.Data) >= 0)
        {
            _tail.Next = newNode;
            newNode.Previous = _tail;
            _tail = newNode;
            return;
        }

        var current = _head.Next;
        while (current != null && data.CompareTo(current.Data) > 0)
            current = current.Next;

        var previous = current!.Previous;
        previous!.Next = newNode;
        newNode.Previous = previous;
        newNode.Next = current;
        current.Previous = newNode;
    }

    // ─────────────────────────────────────────────────────────────
    // CONTAINS
    // ─────────────────────────────────────────────────────────────
    public bool Contains(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data)) return true;
            current = current.Next;
        }
        return false;
    }

    // ─────────────────────────────────────────────────────────────
    // REMOVE (original interface — delegates to RemoveFirst)
    // ─────────────────────────────────────────────────────────────
    public void Remove(T data)
    {
        RemoveFirst(data);
    }

    // ─────────────────────────────────────────────────────────────
    // REVERSE (original interface — delegates to SortDescending)
    // ─────────────────────────────────────────────────────────────
    public void Reverse()
    {
        SortDescending();
    }

    // ─────────────────────────────────────────────────────────────
    // SORT ascending
    // ─────────────────────────────────────────────────────────────
    public void Sort()
    {
        if (_head == null || _head.Next == null) return;

        var values = new List<T>();
        var current = _head;
        while (current != null)
        {
            values.Add(current.Data!);
            current = current.Next;
        }
        values.Sort();

        _head = null;
        _tail = null;
        foreach (var v in values)
            InsertAtEnding(v);
    }

    // ─────────────────────────────────────────────────────────────
    // TOSTRING override
    // ─────────────────────────────────────────────────────────────
    public override string ToString()
    {
        return ToStringForward();
    }

    // ─────────────────────────────────────────────────────────────
    // 2. SHOW FORWARD
    // ─────────────────────────────────────────────────────────────
    public string ToStringForward()
    {
        if (_head == null) return "(lista vacía)";

        var result = string.Empty;
        var current = _head;
        while (current != null)
        {
            result += current.Data;
            if (current.Next != null) result += " <-> ";
            current = current.Next;
        }
        return result;
    }

    // ─────────────────────────────────────────────────────────────
    // 3. SHOW BACKWARD
    // ─────────────────────────────────────────────────────────────
    public string ToStringBackward()
    {
        if (_tail == null) return "(lista vacía)";

        var result = string.Empty;
        var current = _tail;
        while (current != null)
        {
            result += current.Data;
            if (current.Previous != null) result += " <-> ";
            current = current.Previous;
        }
        return result;
    }

    // ─────────────────────────────────────────────────────────────
    // 4. SORT DESCENDING — reverses pointer links in O(n)
    // ─────────────────────────────────────────────────────────────
    public void SortDescending()
    {
        if (_head == null || _head.Next == null) return;

        var current = _head;
        while (current != null)
        {
            var temp = current.Next;
            current.Next = current.Previous;
            current.Previous = temp;
            current = temp;
        }

        var tempHead = _head;
        _head = _tail;
        _tail = tempHead;
    }

    // ─────────────────────────────────────────────────────────────
    // 5. GET MODES
    // ─────────────────────────────────────────────────────────────
    public List<T> GetModes()
    {
        var modes = new List<T>();
        if (_head == null) return modes;

        var freq = new Dictionary<T, int>();
        var current = _head;
        while (current != null)
        {
            if (freq.ContainsKey(current.Data!))
                freq[current.Data!]++;
            else
                freq[current.Data!] = 1;
            current = current.Next;
        }

        int max = 0;
        foreach (var pair in freq)
            if (pair.Value > max) max = pair.Value;

        if (max == 1) return modes;

        foreach (var pair in freq)
            if (pair.Value == max) modes.Add(pair.Key);

        modes.Sort();
        return modes;
    }

    // ─────────────────────────────────────────────────────────────
    // 6. GET CHART
    // ─────────────────────────────────────────────────────────────
    public string GetChart()
    {
        if (_head == null) return "(lista vacía)";

        var freq = new Dictionary<T, int>();
        var insertionOrder = new List<T>();

        var current = _head;
        while (current != null)
        {
            if (!freq.ContainsKey(current.Data!))
            {
                freq[current.Data!] = 1;
                insertionOrder.Add(current.Data!);
            }
            else
            {
                freq[current.Data!]++;
            }
            current = current.Next;
        }

        var result = string.Empty;
        foreach (var key in insertionOrder)
            result += $"{key,-10} {new string('*', freq[key])}\n";

        return result.TrimEnd();
    }

    // ─────────────────────────────────────────────────────────────
    // 7. EXISTS
    // ─────────────────────────────────────────────────────────────
    public bool Exists(T data)
    {
        return Contains(data);
    }

    // ─────────────────────────────────────────────────────────────
    // 8. REMOVE FIRST OCCURRENCE
    // ─────────────────────────────────────────────────────────────
    public bool RemoveFirst(T data)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Data!.Equals(data))
            {
                RemoveNode(current);
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    // ─────────────────────────────────────────────────────────────
    // 9. REMOVE ALL OCCURRENCES
    // ─────────────────────────────────────────────────────────────
    public int RemoveAll(T data)
    {
        int count = 0;
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            if (current.Data!.Equals(data))
            {
                RemoveNode(current);
                count++;
            }
            current = next;
        }
        return count;
    }

    // ─────────────────────────────────────────────────────────────
    // HELPER — detaches a node safely
    // ─────────────────────────────────────────────────────────────
    private void RemoveNode(Node<T> node)
    {
        if (node.Previous != null)
            node.Previous.Next = node.Next;
        else
            _head = node.Next;

        if (node.Next != null)
            node.Next.Previous = node.Previous;
        else
            _tail = node.Previous;
    }
}
