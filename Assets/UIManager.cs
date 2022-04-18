using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]    
    Text timerText;
    [SerializeField]
    GameObject timer;
    [SerializeField]
    Transform start;
    [SerializeField]
    Transform end;
    [SerializeField]
    GameObject getText;
    private bool willShowText = true;

    private void Start() {
        if (SceneManager.GetActiveScene().name == "Scenario 2") {}
        willShowText = false;
    }


    public void SetSeconds(float time) {
        int mins = (int) time / 60; 
        int secs = (int) (time % 60);

        string extraZero = secs < 10 ? "0" : "";
        string text = mins + ":" + extraZero + secs;
        timerText.text = text; 
    }

    public void DisplayItem(string item, bool add) {
        if (willShowText) {
            Vector3 d = end.position - start.position;
            Vector3 pos = d * Random.Range(0f, 1f) + start.position;
            Text text = Instantiate(getText, pos, transform.rotation, transform).GetComponent<Text>();
            if (add) {
                text.text = "+ " + item;
                text.color = Color.green;
            } else {
                text.text = "- " + item;
                text.color = Color.red; 
            }
        }
    }
}
