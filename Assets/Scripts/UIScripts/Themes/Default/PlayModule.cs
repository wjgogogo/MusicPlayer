using UnityEngine;
using UnityEngine.UI;

public class PlayModule : ThemeModule
{
    [SerializeField]
    private Slider m_audioSlider;
    [SerializeField]
    private Slider m_volumeSlider;
    [SerializeField]
    private Text m_audioInfo;

    [SerializeField]
    private Toggle m_musicPlay;
    [SerializeField]
    private Button m_preSong;
    [SerializeField]
    private Button m_nextSong;

    public string TestMusicFilePath = "";
    //lack a naudio sources
    private NaudioSources m_audio=new NaudioSources();
    //lack a list control component

    private void Start()
    {
        //get the Naudio sources
        m_audio.LoadMusic(TestMusicFilePath);

        m_volumeSlider.onValueChanged.AddListener(SetVolume);
        m_audioSlider.maxValue = m_audio.TotalTime;
        m_audioSlider.onValueChanged.AddListener(SetAudioTime);
        m_musicPlay.onValueChanged.AddListener(PlaySong);
    }

    public void PlaySong(bool status)
    {
        m_audio.Play(status);
        Debug.Log(status);
    }

    public void PlayNextSong()
    {

    }

    public void PlayPreSong()
    {

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

    private void OnApplicationQuit()
    {
        m_audio.OnApplicationQuit();
    }
}
