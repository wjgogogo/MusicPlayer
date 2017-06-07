public class DataToSave
{
    // to save the ui data
    public string themeName = "Theme_default";

    // data of setting
    public bool fullScreen = false;
    public string playingMusicName = "";
    public double musicProgressVolume = 0.0;
    public double volume = 1.0;
    public string language = "zh";
    public string[] musicPaths;

    public override string ToString()
    {
        string str = "";

        str += "theme name : " + themeName + "\n";
        str += "full screen : " + fullScreen + "\n";
        str += "playing music name : " + playingMusicName + "\n";
        str += "music progress volume : " + musicProgressVolume + "\n";
        str += "music volume : " + volume + "\n";
        foreach (var item in musicPaths)
        {
            str += "music path : " + item + "\n";
        }
        return str;
    }
}
