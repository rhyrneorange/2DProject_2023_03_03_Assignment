using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : SingletonDontDestroy<PopupManager>
{
    GameObject m_OkCancelPrefab;
    GameObject m_OkPrefab;
    List<GameObject> m_popupList = new List<GameObject>();
    int m_popupDepth = 1000;
    int m_depthGap = 10;
    public bool IsOpen { get { return m_popupList.Count > 0; } }
    protected override void OnStart()
    {
        m_OkCancelPrefab = Resources.Load<GameObject>("Prefab/Popup/Panel_PopupOkCancel");
        m_OkPrefab = Resources.Load<GameObject>("Prefab/Popup/Popup_Ok");
    }
    public void Open_PopupOkCancel(string title, string body, Action okDel = null, Action cancelDel = null, string okBtnText = "Ok", string cancelBtnText = "Cancel")
    {
        var obj = Instantiate(m_OkCancelPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var popup = obj.GetComponent<Popup_OkCancel>();
        popup.SetUI(title, body, okDel, cancelDel, okBtnText, cancelBtnText);
        var panels = obj.GetComponentsInChildren<UIPanel>(); //부모오브젝트도 포함
        for(int i=0; i<panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popupList.Count * m_depthGap + i;
        }
        m_popupList.Add(obj);
    }
    public void Open_PopupOk(string title, string body, Action okDel = null, string okBtnText = "Ok")
    {
        var obj = Instantiate(m_OkPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var popup = obj.GetComponent<Popup_Ok>();
        popup.SetUI(title, body, okDel, okBtnText);
        var panels = obj.GetComponentsInChildren<UIPanel>(); //부모오브젝트도 포함
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_popupDepth + m_popupList.Count * m_depthGap + i;
        }
        m_popupList.Add(obj);
    }
    public void ClosePopup()
    {
        if (m_popupList.Count > 0)
        {
            Destroy(m_popupList[m_popupList.Count - 1]);
            m_popupList.RemoveAt(m_popupList.Count - 1);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(UnityEngine.Random.Range(0, 100) % 2 == 0)
            {
                Open_PopupOkCancel("Notice", "오늘은 휴강입니다.");
            }
            else
            {
                Open_PopupOk("Error", "수업을 진행할 수 없습니다.");
            }
        }
    }
}
