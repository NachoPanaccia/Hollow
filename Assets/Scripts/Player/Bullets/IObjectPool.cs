public interface IObjectPool<T>
{
    bool TryGet(out T obj);
    void Return(T obj);
    int CountAvailable { get; }
    int Capacity { get; }
}