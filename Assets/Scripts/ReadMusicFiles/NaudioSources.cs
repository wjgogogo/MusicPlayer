public class NaudioSources
{
    //the total length of the song
    private float m_totalTime;
    //the current time
    private float m_currentTime;

    //song's name
    private string m_name;
    //play or stop
    private bool m_playStatus;

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
            return m_currentTime;
        }
    }

    public string Name
    {
        get
        {
            return m_name;
        }
    }

    public bool PlayStatus
    {
        get
        {
            return m_playStatus;
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

    }

    /// <summary>
    /// set sound volume
    /// </summary>
    /// <param name="value"></param>
    public void SetVolume(float value)
    {

    }
}
