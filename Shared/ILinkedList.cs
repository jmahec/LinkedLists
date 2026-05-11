namespace Shared;

public interface ILinkedList<T>
{
    // Original methods
    bool Contains(T data);
    void InsertAtBeginning(T data);
    void InsertAtEnding(T data);
    void InsertOrdered(T data);
    void Remove(T data);
    void Reverse();
    void Sort();
    string ToString();

    // Taller #5 methods
    string ToStringForward();
    string ToStringBackward();
    void SortDescending();
    List<T> GetModes();
    string GetChart();
    bool Exists(T data);
    bool RemoveFirst(T data);
    int RemoveAll(T data);
}