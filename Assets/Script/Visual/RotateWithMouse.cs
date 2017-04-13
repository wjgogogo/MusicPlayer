using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithMouse : MonoBehaviour {

    public Vector2 m_maxOffsetDegree = new Vector2(15, 15);

	// Use this for initialization
	void Start () {
        Debug.Log(Screen.width + " " + Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Input.mousePosition.ToString());
        Vector2 offset = new Vector2(Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2);

        offset.x = -offset.x / Screen.width / 2 * m_maxOffsetDegree.x;
        offset.y = -offset.y / Screen.height / 2 * m_maxOffsetDegree.y;

        Camera.main.transform.eulerAngles = new Vector3(offset.y, offset.x);

    }
}
