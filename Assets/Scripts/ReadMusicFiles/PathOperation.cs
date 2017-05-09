using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathOperation : MonoBehaviour
{
    private const string pathKey = "paths";

    /// <summary>
    /// 所有保存的路径
    /// </summary>
    public List<string> musicPaths = new List<string>();

    private FileOperation fileOperation;

    private void Awake()
    {
        fileOperation = gameObject.GetComponent<FileOperation>();
        LoadPaths();
    }

    /// <summary>
    /// 读取所保存的路径
    /// </summary>
    private void LoadPaths()
    {
        string allPath = PlayerPrefs.GetString(pathKey, "");
        if (allPath != "")
        {
            string[] pathGroup = allPath.Split(';');
            foreach (var path in pathGroup)
            {
                musicPaths.Add(path);
            }
            musicPaths.RemoveAt(musicPaths.Count - 1);
        }
    }

    /// <summary>
    /// 保存路径
    /// </summary>
    private void SavePaths()
    {
        if (musicPaths.Count > 0)
        {
            string allpath = "";
            for (int i = 0; i < musicPaths.Count; i++)
            {
                allpath += musicPaths[i] + ";";
            }
            PlayerPrefs.SetString(pathKey, allpath);
        }
        else
            PlayerPrefs.SetString(pathKey, "");
    }

    /// <summary>
    /// 添加新的路径
    /// </summary>
    /// <param name="newpath"></param>
    public bool AddPath(string newpath)
    {
        if (musicPaths.Contains(newpath))
            return false;
        else
        {
            musicPaths.Add(newpath);
            SavePaths();
            return true;
        }
    }

    /// <summary>
    /// 删除一条路径
    /// </summary>
    /// <param name="oldpath"></param>
    public void DeletePath(string oldpath)
    {
        if (!musicPaths.Contains(oldpath))
            return;
        else
        {
            musicPaths.Remove(oldpath);
            SavePaths();
        }
    }

    /// <summary>
    /// 清空所有路径
    /// </summary>
    public void DeleteAllPath()
    {
        musicPaths.Clear();
        SavePaths();
    }
}