
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualInfo : MonoBehaviour
{
    /// <summary>
    /// audio tag "PlayingAudio"
    /// </summary>
    public static string AUDIO_SOURCE = "PlayingAudio";

    public enum SPECTRUM_SIZE : int
    {
        SIZE64 = 64,
        SIZE128 = 128,
        SIZE256 = 256,
        SIZE512 = 512,
        SIZE1024 = 1024,
        SIZE2048 = 2048,
        SIZE4096 = 4096,
        SIZE8192 = 8192
    }

    /// <summary>
    /// get audio source by tag named PlayingAudio
    /// </summary>
    private AudioSource m_audio;

    /// <summary>
    /// initialize size on awake
    /// </summary>
    public SPECTRUM_SIZE m_spectrumSize = SPECTRUM_SIZE.SIZE1024;

    public FFTWindow m_fftWindow = FFTWindow.Triangle;

    private int m_channel = 0;

    public int Channel
    {
        get
        {
            return m_channel;
        }
        set
        {
            m_channel = value < 0 ? 0 : (value >= m_audio.clip.channels ? 0 : value);
        }
    }

    /// <summary>
    /// must use UpdateSamples function before use this member
    /// </summary>
    private float[] m_samples;

    public float[] Samples
    {
        get
        {
            return m_samples;
        }
    }

    /// <summary>
    /// initialization 
    /// </summary>
    private void Awake()
    {
        m_samples = new float[(int)m_spectrumSize];
        m_audio = GameObject.FindGameObjectWithTag(AUDIO_SOURCE).GetComponent<AudioSource>();
    }

    /// <summary>
    /// update samples data
    /// </summary>
    protected void UpdateSamples()
    {
        try
        {
            m_audio.GetSpectrumData(m_samples, m_channel, m_fftWindow);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

}
