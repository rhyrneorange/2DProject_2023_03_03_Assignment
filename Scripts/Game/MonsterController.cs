using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int CurMonsterNum;

    [SerializeField] float m_speed = 2f;

    void OnEnable()
    {
        CurMonsterNum = MonsterManager.Instance.RandomMonsterNum;
    }

    public void BehaviourUpdate()
    {
        transform.position += Vector3.down * m_speed * Time.deltaTime;
        var viewPos = MonsterManager.Instance.MainCamera.WorldToViewportPoint(transform.position);
        if (viewPos.y < -0.2f)
        {
            MonsterManager.Instance.RemoveMonster(this);
        }
    }

    void Start()
    {
        
    }

    /*void Update()
    {
        transform.position += Vector3.down * m_speed * Time.deltaTime;
    }*/
}
