using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagsManager : MonoBehaviour
{
    /*
     * About setting tags
     * */
    public const string SETTING_FULLSCREEN_TAG = "FullScreen";
    public const string SETTING_MIN_MODE_TAG = "MinMode";
    public const string SETTING_CLOSE_TAG = "CloseApp";

    /*
     * About audio source tags
     * */
    public const string AUDIO_SOURCE = "PlayingAudio";


    /*
     * About audio control tags
     * */
    public const string AUDIO_SLIDER_TAG = "MusicProgressBar";
    public const string AUDIO_VOLUME_TAG = "MusicVolumeBar";
    public const string AUDIO_INFO_TAG = "MusicInfo";

    /*
     * About music list tags
     * */
    public const string LIST_ITEM_TAG = "ListItem";
    public const string FRESH_LIST_TAG = "FreshList";


    /*
     * About setting components
     * */
    public Toggle m_fullScreenToggle;
    public Button m_closeAppButton;
    public Button m_minModeButton;

    /*
     * About audio sources components
     * */
    public AudioSource m_audio;

    /*
     * About audio control components
     * */
    public Slider m_audioSlider;
    public Slider m_volumeSlider;
    public Text m_audioInfo;

    /*
     * About music list components
     * */
    public GameObject m_listItem;
    public Button m_freshButton;

    // Use this for initialization
    void Awake()
    {
        if (m_fullScreenToggle != null) m_fullScreenToggle.tag = SETTING_FULLSCREEN_TAG;
        if (m_closeAppButton != null) m_closeAppButton.tag = SETTING_CLOSE_TAG;
        if (m_minModeButton != null) m_minModeButton.tag = SETTING_MIN_MODE_TAG;

        if (m_audio != null) m_audio.tag = AUDIO_SOURCE;

        if (m_audio != null) m_audioSlider.tag = AUDIO_SLIDER_TAG;
        if (m_volumeSlider != null) m_volumeSlider.tag = AUDIO_VOLUME_TAG;
        if (m_audioInfo != null) m_audioInfo.tag = AUDIO_INFO_TAG;

        if (m_listItem != null) m_listItem.tag = LIST_ITEM_TAG;
        if (m_fullScreenToggle != null) m_freshButton.tag = FRESH_LIST_TAG;
    }

}
