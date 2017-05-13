public class LanguageData
{
    public string appName;
    public string close;
    public string fullscreen;
    public string confirm;
    public string cancel;
    public string soundVolume;
    public string play;
    public string pause;
    public string stop;
    public string theme;
    public string import;
    public string setting;
    public string previousSong;
    public string nextSong;
    public string setTheme;
    public string setLanguage;

    public string debug;

    public LanguageData()
    {
        appName = @"音乐播放器";
        close = @"关闭应用";
        fullscreen = @"全屏";
        confirm = @"确认";
        cancel = @"取消";
        soundVolume = @"音量";
        play = @"播放";
        pause = @"暂停";
        stop = @"停止";
        theme = @"主题";
        import = @"导入歌曲";
        setting = @"设置";
        previousSong = @"上一曲";
        nextSong = @"下一曲";
        setTheme = @"设置主题";
        setLanguage = @"设置语言";

        debug = @"调试";
    }

    private void ToUTF8()
    {
        StringTools.ToUTF8(ref appName);
        StringTools.ToUTF8(ref close);
        StringTools.ToUTF8(ref fullscreen);
        StringTools.ToUTF8(ref confirm);
        StringTools.ToUTF8(ref cancel);
        StringTools.ToUTF8(ref soundVolume);
        StringTools.ToUTF8(ref play);
        StringTools.ToUTF8(ref pause);
        StringTools.ToUTF8(ref stop);
        StringTools.ToUTF8(ref theme);
        StringTools.ToUTF8(ref import);
        StringTools.ToUTF8(ref setting);
        StringTools.ToUTF8(ref debug);
    }
}
