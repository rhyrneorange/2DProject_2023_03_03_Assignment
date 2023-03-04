using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_OkCancel : MonoBehaviour
{
    [SerializeField] UILabel m_title;
    [SerializeField] UILabel m_body;
    [SerializeField] UILabel m_okBtnLabel;
    [SerializeField] UILabel m_cancelBtnLabel;
    Action m_okDel;
    Action m_cancelDel;

    public void SetUI(string title, string body, Action okDel, Action cancelDel, string okBtnText, string cancelBtnText)
    {
        m_title.text = title;
        m_body.text = body;
        m_okBtnLabel.text = okBtnText;
        m_cancelBtnLabel.text = cancelBtnText;
        m_okDel = okDel;
        m_cancelDel = cancelDel;
    }
    public void OnPressOk()
    {
        if (m_okDel != null)
        {
            m_okDel();
        }
    }
    public void OnPressCancel()
    {
        if (m_cancelDel != null)
        {
            m_cancelDel();
        }
        else
        {
            PopupManager.Instance.ClosePopup();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
}