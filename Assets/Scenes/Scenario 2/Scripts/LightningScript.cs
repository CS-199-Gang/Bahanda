using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class LightningScript : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> audioClips;
    [SerializeField]
    private float avgDuration;
    [SerializeField]
    private Light lightning;
    private AudioSource audioSource;
    private float minDuration;
    private float maxDuration;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        lightning = GetComponent<Light>();
    }

    private void Start() {
        minDuration = avgDuration * 0.6f;
        maxDuration = avgDuration * 1.4f;

        Invoke("DoLightning", Random.Range(minDuration, maxDuration));
    }

    private void DoLightning() {
        audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
        audioSource.PlayDelayed(Random.Range(0f, 2.5f));
        lightning.enabled = true;
        Invoke("OffLightning", Random.Range(0.2f, 0.4f));
        Invoke("DoLightning", Random.Range(minDuration, maxDuration));
    } 

    private void OffLightning() {
        lightning.enabled = false;
    }

}
