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
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        if (objManager.m_audio)
        {
            m_audio = objManager.m_audio;
        }

        if (objManager.m_volumeSlider)
        {
            m_volumeSlider = objManager.m_volumeSlider;
            m_volumeSlider.onValueChanged.AddListener(ChangeVolume);
            m_audio.volume = m_volumeSlider.value;
        }

        if (objManager.m_audioSlider)
        {
            m_audioSlider = objManager.m_audioSlider;
            m_audioSlider.maxValue = m_audio.clip.length;
            m_audioSlider.onValueChanged.AddListener(ChangeAudioTimeline);
        }

        if (objManager.m_audioInfo)
        {
            m_audioInfo = objManager.m_audioInfo;
            m_audioInfo.text = GetAudioInfo();
        }

        if (objManager.m_audioPlay)
        {
            m_musicPlay = objManager.m_audioPlay;
            m_musicPlay.onValueChanged.AddListener(ChangePlayStatus);
        }

        if (objManager.m_audioPreSong)
        {
            m_preSong = objManager.m_audioPreSong;
        }

        if (objManager.m_audioNextSong)
        {
            m_nextSong = objManager.m_audioNextSong;
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
        if (isPlaying && !m_audio.isPlaying)
        {
            m_audio.Play();
        }
        else if (!isPlaying && m_audio.isPlaying)
        {
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

        ret += "name: " + m_audio.clip.name;
        ret += "\nchannels num: " + m_audio.clip.channels;
        ret += "\nlength: " + m_audio.clip.length;

        return ret;
    }
}
