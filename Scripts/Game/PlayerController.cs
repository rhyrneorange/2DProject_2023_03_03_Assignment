using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Camera m_mainCam;

    [Header("발사체 컨트롤")]
    GameObjectPool<BulletController> m_bulletpool;
    [SerializeField] GameObject m_bulletPrefab;
    [SerializeField] Transform m_firePos;
    [SerializeField] float m_speed = 4f;

    [Header("주인공 컨트롤")]
    [SerializeField] Vector3 m_dir;
    Vector3 m_startPos;

    Vector3 m_prevPos;

    [SerializeField] float m_duration = 0.1f;
    float m_moveValue;
    bool m_isDrag;
    
    public void RemoveBullet(BulletController bullet)
    {
        bullet.gameObject.SetActive(false);
        m_bulletpool.Set(bullet);
    }

    void CreateBullet()
    {
        var bullet = m_bulletpool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = m_firePos.position;
    }

    void Start()
    {
        m_mainCam = Camera.main;
        InvokeRepeating("CreateBullet", 1f, m_duration);
        m_bulletpool = new GameObjectPool<BulletController>(1, () =>
        {
             var obj = Instantiate(m_bulletPrefab); //Prefab을 생성해서
             obj.SetActive(false); //비활성화하고
             var bullet = obj.GetComponent<BulletController>(); //Prefab의 BulletController를 얻어와서
             bullet.Initialized(this);
             return bullet; //반환
        });
    }
    void Update()
    {
        m_dir = new Vector3(Input.GetAxis("Horizontal"), 0f);
        m_moveValue = m_speed * Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            m_isDrag = true;
            m_startPos = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isDrag = false;
        }
        if (m_isDrag)
        {
            var endPos = m_mainCam.ScreenToWorldPoint(Input.mousePosition);
            var result = endPos - m_startPos;
            m_dir = result.x < 0f ? Vector3.left : Vector3.right;
            m_moveValue = Mathf.Abs(result.x);
            m_startPos = endPos;
        }

        transform.position += m_dir * m_moveValue;
        var viewPos = m_mainCam.WorldToViewportPoint(transform.position);
        if (viewPos.x < 0f)
            viewPos.x = 0f;
        else if (viewPos.x > 1f)
            viewPos.x = 1f;
        transform.position = m_mainCam.ViewportToWorldPoint(viewPos);
    }
}
