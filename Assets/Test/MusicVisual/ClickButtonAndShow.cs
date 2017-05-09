using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButtonAndShow : MonoBehaviour {

    public GameObject m_clicked;
    
    // Use this for initialization
    void Start () {

	}


	// Update is called once per frame
	void Update () {
        m_clicked = InputTools.GetMouseClickObj();
        if (m_clicked != null)
        {
            
        }
    }
}
