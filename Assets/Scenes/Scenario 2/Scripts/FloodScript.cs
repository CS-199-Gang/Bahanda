using UnityEngine;
using UnityEngine.Audio;

public class FloodScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixer rainMixer;

    private AudioSource underwaterAudio;
    private ScenarioTwoScript s2;
    private InventoryManager inventoryManager;
    private bool inWater = false;
    private float timeInWater = 0;

    private void Awake() {
        underwaterAudio = GetComponent<AudioSource>();
        s2 = FindObjectOfType<ScenarioTwoScript>();
        inventoryManager = FindObjectOfType<InventoryManager>();
    }

    private void Update() {
        if (inWater) {
            timeInWater += Time.deltaTime;
            if (timeInWater > 1) {
                timeInWater -= 1;
                inventoryManager.AddItem("inWater", 1, "Swam in Water");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            inWater = true;
            underwaterAudio.mute = false;
            s2.AddVol(-10f);
            s2.AddMult(1f/3f);
            s2.DampenSound();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            inWater = false;
            underwaterAudio.mute = true;
            s2.AddVol(10f);
            s2.AddMult(3f);
            s2.DampenSound();
        }
    }
}
