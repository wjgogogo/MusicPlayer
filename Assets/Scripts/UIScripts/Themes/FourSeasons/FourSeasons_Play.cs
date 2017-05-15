using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FourSeasons_Play : PlayModule {
    [SerializeField]
    private Button m_volumeButton;

    private bool m_volumeIsOn = false;

    private void Start()
    {
        m_volumeButton.onClick.AddListener(OpenVolumeSlider);
        OpenVolumeSlider();
    }

    private void OpenVolumeSlider()
    {
        m_volumeSlider.gameObject.SetActive(m_volumeIsOn);
        m_volumeIsOn = !m_volumeIsOn;
    }
    
}
