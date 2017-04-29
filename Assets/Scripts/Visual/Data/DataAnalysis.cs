using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataAnalysis : MonoBehaviour
{
    private DataToSave m_data = new DataToSave();

    public string m_filePath = "config.json";

    public DataToSave Data
    {
        get
        {
            return m_data;
        }

        set
        {
            m_data = value;
        }
    }

    private void Awake()
    {
        ReadData();
    }

    public void UpdateSaveData()
    {
        UIComponentsManager uiObj = GameObject.FindGameObjectWithTag(UIComponentsManager.SELF_TAG).GetComponent<UIComponentsManager>();
        ComponentsManager obj = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        m_data.m_playingMusicName = obj.m_audio.name;
        m_data.m_musicProgressVolume = obj.m_audio.time;

        m_data.m_volume = uiObj.m_volumeSlider.value;
        m_data.m_fullScreen = uiObj.m_fullScreenToggle.isOn;
    }

    private void ReadData()
    {
        if (!File.Exists(m_filePath))
        {
            using (File.Create(m_filePath)) ;
            FileTools.SaveObjectDataToFile(m_data, m_filePath);
        }
        else
        {
            FileTools.ReadFileToObject(ref m_data, m_filePath);
        }
    }

    private void SaveData()
    {
        FileTools.SaveObjectDataToFile(m_data, m_filePath);
    }
    
    private void OnApplicationQuit()
    {
        UpdateSaveData();
        SaveData();
    }
}
