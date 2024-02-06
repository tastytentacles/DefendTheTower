using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {
    public float speed = .25f;
    
    [HideInInspector]
    public GameObject target;



    void Update() {
        if (target != null) {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 20, speed);

            Quaternion toRotation = Quaternion.LookRotation(target.transform.position - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, toRotation, speed);
        } else {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, speed);

            Quaternion toRotation = Quaternion.LookRotation(Vector3.zero - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, toRotation, speed);
        }
    }
}
