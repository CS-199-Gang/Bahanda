using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DigitalRuby.RainMaker;

public class ScenarioTwoScript : MonoBehaviour {
    const float FLOODRISESTART = 1f / 4f;
    const float FLOODRISEEND = 3f / 4f;
    const float RAININTENSITYSTART = 0;
    const float RAININTENSITYEND = 5f / 8f;
    const float MAXRAINVOL = 0;
    const float MINRAINVOL = -20;
    const float MAXLOWPASSFREQ = 22000;
    const float MINLOWPASSFREQ = 1300;
    const int WINDOWS = 15;

    [SerializeField]
    private Transform floodTransform;
    [SerializeField]
    private float floodMaxHeight;
    [SerializeField]
    private float startIntensity;
    [SerializeField]
    private float maxIntensity;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private RainScript rainScript;

    private float maxTime = 420;
    private float currTime = 0;
    private float startHeight;
    private int soundClarity = WINDOWS;

    void Start() {
        maxTime = PlayerPrefs.GetInt("scenario2Time", (int)maxTime);
        startHeight = floodTransform.position.y;
        rainScript.RainIntensity = startIntensity;
    }

    void Update() {
        // Increment time
        currTime += Time.deltaTime;
        float progress = currTime / maxTime;

        // Flood height
        if (progress >= FLOODRISESTART && progress <= FLOODRISEEND) {
            floodTransform.gameObject.SetActive(true);
            float floodProgress = (progress - FLOODRISESTART) / (FLOODRISEEND - FLOODRISESTART);
            floodTransform.position = new Vector3(floodTransform.position.x,
                startHeight + floodMaxHeight * floodProgress,  floodTransform.position.z);
        }

        // Rain intensity                             
        if (progress >= RAININTENSITYSTART && progress <= RAININTENSITYEND) {
            float intensityProgress = (progress - RAININTENSITYSTART) / (RAININTENSITYEND - RAININTENSITYSTART);
            rainScript.RainIntensity = startIntensity + maxIntensity * intensityProgress;
        }
    }

    public void DampenSound(int change) {
        soundClarity += change;
        if (soundClarity > WINDOWS) {
            soundClarity = WINDOWS;
        } else if (soundClarity < 0) {
            soundClarity = 0;
        }

        float level = (float) soundClarity / WINDOWS; 
        float soundLevel = MINRAINVOL + (MAXRAINVOL - MINRAINVOL) * level;
        float lowPassLevel = MINLOWPASSFREQ + (MAXLOWPASSFREQ - MINLOWPASSFREQ) * level;
        audioMixer.SetFloat("RainVolume", soundLevel);
        audioMixer.SetFloat("RainLowPass", lowPassLevel); 
    }
}
