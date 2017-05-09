using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponentsManager : MonoBehaviour
{

    public const string SELF_TAG = "UIComponentsManager";

    /*
     * UI root node
     * */
    public GameObject m_UIRoot;

    /*
     * About setting components
     * */
    public Toggle m_fullScreenToggle;
    public Button m_closeAppButton;
    public Button m_minModeButton;

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
     * Debug components
     * */
    public Text m_info;

}
