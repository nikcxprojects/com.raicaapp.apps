using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] Text musicText;

    [Space(10)]
    [SerializeField] AudioSource sounds;
    [SerializeField] Text soundsText;

    [Space(10)]
    [SerializeField] Text vibrationText;
    public static bool VibraEnbled { get; set; }

    private void Start()
    {
        SetMusicStatus(true);
        SetSoundsStatus(true);
        SetVibrationStatus(true);
    }

    public void SetMusicStatus(bool IsEnable)
    {
        music.mute = !IsEnable;
        musicText.text = $"{(IsEnable ? "ON" : "OFF")}";
    }

    public void SetSoundsStatus(bool IsEnable)
    {
        sounds.mute = !IsEnable;
        soundsText.text = $"{(IsEnable ? "ON" : "OFF")}";
    }

    public void SetVibrationStatus(bool IsEnable)
    {
        vibrationText.text = $"{(IsEnable ? "ON" : "OFF")}";
        VibraEnbled = IsEnable;
    }
}
