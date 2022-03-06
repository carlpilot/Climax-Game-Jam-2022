using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingFloor : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float timeRemaining;
    private float timeRemaining2;

    private Renderer renderer;

    void Start()
    {
        timeRemaining = -100;
        timeRemaining2 = -100;
        renderer = GetComponentInChildren<Renderer>();
        Color c = renderer.material.color;
        renderer.material.color = new Color(c.r, c.g, c.b, 50);
    }

    void Update() {
        if (timeRemaining > 0) {
            Color c = renderer.material.color;
            if (timeRemaining > 4) {
                renderer.material.color = new Color(c.r, c.g, c.b, 255);
            } else if (timeRemaining > 3) {
                renderer.material.color = new Color(c.r, c.g, c.b, 200);
            } else if (timeRemaining > 2) {
                renderer.material.color = new Color(c.r, c.g, c.b, 150);
            } else if (timeRemaining > 1) {
                renderer.material.color = new Color(c.r, c.g, c.b, 100);
            } else {
                renderer.material.color = new Color(c.r, c.g, c.b, 50);
            }
            timeRemaining -= Time.deltaTime;
        } else if (timeRemaining != -100) {
            disappear();
        }

        if (timeRemaining2 > 0) {
            timeRemaining2 -= Time.deltaTime;
        } else if (timeRemaining2 != -100) {
            reappear();
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if (timeRemaining == -100 && timeRemaining2 == -100) {
            timeRemaining = 3;
        }
    }

    private void disappear() {
        Color c = renderer.material.color;
        renderer.material.color = new Color(c.r, c.g, c.b, 0);
        gameObject.GetComponent<BoxCollider>().enabled = false;
        timeRemaining = -100;
        timeRemaining2 = 3;
    }

    private void reappear() {
        Color c = renderer.material.color;
        renderer.material.color = new Color(c.r, c.g, c.b, 255);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        timeRemaining = -100;
        timeRemaining2 = -100;
    }

}
