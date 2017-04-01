using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVisualModule : AudioVisualInfo
{
    // Use this for initialization
    void Start()
    {
    }

    public int height = 4;
    public int space = 4;
    
    // Update is called once per frame
    void Update()
    {
        height = height < 1 ? 1 : height;
        int posx = 0;
        UpdateSamples();
        for (int i = 0; i < Samples.Length; i++)
        {
            Debug.DrawLine(new Vector3(posx / 100.0f, 0, 0), new Vector3(posx / 100.0f, Samples[i] * height, 0), Color.green);
            posx += space;
        }
    }

}
