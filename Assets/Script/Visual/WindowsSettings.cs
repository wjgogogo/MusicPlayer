using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsSettings : MonoBehaviour
{
    public Toggle m_fullScreenToggle;
    public Button m_closeAppButton;
    public Button m_minModeButton;

    public GameObject m_userInterface;

    private bool m_calledMenu = false;
    // Use this for initialization
    void Start()
    {
        GetControlComponents();
        m_calledMenu = m_userInterface.activeSelf;
    }

    /// <summary>
    /// Get all comtrol components
    /// </summary>
    private void GetControlComponents()
    {
        if (m_fullScreenToggle == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(TagsManager.SETTING_FULLSCREEN_TAG);
            if (obj != null)
            {
                m_fullScreenToggle = obj.GetComponent<Toggle>();
                m_fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
            }
        }
        else
        {
            m_fullScreenToggle.onValueChanged.AddListener(ToggleFullScreen);
        }

        if (m_closeAppButton == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(TagsManager.SETTING_CLOSE_TAG);
            if (obj != null)
            {
                m_closeAppButton = obj.GetComponent<Button>();
                m_closeAppButton.onClick.AddListener(CloseApp);
            }
        }
        else
        {
            m_closeAppButton.onClick.AddListener(CloseApp);
        }

        if (m_minModeButton == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag(TagsManager.SETTING_MIN_MODE_TAG);
            if (obj != null)
            {
                m_minModeButton = obj.GetComponent<Button>();
                m_minModeButton.onClick.AddListener(MinMode);
            }
        }
        else
        {
            m_minModeButton.onClick.AddListener(MinMode);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_calledMenu)
            {
                m_userInterface.SetActive(false);
            }
            else
            {
                m_userInterface.SetActive(true);
            }

            m_calledMenu = !m_calledMenu;
        }
    }

    /// <summary>
    /// Get the windows informatioin
    /// </summary>
    /// <returns></returns>
    public string GetWindowsSettingInfo()
    {
        string ret = string.Empty;

        foreach (var item in Screen.resolutions)
        {
            ret += item.ToString() + "\n";
        }

        ret += "fullScrren: " + Screen.fullScreen.ToString();

        return ret;
    }

    /// <summary>
    /// Whether full screen
    /// </summary>
    /// <param name="flag">The status of the toggle</param>
    private void ToggleFullScreen(bool flag)
    {
        Resolution newSolution = new Resolution();

        if (!flag)
        {
            newSolution.width = 800;
            newSolution.height = 600;
        }
        else
        {
            newSolution = Screen.resolutions[Screen.resolutions.Length - 1];
        }
        Screen.SetResolution(newSolution.width, newSolution.height, flag);
        Debug.Log("Fullscreen");
    }

    /// <summary>
    /// Close the application
    /// </summary>
    private void CloseApp()
    {
        Debug.Log("Close App");
        Application.Quit();
    }

    /// <summary>
    /// Minimized the application
    /// </summary>
    private void MinMode()
    {
        try
        {

        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
        Debug.Log("Min Mode");
    }
}
