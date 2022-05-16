using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using OVRSimpleJSON;

public class EndScene : MonoBehaviour {

    [SerializeField]
    private GameObject menuOne;
    [SerializeField]
    private GameObject menuTwo;
    [SerializeField]
    private Transform hoverCanvas;
    [SerializeField]
    private Transform gridOne;
    [SerializeField]
    private Transform gridTwo;
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
            int currQuantity = inventory.ContainsKey(t.itemRequired) ? inventory[t.itemRequired] : 0; 

            if (!t.isNeeded && currQuantity <= 0) {
                continue;
            }

            Transform grid = t.isNeeded ? gridOne : gridTwo; 
            GameObject tb = Instantiate(taskBox, grid.position, grid.rotation, grid);
            TaskBoxScript tbs = tb.GetComponent<TaskBoxScript>();
            tbs.SetItems(t, currQuantity);
            tbs.GetHoverBoxTransform().SetParent(hoverCanvas);
        }
    }

    public void ShowMenuOne() {
        menuOne.SetActive(true);
        menuTwo.SetActive(false);
    }

    public void ShowMenuTwo() {
        if (gridTwo.childCount == 0) {
            NextScene();
            return;
        }
        menuOne.SetActive(false);
        menuTwo.SetActive(true);
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

        string name = PlayerPrefs.GetString("name");
        string number = PlayerPrefs.GetString("number");

        // Scenario One
        int battery = inventory.ContainsKey("battery") ? inventory["battery"] : 0;
        int food = inventory.ContainsKey("food") ? inventory["food"] : 0;
        int matchsticks = inventory.ContainsKey("matchsticks") ? inventory["matchsticks"] : 0;
        int firstaid = inventory.ContainsKey("firstaid") ? inventory["firstaid"] : 0;
        int flashlight = inventory.ContainsKey("flashlight") ? inventory["flashlight"] : 0;
        int docs = inventory.ContainsKey("docs") ? inventory["docs"] : 0;
        int medicine = inventory.ContainsKey("medicine") ? inventory["medicine"] : 0;
        int water = inventory.ContainsKey("water") ? inventory["water"] : 0;

        // Scenario Two
        int door = inventory.ContainsKey("door") ? inventory["door"] : 0;
        int window = inventory.ContainsKey("window") ? inventory["window"] : 0;
        int plug = inventory.ContainsKey("plug") ? inventory["plug"] : 0;
        int power = inventory.ContainsKey("power") ? inventory["power"] : 0;

        string tasks = $"{{ \"water\": {water}, \"food\": {food}, \"matchsticks\": {matchsticks}, \"first_aid_kit\": {firstaid}, \"flashlight\": {flashlight}, \"docs\": {docs}, \"battery\": {battery}, \"medicine\": {medicine}, \"door\": {door}, \"plug\": {plug}, \"power\": {power}, \"window\": {window} }}";

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
