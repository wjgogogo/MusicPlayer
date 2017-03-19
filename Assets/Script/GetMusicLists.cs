using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetMusicLists : MonoBehaviour
{
    private AudioSource m_auio; 
    // Use this for initialization
    void Start()
    {
        m_auio = gameObject.GetComponent<AudioSource>();

        string[] drives = Directory.GetLogicalDrives();
        foreach (var item in drives)
        {
            string musicUrl = ReadDirsFiles(item);

            if (musicUrl != string.Empty)
            {
                Debug.Log(musicUrl);

                FileInfo file = new FileInfo(musicUrl);
                WWW www = new WWW("file:///"+ file.FullName);
                while (!www.isDone)
                {
                   // yield return 0;
                }
                
                m_auio.clip = NAudioPlayer.FromMp3Data(www.bytes);
                m_auio.Play();
                Debug.Log(file.Name);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

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
            return resualt;
        }
        DirectoryInfo[] dirs = dirInfo.GetDirectories();
        foreach (var item in dirs)
        {
            if (item.Name.Contains("Recovery") || item.Name.Contains("Sys") || item.Name.Contains("Config") || item.Name.Contains("Cache"))
            {
                continue;
            }

            string dirpath = Path.Combine(path, item.Name);
            // Debug.Log(dirpath);

            files = item.GetFiles("*.mp3");
            foreach (var file in files)
            {
                Debug.Log(file.FullName);
                resualt = file.FullName;
                return resualt;
            }
        }

        return resualt;
    }
}
