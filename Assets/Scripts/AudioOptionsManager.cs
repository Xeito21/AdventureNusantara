using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioOptionsManager : MonoBehaviour
{
    [Header("Volume Slider")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectsSlider;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectsSliderText;

    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }
    private const string MusicVolumeKey = "MusicVolume";
    private const string SoundEffectsVolumeKey = "SoundEffectsVolume";

    private void Start()
    {
        LoadVolumeSettings();
    }

    public void OnMusicSliderValueChange(float value)
    {
        musicVolume = value;
        musicSliderText.text = Mathf.RoundToInt(value * 100).ToString();
        AudioManager.Instance.UpdateMixerVolume();
        SaveVolumeSettings();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        soundEffectsVolume = value;
        soundEffectsSliderText.text = Mathf.RoundToInt(value * 100).ToString();
        AudioManager.Instance.UpdateMixerVolume();
        SaveVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            musicVolume = PlayerPrefs.GetFloat(MusicVolumeKey);
            musicSlider.value = musicVolume;
            musicSliderText.text = Mathf.RoundToInt(musicVolume * 100).ToString();
        }
        else
        {
            musicVolume = 1f;
            musicSlider.value = musicVolume;
            musicSliderText.text = "100";
        }

        if (PlayerPrefs.HasKey(SoundEffectsVolumeKey))
        {
            soundEffectsVolume = PlayerPrefs.GetFloat(SoundEffectsVolumeKey);
            soundEffectsSlider.value = soundEffectsVolume;
            soundEffectsSliderText.text = Mathf.RoundToInt(soundEffectsVolume * 100).ToString();
        }
        else
        {
            soundEffectsVolume = 1f;
            soundEffectsSlider.value = soundEffectsVolume;
            soundEffectsSliderText.text = "100";
        }
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat(MusicVolumeKey, musicVolume);
        PlayerPrefs.SetFloat(SoundEffectsVolumeKey, soundEffectsVolume);
        PlayerPrefs.Save();
    }
}
