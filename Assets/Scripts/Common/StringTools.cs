using System.Text;

public static class StringTools
{
    /// <summary>
    /// convert to utf8
    /// </summary>
    /// <param name="text"></param>
    public static void ToUTF8(ref string text)
    {
        byte[] bytes = Encoding.Default.GetBytes(text);
        text = Encoding.UTF8.GetString(bytes);
    }

}
