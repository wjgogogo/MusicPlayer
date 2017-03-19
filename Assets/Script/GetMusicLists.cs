using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetMusicLists : MonoBehaviour
{
    private AudioSource m_auio;
    public Dropdown dropdown;
    // Use this for initialization
    void Start()
    {
        //m_auio = gameObject.GetComponent<AudioSource>();

        //string[] drives = Directory.GetLogicalDrives();
        //foreach (var item in drives)
        //{
        //    string musicUrl = ReadDirsFiles(item);

        //    if (musicUrl != string.Empty)
        //    {
        //        Debug.Log(musicUrl);

        //        FileInfo file = new FileInfo(musicUrl);
        //        WWW www = new WWW("file:///"+ file.FullName);
        //        while (!www.isDone)
        //        {
        //           // yield return 0;
        //        }

        //        m_auio.clip = NAudioPlayer.FromMp3Data(www.bytes);
        //        m_auio.Play();
        //        Debug.Log(file.Name);
        //    }
        //}
        InitDrivesData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InitDrivesData()
    {
        string[] drives = Directory.GetLogicalDrives();

        dropdown.ClearOptions();
        List<Dropdown.OptionData> list = new List<Dropdown.OptionData>();
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = "All";
            list.Add(data);
        }
        foreach (var item in drives)
        {
            Dropdown.OptionData data = new Dropdown.OptionData();
            data.text = item;
            list.Add(data);
        }

        dropdown.AddOptions(list);
    }

    public void AddMusicButtonClick()
    {

        dropdown.transform.parent.gameObject.SetActive(true);
    }

    public void DoneButtonClick()
    {
        string[] drives = Directory.GetLogicalDrives();
        if (dropdown.value == 0)
        {
            foreach (var item in drives)
            {
                ReadDirsFiles(item);
            }
        }
        else
        {
            ReadDirsFiles(drives[dropdown.value - 1]);
        }
        dropdown.transform.parent.gameObject.SetActive(false);
    }

    /// <summary>
    /// 删除某一类文件夹及子文件,start with 通配符
    /// </summary>
    /// <param name="path">文件所在路径</param>
    protected string ReadDirsFiles(string path)
    {
        string resualt = string.Empty;
        if (path.Contains("C"))
        {
            return resualt;
        }

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        FileInfo[] files = dirInfo.GetFiles("*.mp3");
        foreach (var file in files)
        {
            Debug.Log(file.FullName);
            resualt = file.FullName;
        }
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (var item in dirs)
        {
            if (item.Name.Contains("Recovery") || item.Name.Contains("Sys") || item.Name.Contains("Config") || item.Name.Contains("Cache"))
            {
                continue;
            }
            
            files = item.GetFiles("*.mp3");
            foreach (var file in files)
            {
                Debug.Log(file.FullName);
                resualt = file.FullName;
            }
        }

        return resualt;
    }
}
