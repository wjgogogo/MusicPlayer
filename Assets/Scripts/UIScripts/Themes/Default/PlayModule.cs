using UnityEngine;
using UnityEngine.UI;

public class PlayModule : ThemeModule
{
    [SerializeField]
    protected Slider m_audioSlider;
    [SerializeField]
    protected Slider m_volumeSlider;
    [SerializeField]
    protected Text m_audioInfo;

    [SerializeField]
    protected Toggle m_musicPlay;
    [SerializeField]
    protected Button m_preSong;
    [SerializeField]
    protected Button m_nextSong;

    private string m_currentPlaySongPath;

    private UIEventListener m_preSongOnClick;
    private UIEventListener m_nextSongOnClick;

    //lack a naudio sources
    private NaudioSources m_audio;

    public UIEventListener PreSongOnClick
    {
        get
        {
            return m_preSongOnClick;
        }
    }

    public UIEventListener NextSongOnClick
    {
        get
        {
            return m_nextSongOnClick;
        }
    }

    public string CurrentPlaySongPath
    {
        get
        {
            return m_currentPlaySongPath;
        }
    }

    //lack a list control component

    public void Init()
    {
        m_audio = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>().m_audio;
        m_volumeSlider.onValueChanged.AddListener(SetVolume);
        m_audioSlider.onValueChanged.AddListener(SetAudioTime);
        m_musicPlay.onValueChanged.AddListener(PlaySong);
        m_preSongOnClick = m_preSong.gameObject.AddComponent<UIEventListener>();
        m_nextSongOnClick = m_nextSong.gameObject.AddComponent<UIEventListener>();
        if (m_audio != null)
            m_audioSlider.maxValue = m_audio.TotalTime;
    }

    public void PlaySong(string path, bool status)
    {
        m_currentPlaySongPath = path;
        m_audio.LoadMusic(path);

        if (m_musicPlay.isOn == status)
            PlaySong(status);
        else
            m_musicPlay.isOn = status;
    }

    private void PlaySong(bool status)
    {
        m_audioSlider.maxValue = m_audio.TotalTime;
        m_audioInfo.text = m_audio.Name;
        m_audio.Play(status);
    }

    private void SyncAudioSlider()
    {
        m_audioSlider.value = m_audio.CurrentTime;
    }

    private void SetVolume(float value)
    {
        m_audio.Volume = value;
    }

    private void SetAudioTime(float time)
    {
        if (Mathf.Abs(time - m_audio.CurrentTime) > 1.0f)
            m_audio.CurrentTime = time;
    }

    private void SetAudioInfo()
    {
        m_audioInfo.text = m_audio.Name;
    }

    private void Update()
    {
        SyncAudioSlider();
    }
    
}
