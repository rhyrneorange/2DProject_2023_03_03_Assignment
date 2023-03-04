using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : class, new() //�Ϲ�ȭ ����
{
    Queue<T> m_memoryPool = new Queue<T>();
    Func<T> m_createFunc;
    int m_count;

    public GameObjectPool(int count, Func<T> createFunc)
    {
        m_count = count; //���
        m_createFunc = createFunc; //���
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
        if (m_memoryPool.Count> 0) //������ ������ ����
        {
            return m_memoryPool.Dequeue();
        }
        return m_createFunc(); //���ڶ�� ���� ��ȯ
    }
    public void Set(T obj)
    {
        m_memoryPool.Enqueue(obj);
    }
}
