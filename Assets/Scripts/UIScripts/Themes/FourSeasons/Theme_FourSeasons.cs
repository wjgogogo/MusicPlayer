using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Theme_FourSeasons : Theme {

    [SerializeField]
    private Button[] m_changeModules;

    private void Start()
    {
        AddListener();
    }

    private void AddListener()
    {
        for (int i = 0; i < m_changeModules.Length; i++)
        {
            UIEventListener btnListener = m_changeModules[i].gameObject.AddComponent<UIEventListener>();
            btnListener.OnClick += delegate (GameObject obj)
            {
                SwitchModule(obj);
            };
        }
    }

    private new void Update()
    {
        base.Update();

        if (Input.GetKeyDown(m_callOutKey))
        {
            for (int i = 0; i < m_changeModules.Length; i++)
            {
                m_changeModules[i].gameObject.SetActive(m_calledMenu);
            }
        }
    }

    /// <summary>
    /// switch the module
    /// </summary>
    /// <param name="obj"></param>
    private void SwitchModule(GameObject obj)
    {
        for (int i = 0; i < m_changeModules.Length; i++)
        {
            if (obj.gameObject.name == m_changeModules[i].gameObject.name)
            {
                ActiveOneModule(i, true);
            }
        }
    }
}
