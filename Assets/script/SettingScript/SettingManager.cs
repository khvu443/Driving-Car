using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("----- Quit Setting -----")]
    [SerializeField] GameObject SettingPanel;

    [Header("----- Resolution Setting -----")]
    [SerializeField] TMP_Dropdown resolutionDropdown;

    [Header("----- Quality Setting -----")]
    [SerializeField] TMP_Dropdown qualityDropdown;

    [Header("----- Sound Setting -----")]
    [SerializeField] AudioMixer Mixer;
    [SerializeField] Slider sliderMaster;
    [SerializeField] Slider sliderMusic;
    [SerializeField] Slider sliderSFX;



    Resolution[] res;


    private void Start()
    {
        loadVolume();

        LoadQuality();

        LoadAllResolution();
        
        Screen.fullScreen = true;
    }


    public void SetVolumeMaster(float value)
    {
        Mixer.SetFloat("Master", value);
        PlayerPrefs.SetFloat("MasterPrefs", value);
    }
    public void SetVolumeMusic(float value)
    {
        Mixer.SetFloat("Music", value);
        PlayerPrefs.SetFloat("MusicPrefs", value);
    }

    public void SetVolumeSFX(float value)
    {
        Mixer.SetFloat("SFX", value);
        PlayerPrefs.SetFloat("SFXPrefs", value);
    }

    void loadVolume()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("MasterPrefs");
        sliderMusic.value = PlayerPrefs.GetFloat("MusicPrefs");
        sliderSFX.value = PlayerPrefs.GetFloat("SFXPrefs");

        SetVolumeMaster(sliderMaster.value);
        SetVolumeMusic(sliderMusic.value);
        SetVolumeSFX(sliderSFX.value);
    }

    //-----------------------------------------------

    void LoadQuality()
    {
        int level;
        if (!PlayerPrefs.HasKey("QualityLevel"))
        {
            level = QualitySettings.GetQualityLevel();
        }
        else
        {
            level = PlayerPrefs.GetInt("QualityLevel");
        }
        qualityDropdown.value = level;
        SetQuality(level);
    }
    public void SetQuality(int indexQuality)
    {
        QualitySettings.SetQualityLevel(indexQuality);
        PlayerPrefs.SetInt("QualityLevel", indexQuality);
    }

    //----------------------------------------------
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //---------------------------------------------
    void LoadAllResolution()
    {
        int curr = 0;
        res = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> opts = new List<string>();
        for (int i = 0; i < res.Length; i++)
        {
            string opt = res[i].width + " x " + res[i].height;
            opts.Add(opt);

            if (!PlayerPrefs.HasKey("Width") && !PlayerPrefs.HasKey("Height"))
            {
                if (res[i].width == Screen.currentResolution.width &&
                    res[i].height == Screen.currentResolution.height)
                {
                    curr = i;
                }
            }
            else
            {
                if (res[i].width == PlayerPrefs.GetInt("Width") &&
                    res[i].height == PlayerPrefs.GetInt("Height"))
                {
                    curr = i;
                }
            }

        }
        resolutionDropdown.AddOptions(opts);
        resolutionDropdown.value = curr;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Width", resolution.width);
        PlayerPrefs.SetInt("Height", resolution.height);
    }

    public void ShowSettingPanel()
    {
        SettingPanel.SetActive(!SettingPanel.activeInHierarchy);
    }

}
