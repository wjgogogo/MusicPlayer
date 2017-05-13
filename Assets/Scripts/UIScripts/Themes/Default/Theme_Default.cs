using UnityEngine;
using UnityEngine.UI;

public class Theme_Default : Theme
{
    [SerializeField]
    private KeyCode CallOutKey = KeyCode.Escape;

    [SerializeField]
    private ListModule listModule;
    [SerializeField]
    private PlayModule playModule;
    [SerializeField]
    private SettingModule settingModule;

    private bool m_calledMenu = true;

    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (Input.GetKeyDown(CallOutKey))
        {
            m_calledMenu = !m_calledMenu;
            SetAllModulesActive(m_calledMenu);
        }
    }
    
}
