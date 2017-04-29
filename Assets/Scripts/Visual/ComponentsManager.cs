using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentsManager : MonoBehaviour
{
    public const string SELF_TAG = "ComponentsManager";

    /*
     * About audio sources components
     * */
    public AudioSource m_audio;

    /*
     * Manage data
     * */
    public GameObject DataManager;

    /*
     * data analysis
     * */
    public DataAnalysis m_data;
}
