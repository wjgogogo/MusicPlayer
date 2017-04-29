using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{

    public Text m_title;
    public Button m_normalSetting;
    public Button m_musicSetting;
    public Button m_themeSetting;
    public Button m_debugInfo;

    public GameObject m_normalPanel;
    public GameObject m_musicPanel;
    public GameObject m_themePanel;
    public GameObject m_debugPanel;

    // Use this for initialization
    void Start()
    {
        SetListener();
        OpenDebugPanel();
    }

    private void SetListener()
    {
        m_normalSetting.onClick.AddListener(OpenNormalPanel);
        m_musicSetting.onClick.AddListener(OpenMusicPanel);
        m_themeSetting.onClick.AddListener(OpenThemePanel);
        m_debugInfo.onClick.AddListener(OpenDebugPanel);
    }

    private void CloseAllPanels()
    {
        if (m_normalPanel.activeSelf)
            m_normalPanel.SetActive(false);
        if (m_musicPanel.activeSelf)
            m_musicPanel.SetActive(false);
        if (m_themePanel.activeSelf)
            m_themePanel.SetActive(false);
        if (m_debugPanel.activeSelf)
            m_debugPanel.SetActive(false);
    }

    void OpenNormalPanel()
    {
        m_title.text = "常\n规\n设\n置";
        CloseAllPanels();
        m_normalPanel.SetActive(true);
    }

    void OpenMusicPanel()
    {
        m_title.text = "音\n乐\n设\n置";
        CloseAllPanels();
        m_musicPanel.SetActive(true);
    }

    void OpenThemePanel()
    {
        m_title.text = "主\n题\n设\n置";
        CloseAllPanels();
        m_themePanel.SetActive(true);
    }

    void OpenDebugPanel()
    {
        m_title.text = "测\n试\n面\n板";
        CloseAllPanels();
        m_debugPanel.SetActive(true);
    }
}
