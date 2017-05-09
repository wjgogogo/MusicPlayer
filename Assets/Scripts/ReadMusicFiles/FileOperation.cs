using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows.Forms;
using System;
using System.Linq;
using System.Threading;
using System.Text;

/*作为基础类存在，主要负责文件的查找，迁移和删除功能*/

public class FileOperation : MonoBehaviour
{
    /// <summary>
    /// 音乐文件格式
    /// </summary>
    private string[] MusicType = { "*.mp3", "*.wav", "*.ogg" };

    private string[] PictureType = { "*.jpg", "*.png" };

    /// <summary>
    /// 最大递归次数
    /// </summary>
    private int MaxRecursiveTime = 5;

    /// <summary>
    /// 存储着音乐文件名字和路径,其中key:名字；value:路径
    /// </summary>
    public Dictionary<string, string> MusicsResult = new Dictionary<string, string>();

    /// <summary>
    /// 路径管理
    /// </summary>
    private PathOperation pathOperation;

    private FolderBrowser folderBrowser;

    /// <summary>
    ///资源锁
    /// </summary>
    private object lockobject = new object();

    private void Start()
    {
        pathOperation = gameObject.GetComponent<PathOperation>();
        folderBrowser = gameObject.GetComponent<FolderBrowser>();
        //ShowPaths();
        LoadPathFiles();
    }

    /// <summary>
    ///记载路径中的文件
    /// </summary>
    private void LoadPathFiles()
    {
        for (int i = 0; i < pathOperation.musicPaths.Count; i++)
        {
            SearchFilesByType(pathOperation.musicPaths[i]);
        }
    }

    /// <summary>
    /// 在特定的目录根据文件的类型查找文件
    /// </summary>
    public void SetPathAndSearchFiles()
    {
        folderBrowser.ShowDialog();
    }

    public void CheckPath(string folderpath)
    {
        if (folderpath != null)
        {
            if (pathOperation.AddPath(folderpath))
                SearchFilesByType(folderpath);
        }
    }

    /// <summary>
    /// 根据文件夹路径选择指定文件类型的文件
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="type">文件类型</param>
    private void SearchFilesByType(string path)
    {
        Action action = () =>
          {
              Debug.Log("扫描开始");
              LoadFiles(path, MusicType, 0);
          };
        action.BeginInvoke(OnCallBack, null);
    }

    private void OnCallBack(IAsyncResult ar)
    {
        Debug.Log("扫描完成,结果是：");
        // ShowDirectionary();
    }

    /// <summary>
    /// 寻找音乐文件
    /// </summary>
    /// <param name="path"></param>
    private void LoadFiles(string path, string[] Types, int CurrentTime)
    {
        lock (lockobject)
        {
            int time = CurrentTime + 1;
            if (time > MaxRecursiveTime)
                return;
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (string type in Types)
            {
                FileInfo[] files = directory.GetFiles(type);
                foreach (FileInfo file in files)
                {
                    MusicOperation(file);
                }
            }
            DirectoryInfo[] childs = directory.GetDirectories();
            foreach (DirectoryInfo child in childs)
            {
                if ((child.Attributes & FileAttributes.Hidden) == 0)
                    LoadFiles(child.FullName, Types, time);
            }
        }
    }

    /// <summary>
    /// 对查找到的歌曲进行操作
    /// </summary>
    /// <param name="file"></param>
    private void MusicOperation(FileInfo file)
    {
        if (!MusicsResult.ContainsValue(file.FullName))
        {
            string name = Path.GetFileNameWithoutExtension(file.Name);
            string newname = ReName(name);
            MusicsResult.Add(newname, file.FullName);
        }
    }

    /// <summary>
    /// 根据关键字删除音乐文件
    /// </summary>
    /// <param name="key"></param>
    public void DeleteFileByKey(string key)
    {
        if (MusicsResult.ContainsKey(key))
            MusicsResult.Remove(key);
    }

    /// <summary>
    /// 音乐文件重新命名
    /// </summary>
    /// <param name="name"></param>
    private string ReName(string name)
    {
        string newname = name;
        int i = 1;
        while (MusicsResult.ContainsKey(newname))
        {
            newname = name + "(" + i++ + ")";
        }
        return newname;
    }

    /*
     * 测试用
     */

    private void ShowDirectionary()
    {
        foreach (var tmp in MusicsResult)
        {
            Debug.Log("key: " + tmp.Key + "     value:  " + tmp.Value);
        }
    }

    private void ShowPaths()
    {
        Debug.Log("paths:" + pathOperation.musicPaths.Count);
        foreach (var temp in pathOperation.musicPaths)
        {
            Debug.Log("path: " + temp);
        }
    }
}