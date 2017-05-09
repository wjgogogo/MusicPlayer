using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicItemControl : MonoBehaviour {
    
    public Text m_musicName;

    public void ClickItem()
    {
        ComponentsManager objManager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        AnalyseMusic analyseMusic = objManager.DataManager.GetComponent<AnalyseMusic>();
        FileOperation file = objManager.DataManager.GetComponent<FileOperation>();

        AudioSource audio = objManager.m_audio;
        string value;
        file.MusicsResult.TryGetValue(m_musicName.text, out value);
        analyseMusic.LoadMusic(value);

        //play music
        UIComponentsManager obj = GameObject.FindGameObjectWithTag(UIComponentsManager.SELF_TAG).GetComponent<UIComponentsManager>();
        if (obj.m_audioPlay)
        {
            audio.time = 0;
            audio.name = m_musicName.text;
            obj.m_audioPlay.isOn = false;
            obj.m_audioPlay.isOn = true;
        }
    }
}
