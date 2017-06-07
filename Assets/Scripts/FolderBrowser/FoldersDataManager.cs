using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FoldersDataManager
{
    public struct FolderInfo
    {
        public string path;
        public bool haschild;

        public FolderInfo(string path, bool haschild)
        {
            this.path = path;
            this.haschild = haschild;
        }
    }

    public List<FolderInfo> ScanDriver()
    {
        List<FolderInfo> driversInfo = new List<FolderInfo>();
        string[] allDrivers = Directory.GetLogicalDrives();
        foreach (var driver in allDrivers)
        {
            driversInfo.Add(new FolderInfo(driver, hasChild(driver)));
        }
        return driversInfo;
    }

    public List<FolderInfo> ScanPath(string path)
    {
        List<FolderInfo> childPaths = new List<FolderInfo>();
        DirectoryInfo directory = new DirectoryInfo(path);
        DirectoryInfo[] children = directory.GetDirectories();
        foreach (var child in children)
        {
            //判断文件夹是否是隐藏的
            if ((child.Attributes & FileAttributes.Hidden) == 0)
            {
                childPaths.Add(new FolderInfo(child.FullName, hasChild(child.FullName)));
            }
        }
        return childPaths;
    }

    public bool hasChild(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        DirectoryInfo[] children = directory.GetDirectories();
        return children.Length == 0 ? false : true;
    }
}