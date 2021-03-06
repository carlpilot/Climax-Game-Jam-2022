using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    static string[] adjectives = { "steamy", "spicy", "salty", "delectable", "tinned", "hot", "cold", "fresh", "baked", "boiled", "fried"};
    static string[] typesOfBean = { "baked", "runner", "kidney", "green", "black", "white", "cowboy", "adzuki", "edamame", "lima", "red", "moong", "pinto", "garbanzo", "soy", "navy", "butter" };
    static string[] beanWords = { "bean", "beans", "beans", "beans", "frijoles", "haricots", "beansontoast"};

    public GameObject player1;
    public GameObject player2;

    public Slider R1;
    public Slider G1;
    public Slider B1;
    public Slider R2;
    public Slider G2;
    public Slider B2;

    public Image picker1, picker2;

    public TMP_InputField usernameInput;
    //HealthBar hb;

    public void Start() {
        //hb = FindObjectOfType<HealthBar> ();
        var renderer1 = player1.GetComponentsInChildren<Renderer>()[1];
        var renderer2 = player2.GetComponentsInChildren<Renderer>()[1];
        if (!PlayerPrefs.HasKey ("R1")) {
            R1.value = 0.4f; G1.value = 0.5f; B1.value = 0.9f;
            R2.value = 0.9f; G2.value = 0.6f; B2.value = 0.3f;
        } else {
            R1.value = PlayerPrefs.GetFloat ("R1");
            G1.value = PlayerPrefs.GetFloat ("G1");
            B1.value = PlayerPrefs.GetFloat ("B1");
            R2.value = PlayerPrefs.GetFloat ("R2");
            G2.value = PlayerPrefs.GetFloat ("G2");
            B2.value = PlayerPrefs.GetFloat ("B2");
        }
        if(!PlayerPrefs.HasKey("Username")) {
            NewUsername ();
        } else {
            usernameInput.text = PlayerPrefs.GetString ("Username");
        }
        if (!PlayerPrefs.HasKey ("LastLevelCompleted")) PlayerPrefs.SetInt ("LastLevelCompleted", 0);
    }

    public void Update() {
        var renderer1 = player1.GetComponentsInChildren<Renderer>()[1];
        var renderer2 = player2.GetComponentsInChildren<Renderer>()[1];
        var ring1 = player1.GetComponentsInChildren<Renderer>()[3];
        var ring2 = player2.GetComponentsInChildren<Renderer>()[3];
        Color colour1 = new Color (R1.value, G1.value, B1.value, 255); ;
        Color colour2 = new Color (R2.value, G2.value, B2.value, 255);
        renderer1.material.color = colour1;
        renderer2.material.color = colour2;
        ring1.material.color = colour1 / 2;
        ring2.material.color = colour2 / 2;
        picker1.color = colour1;
        picker2.color = colour2;
        //hb.RH1.color = colour1; hb.RH2.color = colour1; hb.RH3.color = colour1;
        //hb.BH1.color = colour2; hb.BH2.color = colour2; hb.BH3.color = colour2;
    }

    public void playGame() {
        PlayerPrefs.SetFloat("R1", R1.value);
        PlayerPrefs.SetFloat("G1", G1.value);
        PlayerPrefs.SetFloat("B1", B1.value);
        PlayerPrefs.SetFloat("R2", R2.value);
        PlayerPrefs.SetFloat("G2", G2.value);
        PlayerPrefs.SetFloat("B2", B2.value);
        SaveUsername ();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene (SceneManager.sceneCountInBuildSettings - 1); // load last scene (level select)
    }

    public void quitGame() {
        Application.Quit();
    }

    public void NewUsername () => usernameInput.text = GenerateUsername ();

    public static string GenerateUsername() {
        string u = adjectives[Random.Range (0, adjectives.Length)] + typesOfBean[Random.Range (0, typesOfBean.Length)] + beanWords[Random.Range (0, beanWords.Length)] + Random.Range (100, 999);
        return u.Length <= 25 ? u : GenerateUsername ();
    }

    public void SaveUsername () => PlayerPrefs.SetString ("Username", usernameInput.text.ToUpper());

    public void GoToCredits () => SceneManager.LoadScene ("Credits");

}
