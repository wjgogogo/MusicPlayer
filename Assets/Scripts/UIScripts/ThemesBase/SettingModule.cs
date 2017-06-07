using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingModule : ThemeModule
{
    public Button ImportSongs;
    [SerializeField]
    protected Dropdown m_changeTheme;

    [SerializeField]
    protected Button m_colseApp;
    [SerializeField]
    protected Toggle m_fullscreen;
    [SerializeField]
    protected Dropdown m_changeLanguage;

    [SerializeField]
    protected Text m_textImport;
    [SerializeField]
    protected Text m_textCloseApp;
    [SerializeField]
    protected Text m_textFullscreen;
    [SerializeField]
    protected Text m_textLanguage;
    [SerializeField]
    protected Text m_textTheme;

    private ComponentsManager manager;

    protected FolderBrowserDialog dialog = new FolderBrowserDialog();

    public void Init()
    {
        m_colseApp.onClick.AddListener(CloseApp);
        m_fullscreen.onValueChanged.AddListener(Fullscreen);

        manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        manager.m_data.OnLanguageTypeChange += ModifyTexts;

        ImportSongs.onClick.AddListener(LoadingSongs);

        InitDropdown();
        InitLanguage(manager.m_data.Data.language);
        InitFullscrren(manager.m_data.Data.fullScreen);
    }

    public void GetDialogPath(FolderBrowserManager.GetPath method)
    {
        dialog.getPathEvent += method;
    }

    private void LoadingSongs()
    {
        dialog.ShowDialog();
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

    private void InitTheme(string name)
    {
        for (int i = 0; i < m_changeTheme.options.Count; i++)
        {
            if (name == m_changeTheme.options[i].text)
            {
                m_changeTheme.value = i;
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
        m_changeTheme.ClearOptions();
        m_changeTheme.AddOptions(options);
        InitTheme(manager.m_data.Theme);
        m_changeTheme.onValueChanged.AddListener(LoadTheme);
    }

    private void LoadTheme(int index)
    {
        manager.m_data.Theme = m_changeTheme.options[index].text;
    }

    private void LoadLanguage(int index)
    {
        manager.m_data.LanguageType = m_changeLanguage.options[index].text;
    }

    private void ModifyTexts()
    {
        if (m_textImport)
            m_textImport.text = manager.m_data.Language.import;
        if (m_textCloseApp)
            m_textCloseApp.text = manager.m_data.Language.close;
        if (m_textFullscreen)
            m_textFullscreen.text = manager.m_data.Language.fullscreen;
        if (m_textLanguage)
            m_textLanguage.text = manager.m_data.Language.setLanguage;
        if (m_textTheme)
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
        manager.m_data.Data.fullScreen = flag;
    }

    private void OnDestroy()
    {
        manager.m_data.OnLanguageTypeChange -= ModifyTexts;
    }
}
