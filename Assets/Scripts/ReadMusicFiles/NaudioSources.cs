using UnityEngine;
using NAudio.Wave;
using System;
using System.IO;
using NaudioSource;

public class NaudioSources
{
    private WaveOut waveoutPlayer;
    private WaveStream waveStream;
    private WaveChannel32 inputStream;
    private SampleAggregator sampleAggregator;

    private string currentSongPath;
    private float m_totalTime = 0f;

    private FFTSize m_bufferLength = FFTSize.FFT512;

    public FFTSize BufferLength
    {
        set
        {
            m_bufferLength = value;
        }
        get
        {
            return m_bufferLength;
        }
    }

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

    public string CurrentSongPath
    {
        get
        {
            return currentSongPath;
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

    #region Create and Close

    /// <summary>
    /// init event
    /// </summary>
    private void CreateEvent()
    {
        DisposeEvent();

        sampleAggregator = new SampleAggregator((int)m_bufferLength);
        waveoutPlayer = new WaveOut()
        {
            DesiredLatency = 100
        };
        waveoutPlayer.PlaybackStopped += OnPlaybackStopped;
        waveStream = InitFileReader(currentSongPath);
        inputStream = new WaveChannel32(waveStream);
        waveoutPlayer.Init(inputStream);

        m_totalTime = (float)inputStream.TotalTime.TotalSeconds;
        inputStream.Sample += SampleRead;
    }

    private WaveStream InitFileReader(string path)
    {
        if (path.EndsWith(".mp3"))
            return new Mp3FileReader(path);
        else
            return new WaveFileReader(path);
    }

    private void SampleRead(object sender, SampleEventArgs e)
    {
        sampleAggregator.Add(e.Left, e.Right);
    }

    /// <summary>
    /// dispose the waveout and audiofilereader
    /// </summary>
    private void DisposeEvent()
    {
        Stop();
        if (waveStream != null)
        {
            inputStream.Close();
            inputStream = null;
            waveStream.Close();
            waveStream = null;
        }
        if (waveoutPlayer != null)
        {
            waveoutPlayer.Dispose();
            waveoutPlayer = null;
        }
    }

    #endregion Create and Close

    #region Common Operation

    /// <summary>
    /// play the song
    /// </summary>
    private void PlayMusic()
    {
        if (waveoutPlayer != null)
        {
            if (waveoutPlayer.PlaybackState != PlaybackState.Playing)
            {
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
                waveoutPlayer.Pause();
            }
        }
    }

    /// <summary>
    /// stop the song
    /// </summary>
    private void Stop()
    {
        if (waveoutPlayer != null)
            waveoutPlayer.Stop();
        if (inputStream != null)
            inputStream.Position = 0;
    }

    public void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        Debug.Log("the song stopped");
    }

    #endregion Common Operation

    #region Volume

    /// <summary>
    /// change the volume
    /// </summary>
    /// <param name="volume"></param>
    private void SetVolume(float volume)
    {
        if (inputStream != null)
            inputStream.Volume = volume;
    }

    /// <summary>
    /// get the volume
    /// </summary>
    /// <returns></returns>
    private float GetVolume()
    {
        if (inputStream != null)
            return inputStream.Volume;
        else
            return 0.0f;
    }

    #endregion Volume

    #region Time

    /// <summary>
    /// set the current time after change range:0-1
    /// </summary>
    /// <param name="time"></param>
    private void SetCurrentTime(float time)
    {
        if (waveoutPlayer != null && inputStream != null)
        {
            inputStream.CurrentTime = TimeSpan.FromSeconds(time);
        }
    }

    private float GetCurrentTime()
    {
        if (waveoutPlayer != null && inputStream != null)
        {
            TimeSpan time = (waveoutPlayer.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : inputStream.CurrentTime;

            return (float)time.TotalSeconds;
        }
        else
            return 0.0f;
    }

    #endregion Time

    #region Other Public Methods

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
    /// dispose the waveout and audiofilereader when application quit
    /// </summary>
    public void OnApplicationQuit()
    {
        DisposeEvent();
    }

    public void GetSample(float[] sample, int channel, NaudioSource.FFTWindow window)
    {
        if (sampleAggregator != null)
            sampleAggregator.GetFFTResults(sample, channel, window);
    }

    #endregion Other Public Methods
}