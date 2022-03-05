using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    string[] adjectives = { "steamy", "spicy", "salty", "delectable", "many", "tinned"};
    string[] typesOfBean = { "baked", "runner", "kidney", "green", "black", "white", "cowboy", "adzuki", "edamame", "lima" };
    string[] beanWords = { "bean", "beans", "frijoles", "haricot"};

    public GameObject player1;
    public GameObject player2;

    public Slider R1;
    public Slider G1;
    public Slider B1;
    public Slider R2;
    public Slider G2;
    public Slider B2;

    public TMP_InputField usernameInput;

    public void Start() {
        var renderer1 = player1.GetComponentsInChildren<Renderer>()[1];
        var renderer2 = player2.GetComponentsInChildren<Renderer>()[1];
        var color1 = renderer1.material.color;
        var color2 = renderer2.material.color;
        R1.value = color1.r;
        G1.value = color1.g;
        B1.value = color1.b;
        R2.value = color2.r;
        G2.value = color2.g;
        B2.value = color2.b;
        if(!PlayerPrefs.HasKey("Username")) {
            NewUsername ();
        } else {
            usernameInput.text = PlayerPrefs.GetString ("Username");
        }
    }

    public void Update() {
        var renderer1 = player1.GetComponentsInChildren<Renderer>()[1];
        var renderer2 = player2.GetComponentsInChildren<Renderer>()[1];
        var ring1 = player1.GetComponentsInChildren<Renderer>()[3];
        var ring2 = player2.GetComponentsInChildren<Renderer>()[3];
        renderer1.material.color = new Color(R1.value, G1.value, B1.value, 255);
        renderer2.material.color = new Color(R2.value, G2.value, B2.value, 255);
        ring1.material.color = new Color(R1.value / 2, G1.value / 2, B1.value / 2, 255);
        ring2.material.color = new Color(R2.value / 2, G2.value / 2, B2.value / 2, 255);
    }

    public void playGame() {
        PlayerPrefs.SetFloat("R1", R1.value);
        PlayerPrefs.SetFloat("G1", G1.value);
        PlayerPrefs.SetFloat("B1", B1.value);
        PlayerPrefs.SetFloat("R2", R2.value);
        PlayerPrefs.SetFloat("G2", G2.value);
        PlayerPrefs.SetFloat("B2", B2.value);
        SaveUsername ();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void NewUsername () => usernameInput.text = GenerateUsername ();

    public string GenerateUsername() {
        string u = adjectives[Random.Range (0, adjectives.Length)] + typesOfBean[Random.Range (0, typesOfBean.Length)] + beanWords[Random.Range (0, beanWords.Length)] + Random.Range (100, 999);
        return u.Length <= 25 ? u : GenerateUsername ();
    }

    public void SaveUsername () => PlayerPrefs.SetString ("Username", usernameInput.text);

}
