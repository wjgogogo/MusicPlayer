using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : AudioVisualInfo
{
    [SerializeField]
    private int m_cubeNumber = 10;

    [SerializeField]
    private float m_radius = 10.0f;

    [SerializeField]
    private GameObject m_cube;
    [SerializeField]
    private Material[] m_cubeMaterials;

    [SerializeField]
    private float m_maxLength = 200.0f;
    [SerializeField]
    private float m_recoveryRate = 20f;

    private List<GameObject> m_cubes;

    // Use this for initialization
    new void Start()
    {
        base.Start();
        SetSamplesSize(m_cubeNumber);

        m_cubes = new List<GameObject>();
        CreateVisualComponents();
    }

    override protected void CreateVisualComponents()
    {
        Vector2 pos = Vector2.zero;
        for (int i = 0; i < m_cubeNumber; i++)
        {
            pos.x = m_radius * Mathf.Cos(2 * Mathf.PI / m_cubeNumber * i);
            pos.y = m_radius * Mathf.Sin(2 * Mathf.PI / m_cubeNumber * i);
            GameObject obj = Instantiate(m_cube, transform);
            obj.transform.position = new Vector3(pos.x, 0, pos.y);
            obj.GetComponent<MeshRenderer>().material = m_cubeMaterials[Mathf.RoundToInt(UnityEngine.Random.value * 3)];
            m_cubes.Add(obj);
        }
    }

    override protected void UpdateComponentsBySamples()
    {
        m_maxLength = m_maxLength < 1 ? 1 : m_maxLength;
        bool update = false;
        if (m_recoveryRate < 0.1)
            update = true;

        for (int i = 0; i < m_cubes.Count; i++)
        {
            float oldY = m_cubes[i].transform.localScale.y;
            float nowY = 0.0f;

            if (Samples[0, i] * m_maxLength - m_cubes[i].transform.localScale.y > 0 || update)
            {
                nowY = Samples[0, i] * m_maxLength;
            }
            else
            {
                nowY = 0.0f;
                nowY = Mathf.SmoothStep(oldY, nowY, m_recoveryRate * Time.deltaTime);
            }

            m_cubes[i].transform.localScale = new Vector3(1, nowY, 1);
        }
    }
}
