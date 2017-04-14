using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : AudioVisualInfo
{

    public int m_cubeNumber = 10;
    public Vector2 m_origin = Vector2.zero;
    public float m_radius = 10.0f;
    public GameObject m_cube;

    public Material[] m_cubeMaterials;

    public float m_maxLength = 20.0f;
    public float m_restroedTime = 0.2f;
    public float m_updateDeltaTime = 0.1f;

    private List<GameObject> m_cubes;

    private float m_preFrameTime;

    // Use this for initialization
#pragma warning disable CS0108 // 成员隐藏继承的成员；缺少关键字 new
    void Start()
#pragma warning restore CS0108 // 成员隐藏继承的成员；缺少关键字 new
    {
        base.Start();
        m_cubes = new List<GameObject>();
        CreateCubes();
        m_preFrameTime = Time.time;
    }

    /// <summary>
    /// Create all cubes
    /// </summary>
    private void CreateCubes()
    {
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < m_cubeNumber; i++)
        {
            pos.x = m_radius * Mathf.Cos(2 * Mathf.PI / m_cubeNumber * i);
            pos.y = m_radius * Mathf.Sin(2 * Mathf.PI / m_cubeNumber * i);
            GameObject obj = Instantiate(m_cube, transform);
            obj.transform.position = new Vector3(pos.x, 0, pos.y);
            obj.GetComponent<MeshRenderer>().material = m_cubeMaterials[Mathf.RoundToInt(Random.value * 3)];
            m_cubes.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!UpdateCubesLength())
        {
            for (int i = 0; i < m_cubes.Count; i++)
            {
                float times = (m_restroedTime / Time.deltaTime);
                float y = m_cubes[i].transform.localScale.y * (times - 1) / times;
                m_cubes[i].transform.localScale = new Vector3(1, y, 1);
            }
        }
    }

    /// <summary>
    /// Update the length of cubes
    /// </summary>
    /// <returns>true if updated,false if not </returns>
    private bool UpdateCubesLength()
    {
        if (Time.time - m_preFrameTime < m_updateDeltaTime)
        {
            Debug.Log(m_preFrameTime - Time.time);
            return false;
        }

        m_maxLength = m_maxLength < 1 ? 1 : m_maxLength;
        UpdateSamples();
        for (int i = 0; i < m_cubes.Count; i++)
        {
            m_cubes[i].transform.localScale = new Vector3(1, Samples[i] * m_maxLength, 1);
        }
        m_preFrameTime = Time.time;
        return true;
    }
}
