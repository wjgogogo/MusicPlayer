using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NAudio;
using NAudio.Wave;

public class MusicAnalysis : MonoBehaviour
{
    private IWavePlayer wavePlayer;
    private AudioFileReader audioFileReader;

    public void AnalysisMusic(string filepath)
    {
        if (wavePlayer != null)
        {
            wavePlayer.Dispose();
            Debug.Log("waveout dispose");
        }
        wavePlayer = new WaveOut();
        audioFileReader = new AudioFileReader(filepath);
        wavePlayer.Init(audioFileReader);
        wavePlayer.Play();
        Debug.Log("the file: " + filepath + " is playing now!");
    }
}