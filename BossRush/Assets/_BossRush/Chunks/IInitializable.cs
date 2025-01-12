using UnityEngine;
using UnityEngine.Events;

public interface IInitializable
{
    public UnityEvent onInitialized { get; }
    public void Initialize()
    {
        OnInitialize();
        onInitialized.Invoke();
    }

    void OnInitialize();
}

public interface IInitializable<T>
{
    public UnityEvent<T> onInitialized { get; }
    public void Initialize(T data)
    {
        OnInitialize(data);
        onInitialized.Invoke(data);
    }

    void OnInitialize(T data);
}

public interface IInitializable<T1, T2>
{
    public UnityEvent<T1, T2> onInitialized { get; }
    public void Initialize(T1 data1, T2 data2)
    {
        OnInitialize(data1, data2);
        onInitialized.Invoke(data1, data2);
    }

   void OnInitialize(T1 data1, T2 data2);
}

public interface IInitializable<T1, T2, T3>
{
    public UnityEvent<T1, T2, T3> onInitialized { get; }
    public void Initialize(T1 data1, T2 data2, T3 data3)
    {
        OnInitialize(data1, data2, data3);
        onInitialized.Invoke(data1, data2, data3);
    }

    void OnInitialize(T1 data1, T2 data2, T3 data3);
}


public interface IInitializable<T1, T2, T3, T4>
{
    public UnityEvent<T1, T2, T3, T4> onInitialized { get; }
    public void Initialize(T1 data1, T2 data2, T3 data3, T4 data4)
    {
        OnInitialize(data1, data2, data3, data4);
        onInitialized.Invoke(data1, data2, data3, data4);
    }

    void OnInitialize(T1 data1, T2 data2, T3 data3, T4 data4);
}


