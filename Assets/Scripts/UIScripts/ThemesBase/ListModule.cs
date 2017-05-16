using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListModule : ThemeModule
{
    public string TestPath = @"F:\音乐";

    [SerializeField]
    private GameObject m_listItem;

    [SerializeField]
    private float m_itemHeight;

    [SerializeField]
    private RectTransform m_itemRoot;

    [SerializeField]
    private int m_pathDepth = 3;

    /// <summary>
    /// all music files full path
    /// </summary>
    private List<string> m_filesPath = new List<string>();

    /// <summary>
    /// all music folder path
    /// </summary>
    private List<string> m_paths = new List<string>();

    private List<MusicItemControl> m_items = new List<MusicItemControl>();

    private List<UIEventListener> m_listeners = new List<UIEventListener>();

    public List<UIEventListener> Listeners
    {
        get
        {
            return m_listeners;
        }
    }

    public string[] FilesPath
    {
        get
        {
            return m_filesPath.ToArray();
        }
    }

    public void Init()
    {
        m_listItem.GetComponent<RectTransform>().
          SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_itemHeight);
        GetMusicList(TestPath);
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
    private void GetMusicList(string path)
    {
        if (m_paths.Contains(path))
            m_paths.Add(path);

        string[] files = FileTools.GetFilesByRecursion(path, "*.mp3", m_pathDepth);

        m_filesPath.AddRange(files);

        for (int i = 0; i < files.Length; i++)
        {
            CreatListItem(files[i]);
        }
    }

    /// <summary>
    /// Creat a item by path
    /// </summary>
    /// <param name="path"></param>
    private void CreatListItem(string path)
    {
        string name = FileTools.GetFileNameWithoutExtension(path);
        if (ExistInList(name))
            return;

        Button newItem = Instantiate(m_listItem, m_itemRoot).GetComponent<Button>();
        newItem.GetComponentInChildren<MusicItemControl>().Text = name;
        newItem.GetComponentInChildren<MusicItemControl>().FilePath = path;
        UIEventListener listener = newItem.gameObject.AddComponent<UIEventListener>();
        m_listeners.Add(listener);

        m_items.Add(newItem.GetComponentInChildren<MusicItemControl>());

        newItem.GetComponentInChildren<MusicItemControl>().SetImageEnabled(false);
    }

    /// <summary>
    /// set current play song image by path
    /// </summary>
    /// <param name="path"></param>
    public void SetPlayStatus(string path)
    {
        for (int i = 0; i < m_items.Count; i++)
        {
            if (path == m_items[i].FilePath)
                m_items[i].SetImageEnabled(true);
            else
                m_items[i].SetImageEnabled(false);
        }
    }
}
