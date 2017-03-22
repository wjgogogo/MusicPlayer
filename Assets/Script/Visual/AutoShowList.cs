using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AutoShowList : MonoBehaviour
{

    public RectTransform parent;
    public int itemHeight = 30;
    public GameObject listItem;
    public int listNum = 20;
    
    // Use this for initialization
    void Start()
    {
        //freshList();
    }

    public void freshList()
    {
        RectTransform[] olds = parent.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < olds.Length; i++)
        {
            if(olds[i] != parent)
                Destroy(olds[i].gameObject);
        }
        //set rect height
        parent.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Vertical, itemHeight * listNum);

        for (int i = 0; i < listNum; i++)
        {
            //set instantiate
            GameObject newItem = Instantiate(listItem, parent);
            Vector2 pos = new Vector3(0, i * (-itemHeight));
            //set position
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = new Vector2(0, 0);
            rectTransform.offsetMax = new Vector2(0, itemHeight);
            
            rectTransform.anchoredPosition = pos;
            //set text
            newItem.GetComponentInChildren<Text>().text = "item: " + i.ToString();
        }
        //Debug.Log("FreshOk");
    }
    // Update is called once per frame
    void Update()
    {

    }
}
