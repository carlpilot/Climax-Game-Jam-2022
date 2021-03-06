using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerController pc;
    PlayerHealth he;
    public Transform Player1, Player2;
    public MeshRenderer Player1Mesh, Player2Mesh, Player1Torus, Player2Torus;

    public WinScreen winScreen;
    public GameOverScreen gameOverScreen;
    public RectTransform pauseMenu;

    public KeyCode pauseKey = KeyCode.Escape;

    public float voidCutoff = -10f; // player dies below this level

    Timer timer;
    bool hasEnded = false;

    public static int level { get => SceneManager.GetActiveScene ().buildIndex; }

    private void Start () {
        Time.timeScale = 1; // undo any pause which may have happened before scene load
        pauseMenu.gameObject.SetActive (false);
        gameOverScreen.gameObject.SetActive (false);
        winScreen.gameObject.SetActive (false);
        pc = FindObjectOfType<PlayerController> ();
        he = FindObjectOfType<PlayerHealth> ();
        timer = FindObjectOfType<Timer> ();
        Color colour1 = new Color (PlayerPrefs.GetFloat ("R1"), PlayerPrefs.GetFloat ("G1"), PlayerPrefs.GetFloat ("B1"));
        Color colour2 = new Color (PlayerPrefs.GetFloat ("R2"), PlayerPrefs.GetFloat ("G2"), PlayerPrefs.GetFloat ("B2"));
        Player1Mesh.material.color = colour1;
        Player2Mesh.material.color = colour2;
        Player1Torus.material.color = colour1 / 2;
        Player2Torus.material.color = colour2 / 2;
        if (!PlayerPrefs.HasKey ("Username")) PlayerPrefs.SetString ("Username", MainMenu.GenerateUsername ());
    }

    private void Update () {
        if( he.health1 <= 0 || he.health2 <= 0 ||
            Player1.position.y < voidCutoff || Player2.position.y < voidCutoff) {
            Lose ();
        }
        if (Input.GetKeyDown (pauseKey)) Pause ();
    }

    public void Pause () {
        if (hasEnded) return; // no pausing over end or win screens
        pauseMenu.gameObject.SetActive (true);
        Time.timeScale = 0;
    }
    public void Unpause () {
        Time.timeScale = 1;
        pauseMenu.gameObject.SetActive (false);
    }

    public void Win () {
        if (hasEnded) return;
        EndGame ();
        Invoke ("RunWin", 1);
    }

    void RunWin () {
        winScreen.gameObject.SetActive (true);
        winScreen.GetComponent<WinScreen> ().Trigger ();
        putGetHighScores (timer.time);
        if (!PlayerPrefs.HasKey ("LastLevelCompleted") || PlayerPrefs.GetInt ("LastLevelCompleted") < level)
            PlayerPrefs.SetInt ("LastLevelCompleted", level);
    }

    public void Lose () {
        if (hasEnded) return; // don't lose twice
        gameOverScreen.gameObject.SetActive (true);
        gameOverScreen.GetComponent<GameOverScreen> ().Trigger ();
        EndGame ();
        pc.enableRighting = false;
    }

    public void EndGame () {
        hasEnded = true;
        timer.StopTime ();
        if (!timer.extended) timer.ToggleTimerExtended (false);
        pc.enableMovement = false;
        pc.enableJumping = false;
    }

    public void NextLevel () => SceneManager.LoadScene (level + 1);

    public void ToMainMenu () => SceneManager.LoadScene (SceneManager.sceneCountInBuildSettings - 1); // load last scene (level select)

    public void ReloadLevel () => gameOverScreen.RestartScene ();

    public void getHighScores() {
        StartCoroutine(getHSHelper("http://dreamlo.com/lb/" + SecretCode.Public (level) + "/pipe-seconds-asc"));
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
        StartCoroutine(putHSHelper("http://dreamlo.com/lb/" + SecretCode.Private (level) + "/add/" + PlayerPrefs.GetString("Username") + "/" + score + "/" + timeMS));
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

    public void putGetHighScores (float time) {
        int timeMS = Mathf.FloorToInt (time * 1000);
        int score = 10000000 - timeMS; // lower time = higher score
        StartCoroutine (getHSHelper ("http://dreamlo.com/lb/" + SecretCode.Private(level) + "/add-pipe-seconds-asc/" + PlayerPrefs.GetString ("Username") + "/" + score + "/" + timeMS));
    }

}
