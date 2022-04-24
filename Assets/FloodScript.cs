using UnityEngine;
using UnityEngine.Audio;

public class FloodScript : MonoBehaviour
{
    [SerializeField]
    private AudioMixer rainMixer;

    private AudioSource underwaterAudio;
    private ScenarioTwoScript s2;

    private void Awake() {
        underwaterAudio = GetComponent<AudioSource>();
        s2 = FindObjectOfType<ScenarioTwoScript>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            underwaterAudio.mute = false;
            s2.AddVol(-10f);
            s2.AddMult(1f/3f);
            s2.DampenSound();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("FloodDetector")) {
            underwaterAudio.mute = true;
            s2.AddVol(10f);
            s2.AddMult(3f);
            s2.DampenSound();
        }
    }
}
