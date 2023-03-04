using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    public int RandomMonsterNum;

    public Camera MainCamera { get { return m_mainCam; } }

    public enum MonsterType
    {
        None = -1,
        White,
        Yellow,
        Pink,
        Max
    }

    [SerializeField] GameObject[] m_monsterPrefabs;
    Dictionary<MonsterType, GameObjectPool<MonsterController>> m_monsterPool = new Dictionary<MonsterType, GameObjectPool<MonsterController>>();
    List<MonsterController> m_monsterList = new List<MonsterController>(); //생성된 몬스터들
    Camera m_mainCam;
    Vector2 m_startPos = new Vector2(-2.675f, 6f);
    float m_posGap = 1.35f;

    public void RemoveMonster(MonsterController mon)
    {
        mon.gameObject.SetActive(false);
        if(m_monsterList.Remove(mon))
            m_monsterPool[(MonsterType)mon.CurMonsterNum].Set(mon);
    }

    void CreateMonsters()
    {
        for(int i=0; i<5; i++)
        {
            RandomMonsterNum = Random.Range(0, (int)MonsterType.Max);
            var mon = m_monsterPool[(MonsterType)RandomMonsterNum].Get();
            mon.transform.position = m_startPos + Vector2.right * (m_posGap * i);
            mon.gameObject.SetActive(true);
            m_monsterList.Add(mon);
        }
    }

    protected override void OnStart()
    {
        m_mainCam = Camera.main;
        m_monsterPrefabs = Resources.LoadAll<GameObject>("Prefab/Monster");

        for (int i=0; i<(int)MonsterType.Max; i++)
        {
            RandomMonsterNum = i;
            var monPool = new GameObjectPool<MonsterController>(1, () =>
            {
                var obj = Instantiate(m_monsterPrefabs[RandomMonsterNum]); //Prefab을 생성해서
                obj.SetActive(false); //비활성화하고
                obj.transform.SetParent(transform); //MonsterManager의 자식오브젝트로
                var mon = obj.GetComponent<MonsterController>(); //Prefab의 MonsterController를 얻어와서
                return mon; //반환
            });
            m_monsterPool.Add((MonsterType)i, monPool);
        }
        InvokeRepeating("CreateMonsters", 2f, 3f);
    }

    void Update()
    {
        for(int i=0; i<m_monsterList.Count; i++) //몬스터들의
        {
            m_monsterList[i].BehaviourUpdate(); //동작 함수 호출
        }
    }
}
