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

    //the audio source
    private AudioSource m_audio;

    // Use this for initialization
    void Start()
    {
        GetControlComponents();
        ControlInit();
    }

    /// <summary>
    /// get all components of control
    /// </summary>
    private void GetControlComponents()
    {
        if (m_audio == null)
            m_audio = GameObject.FindGameObjectWithTag(TagsManager.AUDIO_SOURCE).GetComponent<AudioSource>();

        if (m_volumeSlider == null)
            m_volumeSlider = GameObject.FindGameObjectWithTag(TagsManager.AUDIO_VOLUME_TAG).GetComponent<Slider>();

        if (m_audioSlider == null)
            m_audioSlider = GameObject.FindGameObjectWithTag(TagsManager.AUDIO_SLIDER_TAG).GetComponent<Slider>();

        if (m_audioInfo == null)
            m_audioInfo = GameObject.FindGameObjectWithTag(TagsManager.AUDIO_INFO_TAG).GetComponent<Text>();
    }

    /// <summary>
    /// initialize all components of control 
    /// </summary>
    private void ControlInit()
    {
        //audio init
        m_audio.volume = m_volumeSlider.value;

        //music progress bar init
        m_audioSlider.maxValue = m_audio.clip.length;
        m_audioSlider.onValueChanged.AddListener(ChangeAudioTimeline);

        //volume init
        m_volumeSlider.onValueChanged.AddListener(ChangeVolume);

        //music info init
        m_audioInfo.text = GetAudioInfo();
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
            if (!m_audio.isPlaying)
            {
                m_audio.Play();
            }
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
