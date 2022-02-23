using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public float maxY;
    public float time;
    float d;

    void Start() {
        d = (maxY - transform.position.y) / time;
    }

    void Update() {
        if (transform.position.y < maxY) {
            transform.position += Vector3.up * d * Time.deltaTime;
        }
    }

}
