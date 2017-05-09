using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private PathOperation operation;

    // Use this for initialization
    private void Start()
    {
        operation = gameObject.GetComponent<PathOperation>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            operation.DeleteAllPath();
        }
    }
}