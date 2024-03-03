using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour {
    Transform restTransform;

    // Start is called before the first frame update
    void Start() {
        restTransform = transform;
    }

    // Update is called once per frame
    void Update() {
        float spinTime = Time.time * .5f;
        transform.position = restTransform.position + new Vector3(Mathf.Cos(Time.time) * 0.005f, Mathf.Sin(Time.time) * 0.005f, 0);
        transform.rotation = restTransform.rotation * Quaternion.Euler(Mathf.Sin(spinTime), Mathf.Cos(spinTime), Mathf.Sin(spinTime));
    }
}
