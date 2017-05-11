using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderOpetation : MonoBehaviour
{
    private FolderBrowser folderBrowser;
    private Animator folderAnimator;
    private string switchKey = "checked";
    private bool isCheck = false;
    private bool showChildren = false;
    private bool hiddenArrow = false;
    private Color unCheckColor;
    private Color checkColor = new Color(0.5f, 1.0f, 1.0f, 1.0f);
    private Color hiddenColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    private string folderPath = null;

    private void Start()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        folderBrowser = objManager.DataManager.GetComponent<FolderBrowser>();
        unCheckColor = GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Image>().color;
        folderAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// 处理点击事件
    /// </summary>
    public void OnClick()
    {
        GetComponent<Transform>().GetChild(1).gameObject.SetActive(!GetComponent<Transform>().GetChild(1).gameObject.activeSelf);
        if (!isCheck)
        {
            isCheck = true;
            bool update = GetComponent<Transform>().GetChild(1).childCount == 0 && !hiddenArrow;
            folderBrowser.UpdateSelect(gameObject, GetComponent<Transform>().GetChild(1).GetComponent<Transform>(), update);
        }
        showChildren = !showChildren;
        ChangeCheckState(isCheck);
    }

    /// <summary>
    /// 改变图标和文字的状态
    /// </summary>
    /// <param name="check"></param>
    public void ChangeCheckState(bool check)
    {
        if (!hiddenArrow)
        {
            GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Image>().color = check ? checkColor : unCheckColor;
            folderAnimator.SetBool(switchKey, showChildren);
        }
        GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Image>().color = check ? checkColor : unCheckColor;
        GetComponent<Transform>().GetChild(0).GetChild(2).GetComponent<Text>().color = check ? checkColor : unCheckColor;
        isCheck = check;
    }

    public void HiddenArrow()
    {
        GetComponent<Transform>().GetChild(0).GetChild(0).GetComponent<Image>().color = hiddenColor;
        hiddenArrow = true;
    }

    public void SetFolderPath(string path)
    {
        folderPath = path;
    }

    public string GetFolderPath()
    {
        return folderPath;
    }
}