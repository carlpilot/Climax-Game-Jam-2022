using UnityEngine;
using UnityEngine.SceneManagement;

public class rollCredits : MonoBehaviour
{

    public float speed = 2.0f;
    public float cutoff = 4200;


    void Update () {
        
        transform.Translate(0, speed, 0);
        
        if(transform.position.y >= cutoff){
            SceneManager.LoadScene (0);
        }
    
    }

    public void BackToMenu () => SceneManager.LoadScene (0);
}
