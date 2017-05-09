using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    /*
     * all components of control declare in below
     * */
    public Slider m_audioSlider;
    public Slider m_volumeSlider;
    public Text m_audioInfo;

    public Toggle m_musicPlay;
    public Button m_preSong;
    public Button m_nextSong;

    //the audio source
    private AudioSource m_audio;

    // Use this for initialization
    void Start()
    {
        GetControlComponents();
    }

    /// <summary>
    /// get all components of control
    /// </summary>
    private void GetControlComponents()
    {
        UIComponentsManager uiManager = GameObject.FindGameObjectWithTag(UIComponentsManager.SELF_TAG).GetComponent<UIComponentsManager>();
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        if (manager.m_audio)
        {
            m_audio = manager.m_audio;
        }

        if (uiManager.m_volumeSlider)
        {
            m_volumeSlider = uiManager.m_volumeSlider;
            m_volumeSlider.onValueChanged.AddListener(ChangeVolume);

            m_volumeSlider.value = (float)manager.m_data.Data.m_volume;
        }

        if (uiManager.m_audioSlider)
        {
            m_audioSlider = uiManager.m_audioSlider;
            m_audioSlider.maxValue = m_audio.clip.length;
            m_audioSlider.onValueChanged.AddListener(ChangeAudioTimeline);

            m_audioSlider.value = (float)manager.m_data.Data.m_musicProgressVolume;
        }

        if (uiManager.m_audioInfo)
        {
            m_audioInfo = uiManager.m_audioInfo;
            m_audioInfo.text = GetAudioInfo();
        }

        if (uiManager.m_audioPlay)
        {
            m_musicPlay = uiManager.m_audioPlay;
            m_musicPlay.onValueChanged.AddListener(ChangePlayStatus);
        }

        if (uiManager.m_audioPreSong)
        {
            m_preSong = uiManager.m_audioPreSong;
        }

        if (uiManager.m_audioNextSong)
        {
            m_nextSong = uiManager.m_audioNextSong;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_audioSlider.value = m_audio.time;
    }

    /// <summary>
    /// change the time line of the music
    /// </summary>
    /// <param name="value"></param>
    private void ChangeAudioTimeline(float value)
    {
        if (Mathf.Abs(value - m_audio.time) > 0.02f)
        {
            m_audio.time = value;
        }
    }

    /// <summary>
    /// change the volume of this app
    /// </summary>
    /// <param name="value"></param>
    private void ChangeVolume(float value)
    {
        m_audio.volume = value;
    }

    private void ChangePlayStatus(bool isPlaying)
    {
        ParticleSystem particle = m_audioSlider.GetComponentInChildren<ParticleSystem>();

        if (isPlaying)
        {
            particle.Play();
            m_audio.Play();
            m_audioInfo.text = GetAudioInfo();
        }
        else
        {
            particle.Stop();
            m_audio.Pause();
        }
    }

    /// <summary>
    /// get the infomation of the pitched music
    /// </summary>
    /// <returns></returns>
    public string GetAudioInfo()
    {
        string ret = string.Empty;

        ret += "name: " + m_audio.name;
        ret += "\nchannels num: " + m_audio.clip.channels;
        ret += "\nlength: " + m_audio.clip.length;
        
        return ret;
    }
}
