using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using NAudio.Wave;
using System;

public class AnalyseMusic : MonoBehaviour {


    private FileOperation fileOperation;
    
    void Start()
    {
        fileOperation = gameObject.GetComponent<FileOperation>();
    }
    public AudioClip LoadMusic(string path)
    {
        WWW www = new WWW("file://" + path);
        while (!www.isDone) { };
        if (path.EndsWith(".ogg") || path.EndsWith(".wav"))
        {
            return www.GetAudioClip();
        }
        else
        {
            AudioClip clip = FromMp3Data(www.bytes);
            return clip;
        }
    }

    private AudioClip FromMp3Data(byte[] data)
    {
      
        MemoryStream mp3stream = new MemoryStream(data);
     
        Mp3FileReader mp3audio = new Mp3FileReader(mp3stream);
        WaveStream waveStream = WaveFormatConversionStream.CreatePcmStream(mp3audio);
      
        WAV wav = new WAV(AudioMemStream(waveStream).ToArray());

        AudioClip audioClip = AudioClip.Create("music", wav.SampleCount, 1, wav.Frequency, false);
        audioClip.SetData(wav.LeftChannel, 0);
    
        return audioClip;
    }

    private  MemoryStream AudioMemStream(WaveStream waveStream)
    {
        MemoryStream outputStream = new MemoryStream();
        using (WaveFileWriter waveFileWriter = new WaveFileWriter(outputStream, waveStream.WaveFormat))
        {
            byte[] bytes = new byte[waveStream.Length];
            waveStream.Position = 0;
            waveStream.Read(bytes, 0, Convert.ToInt32(waveStream.Length));
            waveFileWriter.Write(bytes, 0, bytes.Length);
            waveFileWriter.Flush();
        }
        return outputStream;
    }

}


public class WAV
{
    static float bytesToFloat(byte firstByte, byte secondByte)
    {
        short s = (short)((secondByte << 8) | firstByte);
        return s / 32768.0F;
    }

    static int bytesToInt(byte[] bytes, int offset = 0)
    {
        int value = 0;
        for (int i = 0; i < 4; i++)
        {
            value |= ((int)bytes[offset + i]) << (i * 8);
        }
        return value;
    }
    public float[] LeftChannel { get; internal set; }
    public float[] RightChannel { get; internal set; }
    public int ChannelCount { get; internal set; }
    public int SampleCount { get; internal set; }
    public int Frequency { get; internal set; }

    public WAV(byte[] wav)
    {

     
        ChannelCount = wav[22];    

   
        Frequency = bytesToInt(wav, 24);

     
        int pos = 12;  

     
        while (!(wav[pos] == 100 && wav[pos + 1] == 97 && wav[pos + 2] == 116 && wav[pos + 3] == 97))
        {
            pos += 4;
            int chunkSize = wav[pos] + wav[pos + 1] * 256 + wav[pos + 2] * 65536 + wav[pos + 3] * 16777216;
            pos += 4 + chunkSize;
        }
        pos += 8;

      
        SampleCount = (wav.Length - pos) / 2;     
        if (ChannelCount == 2) SampleCount /= 2;       

      
        LeftChannel = new float[SampleCount];
        if (ChannelCount == 2) RightChannel = new float[SampleCount];
        else RightChannel = null;

     
        int i = 0;
        while (pos < wav.Length)
        {
            LeftChannel[i] = bytesToFloat(wav[pos], wav[pos + 1]);
            pos += 2;
            if (ChannelCount == 2)
            {
                RightChannel[i] = bytesToFloat(wav[pos], wav[pos + 1]);
                pos += 2;
            }
            i++;
        }
    }

    public override string ToString()
    {
        return string.Format("[WAV: LeftChannel={0}, RightChannel={1}, ChannelCount={2}, SampleCount={3}, Frequency={4}]", LeftChannel, RightChannel, ChannelCount, SampleCount, Frequency);
    }
}

