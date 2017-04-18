using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsManager : MonoBehaviour
{
    public const string SELF_TAG = "ComponentsManager";

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
    public Toggle m_audioPlay;
    public Button m_audioPreSong;
    public Button m_audioNextSong;

    /*
     * About music list components
     * */
    public GameObject m_listItem;
    public Button m_freshButton;

    /*
     * Manage data
     * */
    public GameObject DataManager;

    public Text m_info;
    private void Start()
    {
        m_info.text = ToString();
    }

    override public string ToString()
    {
        string ret = "";

        if (m_fullScreenToggle != null) ret += m_fullScreenToggle.name + "\n";
        if (m_closeAppButton != null) ret += m_closeAppButton.name + "\n";
        if (m_minModeButton != null) ret += m_minModeButton.name + "\n";

        if (m_audio != null) ret += m_audio.name + "\n";

        if (m_audioSlider != null) ret += m_audioSlider.name + "\n";
        if (m_volumeSlider != null) ret += m_volumeSlider.name + "\n";
        if (m_audioInfo != null) ret += m_audioInfo.name + "\n";
        if (m_audioPlay != null) ret += m_audioPlay.name + "\n";
        if (m_audioPreSong != null) ret += m_audioPreSong.name + "\n";
        if (m_audioNextSong != null) ret += m_audioNextSong.name + "\n";

        if (m_listItem != null) ret += m_listItem.name + "\n";
        if (m_freshButton != null) ret += m_freshButton.name + "\n";

        return ret;
    }
}
