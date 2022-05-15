using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupTextScript : MonoBehaviour
{
    public float speed = 1f;
    public float timer = 3f;

    void Start() {
        Invoke("Disappear", timer);
    }

    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void Disappear() {
        Destroy(gameObject);
    }
}
