using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListModule : ThemeModule
{
    [SerializeField]
    private GameObject m_listItem;

    [SerializeField]
    private float m_itemHeight;

    [SerializeField]
    private RectTransform m_itemRoot;

    private List<string> m_paths = new List<string>();

    private void Start()
    {
        m_listItem.GetComponent<RectTransform>().
          SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_itemHeight);
    }

    /// <summary>
    /// fresh music list
    /// </summary>
    public void FreshMusicList()
    {
        for (int i = 0; i < m_paths.Count; i++)
        {
            GetMusicList(m_paths[i]);
        }
    }

    /// <summary>
    /// return true if exist in list
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    private bool ExistInList(string name)
    {
        Text[] names = m_itemRoot.gameObject.GetComponentsInChildren<Text>();

        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].text == name)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// get music list from file
    /// </summary>
    public void GetMusicList(string path)
    {
        if (m_paths.Contains(path))
            m_paths.Add(path);

        string[] dir = FileTools.GetFilesByRecursion(path, "*.mp3", 3);

        for (int i = 0; i < dir.Length; i++)
        {
            CreatListItem(FileTools.GetFileNameWithoutExtension(dir[i]));
        }
    }

    /// <summary>
    /// Creat a item by name
    /// </summary>
    /// <param name="musicName"></param>
    private void CreatListItem(string musicName)
    {
        if (ExistInList(musicName))
            return;

        Button newItem = Instantiate(m_listItem, m_itemRoot).GetComponent<Button>();
        newItem.GetComponent<Text>().text = musicName;
    }

}
