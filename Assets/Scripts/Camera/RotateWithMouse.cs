using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour {

    public Vector2 m_maxOffsetDegree = new Vector2(15, 15);

    private Transform m_UIRoot;

	// Use this for initialization
	void Start () {
        m_UIRoot = transform;
    }
	
	// Update is called once per frame
	void Update () {

        Vector2 offset = new Vector2(Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2);
        
        offset.x = offset.x / Screen.currentResolution.width/ 2 * m_maxOffsetDegree.x;
        offset.y = -offset.y / Screen.currentResolution.height / 2 * m_maxOffsetDegree.y;

        m_UIRoot.eulerAngles = new Vector3(offset.y, offset.x);
    }
}
