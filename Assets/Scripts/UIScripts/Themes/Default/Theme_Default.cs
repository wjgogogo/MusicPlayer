using UnityEngine;
using UnityEngine.UI;

public class Theme_Default : Theme
{
    [SerializeField]
    private KeyCode m_callOutKey = KeyCode.Escape;
    [SerializeField]
    private ListModule m_listModule;
    [SerializeField]
    private PlayModule m_playModule;
    [SerializeField]
    private SettingModule m_settingModule;

    private bool m_calledMenu = true;

    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_callOutKey))
        {
            m_calledMenu = !m_calledMenu;
            SetAllModulesActive(m_calledMenu);
        }
    }
    
}
