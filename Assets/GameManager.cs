using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text endText;
    List<string> backpack = new List<string>(); 

    public void AddItem(string item) {
        backpack.Add(item);
    }

    public void ShowEndText()
    {
        int food = 0;
        int water = 0;
        bool firstAid = false;
        bool flashlight = false;
        bool matchsticks = false;

        foreach(string s in backpack) {
            switch (s) {
                case "food":
                    food++;
                    break;
                case "water":
                    water++;
                    break;
                case "first aid":
                    firstAid = true;
                    break;
                case "flashlight":
                    flashlight = true;
                    break;
                case "matchsticks":
                    matchsticks = true;
                    break;
            }
        }

        string waterTxt = water == 5 ? "You've collected enough water.\n" : "You will need more water than that.\n";
        string foodTxt = food == 5 ? "You've collected enough food.\n" : "More food would be advisable.\n";
        string firstAidTxt = firstAid ? "The first aid you got will help in emergencies.\n" : "Some first aid would be very helpful.\n";
        string flashlightTxt = flashlight ? "You have a flashlight for when power goes out.\n" : "A flashlight would help in case the power goes out.\n";
        string matchsticksTxt = matchsticks ? "The matchsticks you got will help if you need to start a fire.\n" : "Some matchsticks would help in case you need to start a fire.\n";
        

        endText.text = waterTxt + foodTxt + firstAidTxt + flashlightTxt + matchsticksTxt;


        endText.gameObject.SetActive(true);

    }
}
