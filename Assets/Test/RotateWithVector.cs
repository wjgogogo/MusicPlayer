using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWithVector : MonoBehaviour {
    public float Degree = 0.1f;
    public Transform Target;
    
	// Update is called once per frame
	void Update () {
        transform.RotateAround(Target.position, Vector3.up, Degree);	
	}
}
