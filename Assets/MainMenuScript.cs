using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject startMenu;

    public void StartButton() {
        startMenu.SetActive(true);
    }
}
