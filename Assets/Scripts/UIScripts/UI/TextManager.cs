using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public Text appName;

    public Text close;
    public Text fullscreen;
    public Text soundVolume;
    public Text play;
    public Text pause;
    public Text stop;
    public Text theme;
    public Text import;
    public Text setting;
    public Text previousSong;
    public Text nextSong;

    public Text debug;
    
    private void Start()
    {
        LoadLanguage();
    }

    private void LoadLanguage()
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();

        if (appName) appName.text = manager.m_data.Language.appName;
        
        if (play) play.text = manager.m_data.Language.play;
        if (pause) pause.text = manager.m_data.Language.pause;
        if (stop) stop.text = manager.m_data.Language.stop;
        if (nextSong) nextSong.text = manager.m_data.Language.nextSong;
        if (previousSong) previousSong.text = manager.m_data.Language.previousSong;


        if (close) close.text = manager.m_data.Language.close;
        if (soundVolume) soundVolume.text = manager.m_data.Language.soundVolume;

        if (import) import.text = manager.m_data.Language.import;
        if (setting) setting.text = manager.m_data.Language.setting;
        if (fullscreen) fullscreen.text = manager.m_data.Language.fullscreen;

        if (theme) theme.text = manager.m_data.Language.theme;

        if (debug) debug.text = manager.m_data.Language.debug;
    }

    public void ChangeLanguage(DataAnalysis.LanguageType type)
    {
        ComponentsManager manager = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>();
        manager.m_data.SetLanguage(type);
        LoadLanguage();
    }



}
