using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVisualModule : AudioVisualInfo
{
    // Use this for initialization
#pragma warning disable CS0108 // 成员隐藏继承的成员；缺少关键字 new
    void Start()
#pragma warning restore CS0108 // 成员隐藏继承的成员；缺少关键字 new
    {
        base.Start();
    }

    public int m_height = 4;
    public int m_space = 4;
    
    // Update is called once per frame
    void Update()
    {
        m_height = m_height < 1 ? 1 : m_height;
        int posx = 0;
        UpdateSamples();
        for (int i = 0; i < Samples.Length; i++)
        {
            Debug.DrawLine(new Vector3(posx / 100.0f, 0, 0), new Vector3(posx / 100.0f, Samples[i] * m_height, 0), Color.green);
            posx += m_space;
        }
    }

}
