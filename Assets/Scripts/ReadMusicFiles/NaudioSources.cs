using System.Collections;
using UnityEngine;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Threading;

public class NaudioSources 
{
    private IWavePlayer waveoutPlayer;
    private AudioFileReader audioFileReader;
    private WaveChannel32 waveoutStream;
    private FadeInOutSampleProvider fadeInOut;

    private string currentSongPath;
    private float totalTime = 0.0f;
    private int  FadeDuration = 5;
    private int fftDataSize = 2048;

    //the total length of the song
    private float m_totalTime;

    public float TotalTime
    {
        get
        {
            return m_totalTime;
        }
    }
    
    public float CurrentTime
    {
        get
        {
            return GetCurrentTime();
        }
        set
        {
            SetCurrentTime(value);
        }
    }

    public string Name
    {
        get
        {
            return Path.GetFileNameWithoutExtension(currentSongPath);
        }
    }

    public PlaybackState PlayStatus
    {
        get
        {
            return waveoutPlayer.PlaybackState;
        }
    }

    /// <summary>
    /// range:0-1
    /// </summary>
    public float Volume
    {
        set
        {
            SetVolume(value);
        }
        get
        {
            return GetVolume();
        }
    }

    /// <summary>
    /// get the samples by channel
    /// </summary>
    /// <param name="samples">sample data</param>
    /// <param name="channel">0 left and 1 right</param>
    public void GetSample(ref float samples, int channel)
    {
    }

    /// <summary>
    ///set the status -- play or stop
    /// </summary>
    /// <param name="status"></param>
    public void Play(bool status)
    {
        if (status)
            PlayMusic();
        else
            PauseMusic();
    }

    private float GetCurrentTime()
    {
        if (waveoutPlayer != null && audioFileReader != null)
        {
            TimeSpan time = (waveoutPlayer.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : audioFileReader.CurrentTime;

            return (float)time.TotalSeconds;
        }
        else
            return 0.0f;
    }

    /// <summary>
    /// load the music from the path
    /// </summary>
    /// <param name="path">music path</param>
    public void LoadMusic(string path)
    {
        if (currentSongPath == path)
            return;
        currentSongPath = path;
        try
        {
            CreateEvent();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    /// <summary>
    /// init event
    /// </summary>
    private void CreateEvent()
    {
        CloseEvent();

        waveoutPlayer = new WaveOutEvent();
        waveoutPlayer.PlaybackStopped += OnPlaybackStopped;
        audioFileReader = new AudioFileReader(currentSongPath);
        m_totalTime = (float)audioFileReader.TotalTime.TotalSeconds;
        waveoutStream = new WaveChannel32(audioFileReader);
        fadeInOut = new FadeInOutSampleProvider(audioFileReader);

        waveoutPlayer.Init(fadeInOut);
    }

    /// <summary>
    /// dispose the waveout and audiofilereader
    /// </summary>
    private void CloseEvent()
    {
        if (waveoutPlayer != null)
        {
            waveoutPlayer.Stop();
        }
        if (audioFileReader != null)
        {
            audioFileReader.Dispose();
            audioFileReader = null;
        }
        if (waveoutPlayer != null)
        {
            waveoutPlayer.Dispose();
            waveoutPlayer = null;
        }

        fadeInOut = null;
    }

    private void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        Debug.Log("stop");
    }

    /// <summary>
    /// play the song
    /// </summary>
    private void PlayMusic()
    {
        if (waveoutPlayer != null)
        {
            if (waveoutPlayer.PlaybackState != PlaybackState.Playing)
            {
                Debug.Log("play");
                waveoutPlayer.Play();
            }
        }
    }

    /// <summary>
    /// pause the song
    /// </summary>
    private void PauseMusic()
    {
        if (waveoutPlayer != null)
        {
            if (waveoutPlayer.PlaybackState == PlaybackState.Playing)
            {
                Action fadeOut = new Action(() =>
                  {
                      fadeInOut.BeginFadeOut(FadeDuration);
                      Thread.Sleep(FadeDuration);
                      fadeInOut.BeginFadeIn(0);
                      waveoutPlayer.Pause();
                  });
                fadeOut.BeginInvoke(null, null);
            }
        }
    }


 
    /// <summary>
    /// stop the song
    /// </summary>
    public void Stop()
    {
        if (waveoutPlayer != null)
            waveoutPlayer.Stop();
        if (audioFileReader != null)
            audioFileReader.Position = 0;
    }

    /// <summary>
    /// change the volume
    /// </summary>
    /// <param name="volume"></param>
    private void SetVolume(float volume)
    {
        audioFileReader.Volume = volume;
    }

    /// <summary>
    /// get the volume
    /// </summary>
    /// <returns></returns>
    private float GetVolume()
    {
        if (audioFileReader != null)
            return audioFileReader.Volume;
        else
            return 0.0f;
    }

    /// <summary>
    /// set the current time after change range:0-1
    /// </summary>
    /// <param name="time"></param>
    private void SetCurrentTime(float time)
    {
        if (waveoutPlayer != null && audioFileReader != null)
        {
            audioFileReader.CurrentTime = TimeSpan.FromSeconds(time);
        }
    }

    /// <summary>
    /// dispose the waveout and audiofilereader when application quit
    /// </summary>
    public void OnApplicationQuit()
    {
        CloseEvent();
    }
}