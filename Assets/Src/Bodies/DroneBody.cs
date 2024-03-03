using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DroneBody : MonoBehaviour {
    CharacterController controller;
    Camera playerCamera;
    ControlBody controlBody;



    void Start() {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        controlBody = GetComponent<ControlBody>();
    }

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, controlBody.lookInput.x * .5f, 0));
        playerCamera.transform.Rotate(new Vector3(-controlBody.lookInput.y * .75f, 0, 0));

        Transform tar = playerCamera.transform;
        // print(tar.rotation.signed);
        playerCamera.transform.rotation = Quaternion.Euler(
            Mathf.Clamp(tar.rotation.eulerAngles.x, -90, 90),
            tar.rotation.eulerAngles.y,
            tar.rotation.eulerAngles.z);

        controller.Move(playerCamera.transform.rotation * new Vector3(
            controlBody.moveInput.x,
            0,
            controlBody.moveInput.y) * Time.fixedDeltaTime * 5f);
    }
}