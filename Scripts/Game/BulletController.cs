using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float m_speed = 12f;
    [SerializeField] PlayerController m_player;
    Camera m_mainCam;
    public void Initialized(PlayerController player)
    {
        m_player = player;
    }

    void RemoveBullet()
    {
        m_player.RemoveBullet(this);
    }

    void OnEnable()
    {
        if (IsInvoking("RemoveBullet"))
            CancelInvoke("RemoveBullet");
        Invoke("RemoveBullet", 1f);
    }
    void Start()
    {
        m_mainCam = Camera.main;
    }
    void Update()
    {
        transform.position += Vector3.up * m_speed * Time.deltaTime;
    }
}
