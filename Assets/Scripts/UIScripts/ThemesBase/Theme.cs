using UnityEngine;

public class Theme : MonoBehaviour
{
    [SerializeField]
    private Vector2 m_maxOffsetDegree = new Vector2(15, 15);
    [SerializeField]
    protected ListModule m_listModule;
    [SerializeField]
    protected PlayModule m_playModule;
    [SerializeField]
    protected SettingModule m_settingModule;

    private ThemeModule[] m_modules;

    [SerializeField]
    protected KeyCode m_callOutKey = KeyCode.Escape;

    protected bool m_calledMenu = true;
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
        Init();
    }

    private void Init()
    {
        m_modules = gameObject.GetComponentsInChildren<ThemeModule>();

        m_playModule.PlaySong(manager.m_data.Data.playingMusicNamePath, false,
           (float)manager.m_data.Data.musicProgressValue);
        m_playModule.SetSongSlider((float)manager.m_data.Data.musicProgressValue);
        m_playModule.SetVolumeSlider((float)manager.m_data.Data.volume);
        m_playModule.PreSongOnClick.OnClick += OnPreSongClick;
        m_playModule.NextSongOnClick.OnClick += OnNextSongOnClick;

        GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        GetComponent<Canvas>().worldCamera = manager.m_UICamera;

        for (int i = 0; i < m_listModule.Listeners.Count; i++)
        {
            m_listModule.Listeners[i].OnClick += MusicItemOnclick;
        }

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);

        m_settingModule.GetDialogPath(FreshMusicList);
       
    }

    protected void Update()
    {
        if (Input.GetKeyDown(m_callOutKey))
        {
            m_calledMenu = !m_calledMenu;
            SetAllModulesActive(m_calledMenu);
        }

        if (m_calledMenu)
        {
            RotateWithCamera();
        }
    }

    private void FreshMusicList(string path)
    {
        m_listModule.FreshMusicList(path);
        for (int i = 0; i < m_listModule.Listeners.Count; i++)
        {
            m_listModule.Listeners[i].OnClick += MusicItemOnclick;
        }

        m_listModule.SetPlayStatus(m_playModule.CurrentPlaySongPath);
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
        const float Click_Delta_Time = 1f;
        MusicItemControl item = gb.GetComponent<MusicItemControl>();

        if (Mathf.Abs(Time.time - item.DeltaTime) > Click_Delta_Time)
        {
            item.DeltaTime = Time.time;
            return;
        }

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

    // Rotate with camera
    void RotateWithCamera()
    {

        Vector2 offset = new Vector2(Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2);

        offset.x = offset.x / Screen.currentResolution.width / 2 * m_maxOffsetDegree.x;
        offset.y = -offset.y / Screen.currentResolution.height / 2 * m_maxOffsetDegree.y;

        transform.eulerAngles = new Vector3(offset.y, offset.x);
    }
}
