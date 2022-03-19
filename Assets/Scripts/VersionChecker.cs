using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VersionChecker : MonoBehaviour {
    public static string path = "https://raw.githubusercontent.com/carlpilot/Climax-Game-Jam-2022/main/version.txt";

    public static string CurrentVersion;

    public GameObject newVersionNotification;
    public TMP_Text newVersionMessage;
    public TMP_Text downloadPrompt;
    public TMP_Text versionDisplay;
    public TMP_Text staticVersionDisplay;

    private void Awake () {
        CurrentVersion = Application.version;
        staticVersionDisplay.text = "Version " + CurrentVersion;
    }

    private void Start () {
        print ("Version checker active for version " + CurrentVersion);
        StartCoroutine (WwwRequestVersion ());
    }

    IEnumerator WwwRequestVersion () {
        WWW www = new WWW (path);

        yield return www;// new WaitUntil (() => www.isDone);

        string[] lines = www.text.Split (new char[] { '\n' }, 3);

        if (lines[0] != CurrentVersion) {
            Debug.Log ("Update needed: version available: (" + lines[0] + ") vs current version: (" + CurrentVersion + ")");
            newVersionNotification.SetActive (true);
            newVersionMessage.text = lines[1];
            versionDisplay.text = string.Format ("Current Version: {0}\tNew Version: {1}", CurrentVersion, lines[0]);
            downloadPrompt.text = lines[2];
        } else {
            Debug.Log ("Up to date");
        }
    }

    public void OpenDownloadPage() {
        Application.OpenURL ("https://carlpilot.itch.io/");
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit (); 
        #endif
    }
}
