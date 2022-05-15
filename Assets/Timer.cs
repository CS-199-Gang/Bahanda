using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float time;
    private bool isRunning = false;
    private UIManager uiManager;
    private GameManager gameManager;

    protected virtual void Start() {
        uiManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
        time = gameManager.GetTime(1);
        isRunning = true;

    }

    protected virtual void Update() {
        if (isRunning) {
            if (time > 0) {
                time -= Time.deltaTime;
                uiManager.SetSeconds(time);
            } else {
                time = 0;
                isRunning = false;
                gameManager.NextScene();
            }
        }
    }
}
