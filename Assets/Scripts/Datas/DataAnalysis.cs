using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataAnalysis : MonoBehaviour
{
    public const string FilePath = "config.json";
    public const string LanguageFolder = ".\\Language";
    public const string ThemeFolder = "Prefabs\\Themes";
    private const string ThemePrefix = "Theme_";

    [SerializeField]
    private Transform m_themeRoot;

    private DataToSave m_data = new DataToSave();
    private LanguageData m_language = new LanguageData();

    private List<string> m_languageNames = new List<string>();
    private List<string> m_themeNames = new List<string>();

    private string m_languageType = "zh";
    private string m_theme = "Default";

    private List<string> m_musicPaths = new List<string>();


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

    public string LanguageType
    {
        get
        {
            return m_languageType;
        }
        set
        {
            SetLanguage(value);
            m_languageType = value;
        }
    }

    public string[] LanguageNames
    {
        get
        {
            return m_languageNames.ToArray();
        }
    }

    public string[] ThemeNames
    {
        get
        {
            return m_themeNames.ToArray();
        }
    }

    public string Theme
    {
        get
        {
            return m_theme;
        }

        set
        {
            m_theme = value;
            SetTheme(ThemePrefix + m_theme);
        }
    }

    public string[] MusicPaths
    {
        get
        {
            return m_musicPaths.ToArray();
        }
    }

    public delegate void LanguageEventProxy();

    public event LanguageEventProxy OnLanguageTypeChange;

    private bool isInit = false;

    public void Init()
    {
        if (isInit)
            return;
        isInit = true;
        ReadData();
        InitLanguage();
        Theme = m_theme;
    }

    public void AddMusicPath(string path)
    {
        m_musicPaths.Add(path);
        m_data.musicPaths = m_musicPaths.ToArray();
    }

    private void InitLanguage()
    {
        string[] languages = FileTools.GetFilesByRecursion(LanguageFolder, "*", 1);
        FileTools.GetFilesNameWithoutExtension(languages);
        m_languageNames.AddRange(languages);

        Object[] objs = Resources.LoadAll(ThemeFolder);

        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name.IndexOf(ThemePrefix) != -1)
            {
                m_themeNames.Add(objs[i].name.Replace(ThemePrefix, ""));
            }
        }

        OnLanguageTypeChange += LanguageChange;
    }

    private void LanguageChange()
    {
        Debug.Log("Language changed to " + m_languageType.ToString());
    }

    public void UpdateSaveData()
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        m_data.playingMusicNamePath = manager.m_audio.CurrentSongPath;
        m_data.musicProgressValue = manager.m_audio.CurrentTime;
        m_data.volume = manager.m_audio.Volume;
        m_data.themeName = Theme;
        m_data.fullScreen = Screen.fullScreen;
    }

    private void ReadData()
    {
        if (!File.Exists(FilePath))
        {
            using (File.Create(FilePath))
            {
                ;
            }

            FileTools.SaveObjectDataToFile(m_data, FilePath);
        }
        else
        {
            FileTools.ReadFileToObject(ref m_data, FilePath);
            m_theme = m_data.themeName;
            if (Data.musicPaths != null)
                m_musicPaths.AddRange(Data.musicPaths);
        }
        ReadLanguageFile();
    }

    private void ReadLanguageFile()
    {
        string languagePath = Path.Combine(LanguageFolder, Data.language);

        if (!File.Exists(languagePath))
        {
            Directory.CreateDirectory(LanguageFolder);

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

    private void SetTheme(string name)
    {
        Theme[] old = m_themeRoot.GetComponentsInChildren<Theme>();

        string path = Path.Combine(ThemeFolder, name);
        path = Path.Combine(path, name);

        GameObject newTheme = (GameObject)FileTools.GetResoures(path);
        Instantiate(newTheme, m_themeRoot);

        for (int i = 0; i < old.Length; i++)
        {
            Destroy(old[i].gameObject);
        }
    }

    public void SaveData()
    {
        FileTools.SaveObjectDataToFile(m_data, FilePath);
    }

}
