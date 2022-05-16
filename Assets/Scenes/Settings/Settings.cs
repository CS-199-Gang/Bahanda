using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using OVRSimpleJSON;

public class Settings : MonoBehaviour {
    // Start is called before the first frame update

    string url = "https://bahanda.kimpalao.com/api";
    string id;

    public Text scenario1TimeText;
    public Text scenario2TimeText;

    private int scenario1Time = 600;
    private int scenario2Time = 600;
    private bool isRefreshing = false;

    void Start() {
        scenario1Time = PlayerPrefs.GetInt("scenario1Time");
        scenario2Time = PlayerPrefs.GetInt("scenario2Time");
        FormatTime();
        id = SystemInfo.deviceUniqueIdentifier;
    }

    // Update is called once per frame
    void Update() {
        if (OVRInput.GetDown(OVRInput.Button.One) && !isRefreshing) {
            isRefreshing = true;
            Debug.Log("Refreshing...");
            StartCoroutine(Refresh());
        } else if (OVRInput.GetDown(OVRInput.Button.Two)) {
            SceneManager.LoadScene("Start Screen");
        }
    }

    private void FormatTime() {

        int scenario1Minutes = scenario1Time / 60;
        int scenario1Seconds = scenario1Time % 60;

        int scenario2Minutes = scenario2Time / 60;
        int scenario2Seconds = scenario2Time % 60;

        scenario1TimeText.text = string.Format("{0,2:00}:{1,2:00}", scenario1Minutes, scenario1Seconds);
        scenario2TimeText.text = string.Format("{0,2:00}:{1,2:00}", scenario2Minutes, scenario2Seconds);
    }

    IEnumerator Refresh() {
        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/settings/{id}")) {
            yield return request.SendWebRequest();

            switch (request.responseCode) {
                case 200:
                    JSONNode json = JSON.Parse(request.downloadHandler.text);
                    scenario1Time = json["data"]["scenario_one_time"];
                    scenario2Time = json["data"]["scenario_two_time"];
                    PlayerPrefs.SetInt("scenario1Time", scenario1Time);
                    PlayerPrefs.SetInt("scenario2Time", scenario2Time);
                    PlayerPrefs.Save();
                    FormatTime();
                    break;
                case 404:
                    break;
            }
            isRefreshing = false;
        }

    }
}
