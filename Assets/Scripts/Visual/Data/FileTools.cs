using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

/// <summary>
/// read and save file
/// </summary>
public class FileTools
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
}
