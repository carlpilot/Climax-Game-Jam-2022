using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent (typeof (Slider))]
public class setSoundVolume : MonoBehaviour {
    public enum Mode { Sound, Music };
    public Mode mode;
    public AudioMixer mixer;
    public bool SaveToPrefs;
    public string PlayerPrefName;
    Slider s;

    private void Start () {
        s = GetComponent<Slider> ();
        if (SaveToPrefs && PlayerPrefs.HasKey (PlayerPrefName)) s.value = PlayerPrefs.GetFloat (PlayerPrefName);
    }

    public void SetLevel () {
        mixer.SetFloat (GetName (), Mathf.Log10 (s.value) * 20);
        if (SaveToPrefs) PlayerPrefs.SetFloat (PlayerPrefName, s.value);
    }

    public string GetName () => mode switch
    {
        Mode.Sound => "SoundParam",
        Mode.Music => "MusicParam",
        _ => throw new UnassignedReferenceException ("Sound enum")
    };
}