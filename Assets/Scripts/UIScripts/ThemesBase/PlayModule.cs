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

    //lack a list control component

    public void PlaySong(bool status)
    {

    }

    public void PlayNextSong()
    {

    }

    public void PlayPreSong()
    {

    }

    public void SyncAudioSlider()
    {

    }

    public void SetVolume(float value)
    {

    }

    public void SetAudioInfo()
    {

    }

    private void Update()
    {
        SyncAudioSlider();
    }
}
