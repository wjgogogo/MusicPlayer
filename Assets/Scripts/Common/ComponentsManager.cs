using UnityEngine;

public class ComponentsManager : MonoBehaviour
{
    public const string SELF_TAG = "ComponentsManager";

    // data analysis
    public DataAnalysis m_data;

    // ui camera
    public Camera m_UICamera;

    // aduio sources
    public NaudioSources m_audio = new NaudioSources();

    private void OnApplicationQuit()
    {
        m_data.UpdateSaveData();
        m_data.SaveData();
        m_audio.OnApplicationQuit();
    }
}
