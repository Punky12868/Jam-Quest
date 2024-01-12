using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using System.Reflection;

public class AudioConfig : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] Slider masterVol;
    [SerializeField] Slider musicVol;
    [SerializeField] Slider sfxVol;

    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ConfigMenu");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        LoadConfig();
    }

    private void Awake()
    {
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void LoadConfig()
    {
        masterVol.value = PlayerPrefs.GetFloat("MasterVolume", 0.25f);
        musicVol.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVol.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float volume = masterVol.value;
        
        PlayerPrefs.SetFloat("MasterVolume", volume);
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicVol.value;

        PlayerPrefs.SetFloat("MusicVolume", volume);
        audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGM", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxVol.value;

        PlayerPrefs.SetFloat("SFXVolume", volume);
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && transform.GetChild(0).gameObject.activeInHierarchy)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
