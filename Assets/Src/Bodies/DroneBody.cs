using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DroneBody : MonoBehaviour {
    CharacterController controller;
    Camera playerCamera;
    ControlBody controlBody;


    float xRotation = 0f;



    void Start() {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        controlBody = GetComponent<ControlBody>();
    }

    void FixedUpdate() {
        transform.Rotate(new Vector3(0, controlBody.lookInput.x * .5f, 0));

        xRotation -= controlBody.lookInput.y * .75f;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        controller.Move(playerCamera.transform.rotation * new Vector3(
            controlBody.moveInput.x,
            0,
            controlBody.moveInput.y) * Time.fixedDeltaTime * 5f);
    }
}
