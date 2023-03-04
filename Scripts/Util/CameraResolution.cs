using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraResolution : MonoBehaviour
{
    [SerializeField] float m_targetWidth = 640;
    [SerializeField] float m_targetHeight = 960;
    float m_curAspectRatio;
    float m_fixedRatio;
    Camera m_camera;

    // Start is called before the first frame update
    void Start()
    {
        float w = 0f, h = 0f, x = 0f, y = 0f;
        m_camera = GetComponent<Camera>();
        m_fixedRatio = m_targetWidth / m_targetHeight;
        m_curAspectRatio = (float)Screen.width / (float)Screen.height;
        if (m_curAspectRatio == m_fixedRatio)
        {
            m_camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
        else if (m_curAspectRatio > m_fixedRatio)
        {
            w = m_fixedRatio / m_curAspectRatio;
            x = (1 - w) / 2;
            m_camera.rect = new Rect(x, 0f, w, 1.0f);
        }
        else if (m_curAspectRatio < m_fixedRatio)
        {
            h = m_curAspectRatio / m_fixedRatio;
            y = (1 - h) / 2;
            m_camera.rect = new Rect(0f, y, 1f, h);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
