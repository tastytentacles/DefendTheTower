using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBody : ControlBody {
    [SerializeField] private InputAction playerMove;
    [SerializeField] private InputAction playerLook;

    

    void Start() {
        playerMove.Enable();
        playerLook.Enable();

        playerMove.performed += MoveUpdate;
        playerLook.performed += LookUpdate;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        moveInput = playerMove.ReadValue<Vector2>();
        lookInput = playerLook.ReadValue<Vector2>();
    }

    void MoveUpdate(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
    }

    void LookUpdate(InputAction.CallbackContext context) {
        lookInput = context.ReadValue<Vector2>();
    }
}
