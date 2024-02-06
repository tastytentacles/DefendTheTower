using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour {
    public GameObject towerCache;

    void Update() {
        if (towerCache != null) {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 12, .1f);

            Quaternion toRotation = Quaternion.LookRotation(towerCache.transform.position - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, toRotation, .1f);
        } else {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, .1f);

            Quaternion toRotation = Quaternion.LookRotation(Vector3.zero - Camera.main.transform.position);
            Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, toRotation, .1f);
        }
    }
}
