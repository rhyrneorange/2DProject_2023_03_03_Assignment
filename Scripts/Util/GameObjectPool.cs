using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : class, new() //일반화 선언
{
    Queue<T> m_memoryPool = new Queue<T>();
    Func<T> m_createFunc;
    int m_count;

    public GameObjectPool(int count, Func<T> createFunc)
    {
        m_count = count; //몇개를
        m_createFunc = createFunc; //어떻게
        if (m_createFunc != null)
            Allocation();
    }
    public void Allocation()
    {
        for(int i=0; i<m_count; i++)
        {
            m_memoryPool.Enqueue(m_createFunc());
        }
    }

    public T Get()
    {
        if (m_memoryPool.Count> 0) //있으면 가져다 쓰고
        {
            return m_memoryPool.Dequeue();
        }
        return m_createFunc(); //모자라면 만들어서 반환
    }
    public void Set(T obj)
    {
        m_memoryPool.Enqueue(obj);
    }
}
