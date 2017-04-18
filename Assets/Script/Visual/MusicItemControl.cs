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

        audio.clip = analyseMusic.LoadMusic(file.MusicMatch(m_musicName.text));
        audio.Play();
    }
}
