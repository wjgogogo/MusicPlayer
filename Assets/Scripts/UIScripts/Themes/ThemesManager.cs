using UnityEngine;
using UnityEngine.UI;

public class ThemesManager : MonoBehaviour
{
    public GameObject m_button;
    public GameObject m_themesRoot;
    public GameObject m_moduleRoot;

    public Button m_confirm;

    private string themeName = "";
    //all of prefabs
    private Object[] objs;

    void Start()
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        themeName = manager.m_data.Data.themeName;

        objs = Resources.LoadAll("Prefabs");

        m_confirm.onClick.AddListener(SaveThemeName);

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name.IndexOf("Theme_") != -1)
            {
                Button btn = Instantiate(m_button, m_themesRoot.transform).GetComponent<Button>();
                UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();
                btn.GetComponentInChildren<Text>().text = objs[i].name.Replace("Theme_","");
                btn.gameObject.name = objs[i].name;
                btnListener.OnClick += delegate (GameObject gb)
                {
                    ClickThemeButton(gb);
                };

                ChangeShowTheme(objs[i] as GameObject);
            }
        }
    }

    private void ChangeShowTheme(GameObject theme)
    {
        RectTransform[] olds = m_moduleRoot.GetComponentsInChildren<RectTransform>();

        for (int i = 0; i < olds.Length; i++)
        {
            if (olds[i].gameObject == m_moduleRoot)
                continue;

            Destroy(olds[i].gameObject);
        }

        RectTransform[] childs = theme.GetComponentsInChildren<RectTransform>();
        for (int j = 0; j < childs.Length; j++)
        {
            if (childs[j].name.IndexOf("Module_") == -1)
                continue;

            Button btn = Instantiate(m_button, m_moduleRoot.transform).GetComponent<Button>();
            btn.GetComponentInChildren<Text>().text = childs[j].name.Replace("Module_", "");
        }
    }

    private void ClickThemeButton(GameObject gb)
    {
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == gb.name)
            {
                Debug.Log("Find prefab: " + gb.name);
                themeName = gb.name;
                ChangeShowTheme(objs[i] as GameObject);
            }
        }
    }

    private void SaveThemeName()
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        manager.m_data.Data.themeName = themeName;
        Debug.Log("请手动重启！");
    }
}
