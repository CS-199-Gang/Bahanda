using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ConnectScene : MonoBehaviour {
    // Start is called before the first frame update

    string url = "https://bahanda.kimpalao.com/api";
    string id;

    public Text messageText;

    void Start() {
        id = SystemInfo.deviceUniqueIdentifier;
        StartCoroutine(Connect());
    }

    IEnumerator Connect() {

        // Check if registered

        messageText.text = "Connecting...";

        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/device/{id}")) {
            yield return request.SendWebRequest();

            switch (request.responseCode) {
                case 200:
                    messageText.text = "Device connected.\nStarting game.";
                    break;
                case 404:
                    messageText.text = "Device not registered.";
                    StartCoroutine(Request());
                    break;
            }
        }
    }

    IEnumerator Request() {

        using (UnityWebRequest request = UnityWebRequest.Post($"{url}/device/register/{id}", new WWWForm())) {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                messageText.text += $"\nUse code {request.downloadHandler.text}.";
                yield return StartCoroutine(WaitForRegistration());
            } else {
                messageText.text = $"An error was encountered: {request.error}";
            }

        }
    }

    IEnumerator WaitForRegistration() {
        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/device/{id}")) {
            yield return request.SendWebRequest();

            switch (request.responseCode) {
                case 200:
                    Regex rx = new Regex("\"name\":\"([^\"]+)\"");
                    MatchCollection matches = rx.Matches(request.downloadHandler.text);
                    GroupCollection groups = matches[0].Groups;
                    string school = groups[1].Value;
                    messageText.text = $"Welcome, {school}";
                    yield return 0;
                    break;
                case 404:
                    Debug.Log("Still waiting.");
                    yield return new WaitForSeconds(5);
                    yield return StartCoroutine(WaitForRegistration());
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
