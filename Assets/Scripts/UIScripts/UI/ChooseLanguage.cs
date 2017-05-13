using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLanguage : MonoBehaviour
{
    [SerializeField]
    private Dropdown m_dropdown;

    private void Start()
    {
        List<string> options = new List<string>();

        m_dropdown = GetComponent<Dropdown>();
        m_dropdown.ClearOptions();

        var files = Directory.GetFiles(DataAnalysis.LanguageFolder);

        FileTools.GetFilesNameWithoutExtension(files);

        options.AddRange(files);

        m_dropdown.AddOptions(options);
        m_dropdown.onValueChanged.AddListener(SetLanguage);
    }

    void SetLanguage(int index)
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        
        manager.m_data.LanguageType = m_dropdown.options[index].text;
    }

}
