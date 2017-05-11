using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLanguage : MonoBehaviour
{
    [SerializeField]
    private Dropdown m_dropdown;

    List<string> options = new List<string>();

    private void Start()
    {
        m_dropdown = GetComponent<Dropdown>();
        m_dropdown.ClearOptions();

        var files = Directory.GetFiles(DataAnalysis.m_languageFolder);

        for (int i = 0; i < files.Length; i++)
        {
            options.Add(files[i].Replace(DataAnalysis.m_languageFolder + "\\", ""));
        }

        m_dropdown.AddOptions(options);
        m_dropdown.onValueChanged.AddListener(SetLanguage);
    }

    void SetLanguage(int index)
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        
        index = options.Count - 1 - index;
        index = index < 0 ? 0 : index;
        manager.m_data.Type = options[index];

    }

}
