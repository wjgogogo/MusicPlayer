using System.IO;
using UnityEngine;

public class DataAnalysis : MonoBehaviour
{
    private DataToSave m_data = new DataToSave();

    private LanguageData m_language = new LanguageData();

    public string m_filePath = "config.json";
    public string m_languageFolder = "./Language";

    public DataToSave Data
    {
        get
        {
            return m_data;
        }
    }

    public LanguageData Language
    {
        get
        {
            return m_language;
        }
    }

    public enum LanguageType
    {
        zh,
        en
    }

    private void Awake()
    {
        ReadData();
    }

    public void UpdateSaveData()
    {
        ComponentsManager obj = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        m_data.playingMusicName = obj.m_audio.name;
        m_data.musicProgressVolume = obj.m_audio.time;

        m_data.volume = obj.m_audio.volume;

        m_data.fullScreen = Screen.fullScreen;
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
        ReadLanguageFile();
    }

    private void ReadLanguageFile()
    {
        string languagePath = Path.Combine(m_languageFolder, Data.language);

        if (!File.Exists(languagePath))
        {
            Directory.CreateDirectory(m_languageFolder);

            using (File.Create(languagePath)) { };
            FileTools.SaveObjectDataToFile(m_language, languagePath);
        }
        else
        {
            FileTools.ReadFileToObject(ref m_language, languagePath);
        }
    }

    public void SetLanguage(LanguageType type)
    {
        switch (type)
        {
            case LanguageType.zh:
                m_data.language = "zh";
                break;
            case LanguageType.en:
                m_data.language = "en";
                break;
            default:
                break;
        }
        ReadLanguageFile();
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
