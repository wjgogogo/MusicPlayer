using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemOperation : MonoBehaviour {

    private FileOperation operation;
    private Text ItemName;
    void Start()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        operation = objManager.DataManager.GetComponent<FileOperation>();
        ItemName = GetComponentInChildren<Text>();
    }

    public void Delete()
    {
        operation.DeleteFileByName(ItemName.text);
    }
}
