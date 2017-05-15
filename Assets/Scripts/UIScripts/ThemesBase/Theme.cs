using UnityEngine;

public class Theme : MonoBehaviour
{
    [SerializeField]
    protected ListModule m_listModule;
    [SerializeField]
    protected PlayModule m_playModule;
    [SerializeField]
    protected SettingModule m_settingModule;

    private ThemeModule[] m_modules;

    [SerializeField]
    protected KeyCode m_callOutKey = KeyCode.Escape;

    private bool m_calledMenu = true;
    private ComponentsManager manager;

    protected void Awake()
    {
        manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
    }

    protected void OnEnable()
    {
        manager.m_data.Init();
        m_playModule.Init();
        m_listModule.Init();
        m_settingModule.Init();

    }

    protected void Start()
    {
        Init();
    }

    private void Init()
    {
        m_modules = gameObject.GetComponentsInChildren<ThemeModule>();

        for (int i = 0; i < m_listModule.Listeners.Count; i++)
        {
            m_listModule.Listeners[i].OnClick += MusicItemOnclick;
        }

        m_playModule.PreSongOnClick.OnClick += OnPreSongClick;
        m_playModule.NextSongOnClick.OnClick += OnNextSongOnClick;

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);
        
        GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        GetComponent<Canvas>().worldCamera = manager.m_UICamera;
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_callOutKey))
        {
            m_calledMenu = !m_calledMenu;
            SetAllModulesActive(m_calledMenu);
        }
    }

    /// <summary>
    /// set module status --- active or not
    /// </summary>
    /// <param name="i"></param>
    /// <param name="status"></param>
    /// <returns>return true if succeed</returns>
    protected bool SetActiveModule(int i, bool status)
    {
        if (i >= m_modules.Length)
            return false;

        m_modules[i].SetActive(status);
        return true;
    }

    /// <summary>
    /// set index one's active is status and other's is contrary
    /// </summary>
    /// <param name="index"></param>
    /// <param name="status"></param>
    /// <returns>return true if succeed</returns>
    protected bool ActiveOneModule(int index, bool status)
    {
        if (index >= m_modules.Length)
            return false;

        for (int i = 0; i < m_modules.Length; i++)
        {
            if (i == index)
            {
                if (m_modules[i].IsActive != status)
                    SetActiveModule(i, status);
            }
            else if (m_modules[i].IsActive == status)
                SetActiveModule(i, !status);
        }

        return true;
    }

    /// <summary>
    /// set all the modules status
    /// </summary>
    /// <param name="status"></param>
    protected void SetAllModulesActive(bool status)
    {
        for (int i = 0; i < m_modules.Length; i++)
        {
            SetActiveModule(i, status);
        }
    }

    private void MusicItemOnclick(GameObject gb)
    {
        MusicItemControl item = gb.GetComponent<MusicItemControl>();

        Debug.Log(item.name);
        m_playModule.PlaySong(item.FilePath, true);

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);
    }

    private void OnPreSongClick(GameObject gb)
    {
        string currentSong = m_playModule.CurrentPlaySongPath;
        string[] allSongsPath = m_listModule.FilesPath;
        for (int i = 0; i < allSongsPath.Length; i++)
        {
            if (allSongsPath[i] == currentSong)
            {
                if (i != 0)
                {
                    m_playModule.PlaySong(allSongsPath[i - 1], true);
                }
            }
        }

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);
    }

    private void OnNextSongOnClick(GameObject gb)
    {
        string currentSong = m_playModule.CurrentPlaySongPath;
        string[] allSongsPath = m_listModule.FilesPath;
        for (int i = 0; i < allSongsPath.Length; i++)
        {
            if (allSongsPath[i] == currentSong)
            {
                if (i != allSongsPath.Length - 1)
                {
                    m_playModule.PlaySong(allSongsPath[i + 1], true);
                }
            }
        }

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);
    }
    
}
