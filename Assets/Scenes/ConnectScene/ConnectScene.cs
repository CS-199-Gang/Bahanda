using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ConnectScene : MonoBehaviour {
    // Start is called before the first frame update

    string url = "https://bahanda.kimpalao.com/api";
    string id;
    void Start() {
        id = SystemInfo.deviceUniqueIdentifier;
        StartCoroutine(Connect());
    }

    IEnumerator Connect() {

        // Check if registered

        Debug.Log("Connecting");

        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/device/{id}")) {
            yield return request.SendWebRequest();

            switch (request.responseCode) {
                case 200:
                    Debug.Log("Found!");
                    break;
                case 404:
                    Debug.Log("Not found. Making Request now.");
                    StartCoroutine(Request());
                    break;
            }
        }
    }

    IEnumerator Request() {

        using (UnityWebRequest request = UnityWebRequest.Post($"{url}/device/register/{id}", new WWWForm())) {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success) {
                Debug.Log("Registration request sent. Waiting for confirmation");
                Debug.Log(request.downloadHandler.text);
                yield return StartCoroutine(WaitForRegistration());
            } else {
                Debug.Log("An error was encountered");
                Debug.Log(request.error);
            }

        }
    }

    IEnumerator WaitForRegistration() {
        using (UnityWebRequest request = UnityWebRequest.Get($"{url}/device/{id}")) {
            yield return request.SendWebRequest();

            switch (request.responseCode) {
                case 200:
                    Debug.Log("Done!");
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
