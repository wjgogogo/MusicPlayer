using System.IO;
using UnityEngine;

public class DataAnalysis : MonoBehaviour
{
    [SerializeField]
    public static string m_filePath = "config.json";
    [SerializeField]
    public static string m_languageFolder = "./Language";

    private DataToSave m_data = new DataToSave();

    private LanguageData m_language = new LanguageData();

    private string m_type = "zh";

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

    public string Type
    {
        get
        {
            return m_type;
        }
        set
        {
            SetLanguage(value);
            m_type = value;
        }
    }
    
    public delegate void LanguageEventProxy();

    public event LanguageEventProxy OnLanguageTypeChange;

    private void Awake()
    {
        ReadData();
    }

    private void Start()
    {
        OnLanguageTypeChange += LanguageChange;
    }

    private void LanguageChange()
    {
        Debug.Log("Language changed to " + m_type.ToString());
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
            using (File.Create(m_filePath))
            {
                ;
            }

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

    private void SetLanguage(string type)
    {
        m_data.language = type;
        ReadLanguageFile();
        OnLanguageTypeChange();
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
