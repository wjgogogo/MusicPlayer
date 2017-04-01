using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowsSettings : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Debug.Log(GetWindowsSettingInfo());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public string GetWindowsSettingInfo()
    {
        string ret = string.Empty;

        foreach (var item in Screen.resolutions)
        {
            ret += item.ToString() + "\n";
        }

        ret += "fullScrren: " + Screen.fullScreen.ToString();

        return ret;
    }
}
