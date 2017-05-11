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

    //lack a naudio sources
    private NaudioSources m_audio;
    //lack a list control component

    private void Start()
    {
        if (m_volumeSlider) { m_volumeSlider.onValueChanged.AddListener(SetVolume); }
        if (m_audioSlider) { m_audioSlider.onValueChanged.AddListener(SetAudioTime); }
        //get the Naudio sources
        
    }

    public void PlaySong(bool status)
    {
        m_audio.Play(status);
    }

    public void PlayNextSong()
    {

    }

    public void PlayPreSong()
    {

    }

    private void SyncAudioSlider()
    {
        m_audioSlider.value = m_audio.CurrentTime / m_audio.TotalTime;
    }

    private void SetVolume(float value)
    {
        m_audio.SetVolume(value);
    }

    private void SetAudioTime(float time)
    {
        m_audio.SetTime(time);
    }

    private void SetAudioInfo()
    {
        m_audioInfo.text = m_audio.Name;
    }

    private void Update()
    {
        //SyncAudioSlider();
    }
}
