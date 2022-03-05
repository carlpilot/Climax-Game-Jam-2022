using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerHealth health;
    public Transform Player1, Player2;
    public MeshRenderer Player1Mesh, Player2Mesh, Player1Torus, Player2Torus;

    public WinScreen winScreen;
    public GameObject gameOverScreen;

    public float voidCutoff = -10f; // player dies below this level

    bool hasEnded = false;

    public static int level { get => SceneManager.GetActiveScene ().buildIndex; }

    private void Start () {
        Color colour1 = new Color (PlayerPrefs.GetFloat ("R1"), PlayerPrefs.GetFloat ("G1"), PlayerPrefs.GetFloat ("B1"));
        Color colour2 = new Color (PlayerPrefs.GetFloat ("R2"), PlayerPrefs.GetFloat ("G2"), PlayerPrefs.GetFloat ("B2"));
        Player1Mesh.material.color = colour1;
        Player2Mesh.material.color = colour2;
        Player1Torus.material.color = colour1 / 2;
        Player2Torus.material.color = colour2 / 2;
    }

    private void Update () {
        if( health.health1 <= 0 || health.health2 <= 0 ||
            Player1.position.y < voidCutoff || Player2.position.y < voidCutoff) {
            Lose ();
        }
    }

    public void Win () {
        if (hasEnded) return;
        hasEnded = true;
        winScreen.gameObject.SetActive (true);
        winScreen.GetComponent<WinScreen> ().Trigger ();
        getHighScores ();
    }

    public void Lose () {
        if (hasEnded) return; // don't lose twice
        print ("Died");
        hasEnded = true;
        gameOverScreen.SetActive (true);
        gameOverScreen.GetComponent<GameOverScreen> ().Trigger ();
        Timer t = FindObjectOfType<Timer> ();
        t.StopTime ();
        if(!t.extended) t.ToggleTimerExtended (false);
    }

    public void NextLevel () => SceneManager.LoadScene (level + 1);

    public void MainMenu () => SceneManager.LoadScene (0);

    public void ReloadLevel () => GameOverScreen.RestartScene ();

    public void getHighScores() {
        StartCoroutine(getHSHelper("http://dreamlo.com/lb/622358b4778d3c8cfc1502d1/pipe-seconds-asc"));
    }

    public IEnumerator getHSHelper(string uri) {
        //var result = "";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    var highscores = webRequest.downloadHandler.text;
                    winScreen.LoadLeaderboard (highscores);
                    break;
            }    
        }
    }

    public void putHighScore(float time) {
        int timeMS = Mathf.FloorToInt (time * 1000);
        int score = 10000000 - timeMS; // lower time = higher score
        StartCoroutine(putHSHelper("http://dreamlo.com/lb/tP95lQz0CkyNk7cR_YPPuAkN7wCkOxIkCu7WjI8E345g/add/"+ PlayerPrefs.GetString("Username") + "/" + score + "/" + timeMS));
    }

    public IEnumerator putHSHelper(string uri) {
        //var result = "";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    break;
            }    
        }
    }

}
