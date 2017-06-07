using UnityEngine;

public class FolderBrowserDialog
{
    public static string browserName = "BrowserManager";

    public event FolderBrowserManager.GetPath getPathEvent;

    public FolderBrowserDialog(FolderBrowserManager.GetPath method)
    {
        getPathEvent += method;
    }

    public FolderBrowserDialog()
    {
        getPathEvent =null;
    }

    public void ShowDialog()
    {
        FolderBrowserManager manager = GameObject.Find(browserName).GetComponent<FolderBrowserManager>();
        manager.getPath += getPathEvent;
        manager.showDialog();
    }
}