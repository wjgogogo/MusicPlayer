using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AutoShowList : MonoBehaviour
{

    public int m_itemHeight = 30;
    //public int m_listNum = 20;
    public GameObject m_listItem;
    public RectTransform m_parent;

    public float m_delayFreshSconds = 0.5f;
    public Button m_freshButton;

    //music list count
    int m_musicCount = 0;

    //first launch
    bool m_launch = true;

    // Use this for initialization
    void Start()
    {
        GetControlComponents();
    }

    private void GetControlComponents()
    {
        UIComponentsManager objManager = GameObject.FindGameObjectWithTag(UIComponentsManager.SELF_TAG).GetComponent<UIComponentsManager>();

        if (objManager.m_freshButton)
        {
            m_freshButton = objManager.m_freshButton;
            m_freshButton.onClick.AddListener(ClickFreshButton);
        }
    }

    /// <summary>
    /// call the folder to choose music
    /// </summary>
    private void ClickFreshButton()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        FileOperation file = objManager.DataManager.GetComponent<FileOperation>();

        file.SetPathAndSearchFiles();
    }

    /// <summary>
    /// fresh the list of musics
    /// </summary>
    void FreshMusicList()
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        FileOperation file = manager.DataManager.GetComponent<FileOperation>();

        if (m_musicCount >= file.MusicsResult.Count)
        {
            if (m_launch)
            {
                AnalyseMusic analyseMusic = manager.DataManager.GetComponent<AnalyseMusic>();
                if (!manager.m_data.Data.m_playingMusicName.Equals("Music"))
                {
                    string value;
                    file.MusicsResult.TryGetValue(manager.m_data.Data.m_playingMusicName, out value);
                    manager.m_audio.name = manager.m_data.Data.m_playingMusicName;
                    analyseMusic.LoadMusic(value);
                }
                m_launch = false;
            }
            return;
        }

        // set the height of the item
        m_listItem.GetComponent<RectTransform>().
            SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_itemHeight);
        int count = 0;

        foreach (var item in file.MusicsResult)
        {
            if (count < m_musicCount)
            {
                count++;
                continue;
            }

            //set instantiate
            GameObject newItem = Instantiate(m_listItem, m_parent);

            string str = item.Key;
            //set text
            newItem.GetComponentInChildren<Text>().text = str;
            m_musicCount++;
        }
    }

    /// <summary>
    /// Count the delay times
    /// </summary>
    private int m_counts = 0;

    // Update is called once per frame
    void Update()
    {
        if (m_counts >= Mathf.CeilToInt(m_delayFreshSconds / Time.fixedDeltaTime))
        {
            FreshMusicList();
            m_counts = 0;
        }
        m_counts++;
    }
}
