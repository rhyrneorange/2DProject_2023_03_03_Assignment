using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : SingletonDontDestroy<LoadSceneManager>
{
    public enum SceneState
    {
        None = -1,
        Title,
        Lobby,
        Game
    }
   
    [SerializeField] UIProgressBar m_progress;
    [SerializeField] UILabel m_progressLabel;
    AsyncOperation m_loadState;
    [SerializeField] GameObject m_uiObject;
    SceneState m_sceneState;
    SceneState m_loadSceneState = SceneState.None;
    StringBuilder m_sb = new StringBuilder();

    public void LoadSceneAsync(SceneState scene)
    {
        if (m_loadSceneState != SceneState.None) return;
        m_loadSceneState = scene;
        m_uiObject.SetActive(true);
        m_loadState = SceneManager.LoadSceneAsync((int)scene);
    }
    void HideUI()
    {
        m_uiObject.SetActive(false);
    }
    protected override void OnStart()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupManager.Instance.IsOpen)
            {
                PopupManager.Instance.ClosePopup();
            }
            else
            {
                switch (m_sceneState)
                {
                    case SceneState.Title:
                        PopupManager.Instance.Open_PopupOkCancel("안내", "게임을 종료하시겠습니까?", () =>
                        {
#if UNITY_EDITOR
                            EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
                        }, null, "예", "아니오");
                        break;
                    case SceneState.Lobby:
                        PopupManager.Instance.Open_PopupOkCancel("안내", "타이틀 화면으로 돌아가시겠습니까?", () =>
                        {
                            LoadSceneAsync(SceneState.Title);
                            PopupManager.Instance.ClosePopup();
                        }, null, "예", "아니오");
                        break;
                    case SceneState.Game:
                        PopupManager.Instance.Open_PopupOkCancel("안내", "정말로 게임을 종료하시겠습니까?\r\n저장하지 않은 정보는 모두 사라집니다.", () =>
                        {
                            LoadSceneAsync(SceneState.Lobby);
                            PopupManager.Instance.ClosePopup();
                        }, null, "예", "아니오");
                        break;
                }
            }
        }
        if (m_loadState != null)
        {
            if (m_loadState.isDone)
            {
                m_loadState = null;
                m_progressLabel.text = "100%";
                m_progress.value = 1f;
                Invoke("HideUI", 1f);
                m_sceneState = m_loadSceneState;
                m_loadSceneState = SceneState.None;
            }
            else
            {
                m_progress.value = m_loadState.progress; //0~0.9
                m_sb.Append((int)(m_loadState.progress * 100f));
                m_sb.Append('%');
                m_progressLabel.text = m_sb.ToString();
                m_sb.Clear();
            }
        }
    }
}
