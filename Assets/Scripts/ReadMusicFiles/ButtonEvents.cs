using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvents : MonoBehaviour
{
    private FolderBrowser folderBrowser;

    private void Start()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        folderBrowser = objManager.DataManager.GetComponent<FolderBrowser>();
    }

    public void CommitEvent()
    {
        folderBrowser.CommitButtonClick();
    }

    public void CancelEvent()
    {
        folderBrowser.CancelButtonClick();
    }
}