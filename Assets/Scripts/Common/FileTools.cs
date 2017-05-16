using UnityEngine;
using LitJson;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// read and save file
/// </summary>
public static class FileTools
{
    /// <summary>
    /// text file type
    /// </summary>
    public enum FileType
    {
        TXT,
        JSON,
        INI,
        XML
    }

    /// <summary>
    /// only support json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    /// <param name="type">don't modify this</param>
    public static void ReadFileToObject<T>(ref T obj, string path, FileType type = FileType.JSON)
    {
        if (!File.Exists(path))
        {
            Debug.Log("文件不存在");
            return;
        }

        switch (type)
        {
            case FileType.TXT:
                break;
            case FileType.JSON:
                obj = JsonMapper.ToObject<T>(File.ReadAllText(path));
                break;
            case FileType.INI:
                break;
            case FileType.XML:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// only support json
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="path"></param>
    /// <param name="type">don't modify this</param>
    public static void SaveObjectDataToFile<T>(T obj, string path, FileType type = FileType.JSON)
    {
        switch (type)
        {
            case FileType.TXT:
                break;
            case FileType.JSON:
                JsonWriter writer = new JsonWriter()
                {
                    PrettyPrint = true
                };
                JsonMapper.ToJson(obj, writer);

                File.WriteAllText(path, writer.TextWriter.ToString());

                break;
            case FileType.INI:
                break;
            case FileType.XML:
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// get file path by recursion
    /// </summary>
    /// <param name="path"></param>
    /// <param name="searchPattern"></param>
    /// <param name="depth"></param>
    /// <returns></returns>
    public static string[] GetFilesByRecursion(string path, string searchPattern, int depth)
    {
        List<string> dirPath = new List<string>
        {
            path
        };

        List<string> currentPath = new List<string>();
        string[] dirs = Directory.GetDirectories(path);
        currentPath.AddRange(dirs);
        dirPath.AddRange(dirs);

        for (int i = 0; i < depth; i++)
        {
            string[] current = currentPath.ToArray();
            currentPath.Clear();

            for (int j = 0; j < current.Length; j++)
            {
                dirs = Directory.GetDirectories(current[j]);
                currentPath.AddRange(dirs);
            }

            dirPath.AddRange(dirs);
        }

        List<string> filesPath = new List<string>();
        for (int i = 0; i < dirPath.Count; i++)
        {
            filesPath.AddRange(Directory.GetFiles(dirPath[i], searchPattern));
        }

        return filesPath.ToArray();
    }

    /// <summary>
    /// get file name without extension
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileNameWithoutExtension(string path)
    {
        return Path.GetFileNameWithoutExtension(path);
    }

    public static void GetFilesNameWithoutExtension(string[] paths)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = Path.GetFileNameWithoutExtension(paths[i]);
        }
    }

    /// <summary>
    /// Get resources according to path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static Object GetResoures(string path)
    {
        return Resources.Load(path);
    }
}
