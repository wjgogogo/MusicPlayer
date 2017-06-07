using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderManager : MonoBehaviour
{
    private Color checkColor = new Color(0.0f, 0.3f, 1.0f, 0.5f);
    private Color unCheckColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private FolderBrowserManager manager;
    private Button folderButton;
    private Image arrow;
    private bool isSelected = false;
    private bool hasChild = true;
    public string folderPath;

    private void Start()
    {
        manager = GameObject.Find(FolderBrowserDialog.browserName).GetComponent<FolderBrowserManager>();
        folderButton = transform.GetChild(0).GetComponent<Button>();
        folderButton.onClick.AddListener(ClickEvent);
        arrow = GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Image>();
    }

    private void ClickEvent()
    {
        if (!isSelected)
        {
            isSelected = true;
            bool needScan = GetComponent<Transform>().GetChild(1).childCount == 0 && hasChild;
            manager.updateSelect(gameObject, needScan);
        }
        changeState(isSelected);
        GetComponent<Transform>().GetChild(1).gameObject.SetActive(!GetComponent<Transform>().GetChild(1).gameObject.activeSelf);
        rotateArrow();
    }

    public void changeState(bool state)
    {
        isSelected = state;
        GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Image>().color = isSelected ? checkColor : unCheckColor;
    }

    public void hiddenArrow()
    {
        GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Image>().color = unCheckColor;
        hasChild = false;
    }

    private void rotateArrow()
    {
        float angle = arrow.transform.localEulerAngles.z;
        arrow.transform.Rotate(0, 0, angle == 0 ? -90 : 90);
    }
}