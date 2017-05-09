using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NAudio.Wave;
using System;

public class AnalyseMusic : MonoBehaviour
{
    private FileOperation fileOperation;
    private static string outFilePath = @"C:\Users\Public\Music\wav_musicfile.wav";
    private AudioSource source;
    private bool flag = false;

    private IWavePlayer iwavePlayer;
    private WaveStream waveStream;
    private WaveChannel32 waveChannel32;

    private void Start()
    {
        fileOperation = gameObject.GetComponent<FileOperation>();
        source = GameObject.FindGameObjectWithTag(ComponentsManager.SELF_TAG).GetComponent<ComponentsManager>().m_audio;
    }

    //public void NewLoadMusic(string path)
    //{
    //    byte[] imageData;
    //    using (FileStream fsRead = new FileStream(path, FileMode.Open))
    //    {
    //        int fsLen = (int)fsRead.Length;
    //        imageData = new byte[fsLen];
    //        fsRead.Read(imageData, 0, imageData.Length);
    //    }
    //    LoadAudioFromData(imageData);
    //}

    private bool LoadAudioFromData(byte[] imageData)
    {
        try
        {
            MemoryStream tmpStr = new MemoryStream(imageData);
            waveStream = new Mp3FileReader(tmpStr);
            waveChannel32 = new WaveChannel32(waveStream);

            iwavePlayer = new WaveOut();
            iwavePlayer.Init(waveChannel32);
            iwavePlayer.Play();

            return true;
        }
        catch (Exception ex)
        {
            Debug.LogWarning("Error: " + ex.Message);
            return false;
        }
    }

    public void LoadMusic(string path)
    {
        if (path.EndsWith(".ogg") || path.EndsWith(".wav"))
        {
            StartCoroutine(PlayMusic(path));
        }
        else
        {
            Action action = new Action(() =>
            {
                CovertMp3File(path);
                flag = true;
            });
            action.BeginInvoke(Callback, null);
        }
    }

    private void Callback(IAsyncResult ar)
    {
        flag = true;
    }

    private IEnumerator PlayMusic(string path)
    {
        WWW www = new WWW("file://" + outFilePath);
        yield return www;
        source.clip = www.GetAudioClip();
        source.Play();
    }

    private void CovertMp3File(string path)
    {
        FileStream steam = File.Open(path, FileMode.Open);
        Mp3FileReader reader = new Mp3FileReader(steam);
        WaveFileWriter.CreateWaveFile(outFilePath, reader);
    }

    private void Update()
    {
        if (flag)
        {
            Debug.Log("mp3 play");
            StartCoroutine(PlayMusic(outFilePath));
            flag = false;
        }
    }
}