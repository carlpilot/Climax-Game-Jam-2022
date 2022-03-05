using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public Texture2D[] icons;

    public void SwitchScene (int scene) => SceneManager.LoadScene (scene);
}
