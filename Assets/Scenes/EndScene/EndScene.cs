using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using OVRSimpleJSON;

public class EndScene : MonoBehaviour {

    [SerializeField]
    private Transform hoverCanvas;
    [SerializeField]
    private Transform grid;
    [SerializeField]
    private GameObject taskBox;
    private Dictionary<string, int> inventory;
    private List<Task> tasks;
    private GameManager gameManager;

    string url = "https://bahanda.kimpalao.com/api";
    string id;


    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        inventory = gameManager.GetInventory();
        tasks = gameManager.GetTasks();
        id = SystemInfo.deviceUniqueIdentifier;
    }

    private void Start() {
        foreach (Task t in tasks) {
            GameObject tb = Instantiate(taskBox, grid.position, grid.rotation, grid);
            TaskBoxScript tbs = tb.GetComponent<TaskBoxScript>();
            int currQuantity = inventory.ContainsKey(t.itemRequired) ? inventory[t.itemRequired] : 0;
            tbs.SetItems(t, currQuantity);
            tbs.GetHoverBoxTransform().SetParent(hoverCanvas);
        }
    }

    public void NextScene() {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Scenario 2 End") {
            StartCoroutine(UploadScore());
        }
        gameManager.NextScene();
    }

    IEnumerator UploadScore() {

        Debug.Log("Uploading Score");

        string name = "John Smith";
        string number = "2018012345";

        int water = 1;
        int food = 2;
        int matchsticks = 1;
        int first_aid_kit = 1;
        int flashlight = 1;
        string tasks = $"{{ \"water\": {water}, \"food\": {food}, \"matchsticks\": {matchsticks}, \"first_aid_kit\": {first_aid_kit}, \"flashlight\": {flashlight} }}";

        string data = $"{{ \"name\": \"{name}\", \"student_number\": \"{number}\", \"device_id\": \"{id}\", \"tasks\": {tasks} }}";

        using (UnityWebRequest request = new UnityWebRequest($"{url}/score/upload", "POST")) {
            byte[] json = new System.Text.UTF8Encoding().GetBytes(data);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(json);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
        }
    }
}
