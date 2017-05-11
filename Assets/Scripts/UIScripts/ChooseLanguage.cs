using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLanguage : MonoBehaviour
{

    Dropdown m_dropdown;

    List<string> options = new List<string>();

    private void Start()
    {
        m_dropdown = GetComponent<Dropdown>();
        m_dropdown.ClearOptions();

        options.Add("zh");
        options.Add("en");

        m_dropdown.AddOptions(options);
        m_dropdown.onValueChanged.AddListener(SetLanguage);
    }

    void SetLanguage(int index)
    {
        UIComponentsManager uiManager = GameObject.FindGameObjectWithTag(UIComponentsManager.SELF_TAG).GetComponent<UIComponentsManager>();

        switch (index)
        {
            case 0:
                uiManager.m_textManager.ChangeLanguage(DataAnalysis.LanguageType.zh);
                break;
            case 1:
                uiManager.m_textManager.ChangeLanguage(DataAnalysis.LanguageType.en);
                break;
            default:
                break;
        }
    }

}
