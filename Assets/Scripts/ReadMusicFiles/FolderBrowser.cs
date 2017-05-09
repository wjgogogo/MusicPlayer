using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class FolderBrowser : MonoBehaviour
{
    public GameObject folderBrowserDialog;
    public Transform Content;
    public GameObject Folder;
    private string driverSpriteName = @"FolderUI/Driver";
    private string folderSpriteName = @"FolderUI/Folder";
    private GameObject currentSelect = null;
    private FileOperation fileOperation;

    private enum FolderType
    {
        Folder,
        Driver
    }

    private FolderType Type;

    private void Start()
    {
        CloseDialog();
        fileOperation = GetComponent<FileOperation>();
    }

    public void ShowDialog()
    {
        currentSelect = null;
        folderBrowserDialog.SetActive(true);
        DriverScan();
    }

    public void DriverScan()
    {
        string[] allDrivers = Directory.GetLogicalDrives();
        foreach (var driver in allDrivers)
            InstantiateFolder(Content, "", driver, FolderType.Driver, true);
    }

    private void InstantiateFolder(Transform parent, string parentpath, string name, FolderType type, bool show)
    {
        GameObject obj = Instantiate(Folder, parent);
        obj.name = name;
        obj.GetComponent<Transform>().GetChild(0).GetChild(1).GetComponent<Image>().overrideSprite = (type == FolderType.Driver ? Resources.Load<Sprite>(driverSpriteName) : Resources.Load<Sprite>(folderSpriteName));
        obj.GetComponent<Transform>().GetChild(0).GetChild(2).GetComponent<Text>().text = name;
        obj.GetComponent<FolderOpetation>().SetFolderPath((parentpath == "" ? "" : parentpath + (parentpath.EndsWith("\\") ? "" : "\\")) + name);
        if (!show)
            obj.GetComponent<FolderOpetation>().HiddenArrow();
    }

    private void ClearContent()
    {
        int length = Content.childCount;
        for (int i = length - 1; i >= 0; i--)
        {
            Destroy(Content.GetChild(i).gameObject);
        }
    }

    public void UpdateSelect(GameObject obj, Transform parent, bool update)
    {
        if (currentSelect == obj)
            return;

        ResetPreSelect(obj);
        if (update)
            ScanPath(obj, parent);
    }

    private void ScanPath(GameObject obj, Transform parent)
    {
        string path = obj.GetComponent<FolderOpetation>().GetFolderPath();
        DirectoryInfo directory = new DirectoryInfo(path);
        DirectoryInfo[] children = directory.GetDirectories();
        foreach (var child in children)
        {
            //判断文件夹是否是隐藏的
            if ((child.Attributes & FileAttributes.Hidden) == 0)
            {
                DirectoryInfo[] nextChildren = child.GetDirectories();
                bool show = nextChildren.Length == 0 ? false : true;
                InstantiateFolder(parent, path, child.Name, FolderType.Folder, show);
            }
        }
    }

    private void ResetPreSelect(GameObject obj)
    {
        if (currentSelect != null)
            currentSelect.GetComponent<FolderOpetation>().ChangeCheckState(false);
        currentSelect = obj;
    }

    public void CommitButtonClick()
    {
        PostPath();
        CloseDialog();
    }

    public void CancelButtonClick()
    {
        CloseDialog();
    }

    public void CloseDialog()
    {
        ClearContent();
        folderBrowserDialog.SetActive(false);
    }

    public void PostPath()
    {
        if (currentSelect != null)
        {
            fileOperation.CheckPath(currentSelect.GetComponent<FolderOpetation>().folderPath);
        }
    }
}