using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AutoShowList : MonoBehaviour
{

    public int m_itemHeight = 30;
    public int m_listNum = 20;
    public GameObject m_listItem;
    public RectTransform m_parent;

    public float m_delayFreshSconds = 0.5f;
    public Button m_freshButton;

    // Use this for initialization
    void Start()
    {
        GetControlComponents();
    }

    private void GetControlComponents()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        if (objManager.m_freshButton)
        {
            m_freshButton = objManager.m_freshButton;
            m_freshButton.onClick.AddListener(FreshList);
        }
    }

    private void FreshList()
    {
        RectTransform[] olds = m_parent.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < olds.Length; i++)
        {
            if (olds[i] != m_parent)
                Destroy(olds[i].gameObject);
        }

        // set the height of the item
        m_listItem.GetComponent<RectTransform>().
            SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_itemHeight);

        for (int i = 0; i < m_listNum; i++)
        {
            //set instantiate
            GameObject newItem = Instantiate(m_listItem, m_parent);

            //set text
            newItem.GetComponentInChildren<Text>().text = "item: " + i.ToString();
        }
    }

    /// <summary>
    /// Count the delay times
    /// </summary>
    private int m_counts = 0;
    // Update is called once per frame
    void Update()
    {
        if (m_counts == Mathf.CeilToInt(m_delayFreshSconds / Time.fixedDeltaTime))
        {
            FreshList();
        }
        m_counts++;
    }
}
