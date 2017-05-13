using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingModule : ThemeModule
{
    [SerializeField]
    private Button m_searchSongs;

    [SerializeField]
    private Dropdown m_changeLanguage;

    [SerializeField]
    private Dropdown m_changeTheme;


    private ComponentsManager manager;

    private void Start()
    {
        manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
    }
    private void InitDropdown()
    {
        List<string> options = new List<string>();
        options.AddRange(manager.m_data.LanguageNames);
        m_changeLanguage.AddOptions(options);
        m_changeLanguage.onValueChanged.AddListener(LoadLanguage);

        options.Clear();

        options.AddRange(manager.m_data.ThemeNames);
        m_changeTheme.AddOptions(options);
        m_changeTheme.onValueChanged.AddListener(LoadTheme);
    }

    private void LoadLanguage(int index)
    {
        manager.m_data.Type = m_changeLanguage.options[index].text;
    }

    private void LoadTheme(int index)
    {
        
    }
}
