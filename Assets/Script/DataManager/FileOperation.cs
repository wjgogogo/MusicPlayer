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
public class FileOperation : MonoBehaviour {
    /// <summary>
    /// 文件类型枚举
    /// </summary>
    public enum FileType
    {
        Music,
        Picture,
        lyric,
        Video
    };

    /// <summary>
    /// 音乐文件格式
    /// </summary>
    private string[] MusicType = {"*.mp3","*.wav","*.ogg"};
    private string[] PictureType = { "*.jpg", "*.png"};

    /// <summary>
    /// 最大递归次数
    /// </summary>
    private int MaxRecursiveTime=5;

    /// <summary>
    /// 返回音乐文件查找结果
    /// </summary>
    public List<string> MusicResult = new List<string>();
    /// <summary>
    /// 音乐文件完整路径备份
    /// </summary>
    private List<string> MusicsFullName = new List<string>();

    /// <summary>
    /// 在特定的目录根据文件的类型查找文件
    /// </summary>
    /// <param name="description">文件夹串口的描述</param>
    /// <param name="type">文件类型</param>
    public void SetPathAndSearchFiles(string description, FileType type)
    {
        string folderpath = OpenFoldBrower(description);
        if (folderpath !=null)
            SearchFilesByType(folderpath, type);
    }
    /// <summary>
    /// 打开文件夹选择窗口
    /// </summary>
    private string OpenFoldBrower(string description)
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        dialog.ShowNewFolderButton = true;
        dialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
        dialog.Description = description;
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.SelectedPath;
        }
        else
            return null;    
    }

    /// <summary>
    /// 根据文件夹路径选择指定文件类型的文件
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <param name="type">文件类型</param>
    private void  SearchFilesByType(string path,FileType type)
    {
        Action action = () =>
          {
              switch (type)
              {
                  case FileType.Music:
                      LoadFiles(path,MusicType,0);
                      break;
                  case FileType.Picture:
                      LoadFiles(path, PictureType,0);
                      break;
              }
          };
        action.BeginInvoke(OnCallBack, null);
    }

    void OnCallBack(IAsyncResult ar)
    {
        Thread.Sleep(100);
        ClearResult();
    }


    /// <summary>
    /// 寻找音乐文件
    /// </summary>
    /// <param name="path"></param>
    public void LoadFiles(string path,string[] Types,int CurrentTime)
    {
        int time= CurrentTime + 1;
        if (time > MaxRecursiveTime)
            return;
        DirectoryInfo directory = new DirectoryInfo(path);
        foreach (string type in Types)
        {
            FileInfo[] files = directory.GetFiles(type);
            foreach(FileInfo file in files)
            {
                MusicResult.Add(file.Name);
                MusicsFullName.Add(file.FullName);
            }
        }
        DirectoryInfo[] childs = directory.GetDirectories();
        foreach(DirectoryInfo child in childs)
        {
            LoadFiles(child.FullName,Types,time);
        }
    }

    /// <summary>
    /// 清空结果
    /// </summary>
     public void ClearResult()
    {
        MusicResult.Clear();
    }


    /// <summary>
    /// 根据文件名删除文件
    /// </summary>
    /// <param name="name"></param>
    public void DeleteFileByName(string name)
    {
        string fullname = MusicMatch(name);
        if(fullname!=null)
        {
            File.Delete(fullname);
        }
    }


    /// <summary>
    /// 根据歌曲名匹配完整的路径
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string MusicMatch(string name)
    {
        for (int i = 0; i < MusicsFullName.Count; i++)
        {
            string[] file = MusicsFullName[i].Split('\\');
            if (file[file.Length - 1].Equals(name))
            {
                return MusicsFullName[i];
            }
        }
        return null;
    }
}
