using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButtonAndShow : MonoBehaviour {

    public GameObject m_button;

    private AudioControl m_audioControl;

    // Use this for initialization
    void Start () {
        m_audioControl = new AudioControl();
	}


	// Update is called once per frame
	void Update () {
        m_button = InputTools.GetMouseClickObj();
        if (m_button != null)
        {
            
        }
    }
}
