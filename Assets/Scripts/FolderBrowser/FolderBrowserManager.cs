using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FolderBrowserManager : MonoBehaviour
{
    #region public component

    public GameObject folderBrowser;
    public GameObject folder;
    public Transform foldersContent;
    public Sprite driverSprite, folderSprite;
    public Text currentSelectedFolder;
    public Button commitButton, cancelButton;

    public delegate void GetPath(string path);

    public event GetPath getPath;

    #endregion public component

    #region private component

    private enum Type
    {
        Folder,
        Driver
    }

    private GameObject browserRoot;
    private FoldersDataManager dataManager;
    public GameObject currentSelect = null;
    private string musicPath;
    private object obj;

    #endregion private component

    private void Start()
    {
        closeDialog();
        dataManager = new FoldersDataManager();
        commitButton.onClick.AddListener(commitEvent);
        cancelButton.onClick.AddListener(closeDialog);
    }

    public void showDialog()
    {
        folderBrowser.SetActive(true);
        resetFoldersContent();
        DriverScan();
    }

    private void closeDialog()
    {
        folderBrowser.SetActive(false);
    }

    private void commitEvent()
    {
        if (currentSelect != null)
            getPath(currentSelect.GetComponent<FolderManager>().folderPath);
        getPath = null;
        closeDialog();
    }

    private void resetFoldersContent()
    {
        int length = foldersContent.childCount;
        for (int i = length - 1; i >= 0; i--)
        {
            Destroy(foldersContent.GetChild(i).gameObject);
        }
    }

    private void DriverScan()
    {
        List<FoldersDataManager.FolderInfo> drivers = dataManager.ScanDriver();
        foreach (var driver in drivers)
            InstantiateChild(foldersContent, driver.path, Type.Driver, driver.haschild);
    }

    private void ScanPath(Transform parent, string path)
    {
        List<FoldersDataManager.FolderInfo> children = dataManager.ScanPath(path);
        foreach (var child in children)
        {
            InstantiateChild(parent, child.path, Type.Folder, child.haschild);
        }
    }

    private void InstantiateChild(Transform parent, string path, Type type, bool haschild)
    {
        GameObject child = Instantiate(folder, parent);
        child.name = (type == Type.Folder ? Path.GetFileNameWithoutExtension(path) : path);

        child.GetComponent<Transform>().GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().overrideSprite = (type == Type.Driver ? driverSprite : folderSprite);
        child.GetComponent<Transform>().GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>().text = child.name;
        child.GetComponent<FolderManager>().folderPath = path;
        if (!haschild)
            child.GetComponent<FolderManager>().hiddenArrow();
    }

    public void updateSelect(GameObject folder, bool state)
    {
        if (currentSelect == folder)
            return;

        resetPreSelect(folder);
        updateCurrentFolderName(folder.name);
        if (state)
        {
            ScanPath(folder.GetComponent<Transform>().GetChild(1), folder.GetComponent<FolderManager>().folderPath);
        }
    }

    private void updateCurrentFolderName(string name)
    {
        currentSelectedFolder.text = name;
    }

    private void resetPreSelect(GameObject folder)
    {
        if (currentSelect != null)
            currentSelect.GetComponent<FolderManager>().changeState(false);
        currentSelect = folder;
    }
}