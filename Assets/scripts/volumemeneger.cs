using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumemeneger : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicslider;
    [SerializeField] private Slider musicSFXslider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusisVolume();
            SetSFXVolume();
        }
    }
    public void SetMusisVolume()
    {
        float volume = musicslider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        float volume = musicSFXslider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        float volume1 = musicSFXslider.value;
        myMixer.SetFloat("effect", Mathf.Log10(volume1) * 20);
        float volume2 = musicSFXslider.value;
        myMixer.SetFloat("laser", Mathf.Log10(volume2) * 59);
        float volume3 = musicSFXslider.value;
        myMixer.SetFloat("shield", Mathf.Log10(volume2) * 35);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat("effectVolume", volume1);
        PlayerPrefs.SetFloat("laserVolume", volume2);
        PlayerPrefs.SetFloat("shieldVolume", volume3);
        PlayerPrefs.Save(); 

    }
 


    private void LoadVolume()
    {
        musicslider.value = PlayerPrefs.GetFloat("musicVolume");
        musicSFXslider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSFXslider.value = PlayerPrefs.GetFloat("effectVolume");
        musicSFXslider.value = PlayerPrefs.GetFloat("laserVolume");
        musicSFXslider.value = PlayerPrefs.GetFloat("shieldVolume");

        SetMusisVolume();
        SetSFXVolume();
        

    }
}
