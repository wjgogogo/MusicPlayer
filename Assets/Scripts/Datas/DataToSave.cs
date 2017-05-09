using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataToSave
{
    // to save the ui data
    public string m_themeName = "Theme_default";

    // data of setting
    public bool m_fullScreen = false;
    public string m_playingMusicName = "";
    public double m_musicProgressVolume = 0.0;
    public double m_volume = 1.0;

    public override string ToString()
    {
        string str = "";

        str += "theme name : " + m_themeName + "\n";
        str += "full screen : " + m_fullScreen + "\n";
        str += "playing music name : " + m_playingMusicName + "\n";
        str += "music progress volume : " + m_musicProgressVolume + "\n";
        str += "music volume : " + m_volume + "\n";
        return str;
    }
}
