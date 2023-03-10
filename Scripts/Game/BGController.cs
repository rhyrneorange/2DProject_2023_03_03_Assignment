using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    SpriteRenderer m_sprRenderer;
    [SerializeField] float m_speed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        m_sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_sprRenderer.material.mainTextureOffset += Vector2.up * m_speed * Time.deltaTime;
    }
}
