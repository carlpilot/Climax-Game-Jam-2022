using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void SwitchScene (int scene) => SceneManager.LoadScene (scene);
}
