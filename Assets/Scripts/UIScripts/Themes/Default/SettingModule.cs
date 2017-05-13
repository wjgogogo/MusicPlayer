using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingModule : ThemeModule
{
    public Button SearchSongs;
    public Dropdown ChangeTheme;

    [SerializeField]
    private Button m_colseApp;
    [SerializeField]
    private Toggle m_fullscreen;
    [SerializeField]
    private Dropdown m_changeLanguage;

    [SerializeField]
    private Text m_textImport;
    [SerializeField]
    private Text m_textCloseApp;
    [SerializeField]
    private Text m_textFullscreen;
    [SerializeField]
    private Text m_textLanguage;
    [SerializeField]
    private Text m_textTheme;

    private ComponentsManager manager;

    private void Start()
    {
        m_colseApp.onClick.AddListener(CloseApp);
        m_fullscreen.onValueChanged.AddListener(Fullscreen);

        manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        manager.m_data.OnLanguageTypeChange += ModifyTexts;

        InitDropdown();
        InitLanguage(manager.m_data.Data.language);
        InitFullscrren(manager.m_data.Data.fullScreen);
    }

    private void InitFullscrren(bool isOn)
    {
        if (m_fullscreen.isOn == isOn)
            Fullscreen(isOn);
        else
            m_fullscreen.isOn = isOn;
    }

    private void InitLanguage(string type)
    {
        for (int i = 0; i < m_changeLanguage.options.Count; i++)
        {
            if (type == m_changeLanguage.options[i].text)
            {
                if (i == 0)
                {
                    manager.m_data.LanguageType = type;
                }
                else
                {
                    m_changeLanguage.value = i;
                }
                break;
            }
        }
    }

    private void InitDropdown()
    {
        List<string> options = new List<string>();
        options.AddRange(manager.m_data.LanguageNames);
        m_changeLanguage.ClearOptions();
        m_changeLanguage.AddOptions(options);
        m_changeLanguage.onValueChanged.AddListener(LoadLanguage);

        options.Clear();

        options.AddRange(manager.m_data.ThemeNames);
        ChangeTheme.ClearOptions();
        ChangeTheme.AddOptions(options);
    }

    private void LoadLanguage(int index)
    {
        manager.m_data.LanguageType = m_changeLanguage.options[index].text;
    }

    private void ModifyTexts()
    {
        m_textImport.text = manager.m_data.Language.import;
        m_textCloseApp.text = manager.m_data.Language.close;
        m_textFullscreen.text = manager.m_data.Language.fullscreen;
        m_textLanguage.text = manager.m_data.Language.setLanguage;
        m_textTheme.text = manager.m_data.Language.setTheme;
    }

    private void CloseApp()
    {
        Application.Quit();
    }

    /// <summary>
    /// should be fefactor
    /// </summary>
    /// <param name="flag"></param>
    private void Fullscreen(bool flag)
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
}
