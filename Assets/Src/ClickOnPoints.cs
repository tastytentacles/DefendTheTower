using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickOnPoints : MonoBehaviour {
    public GameObject cursor;
    public InputAction clickAction;
    public InputAction mousePosition;

    private RaycastHit hit;
    private Transform hitObject;
    private Vector3 hitPoint;



    void OnEnable() {
        clickAction.Enable();
        mousePosition.Enable();

        clickAction.performed += clickActionHandler;
        mousePosition.performed += mousePositionHandler;
    }

    void OnDisable() {
        clickAction.Disable();
        mousePosition.Disable();

        clickAction.performed -= clickActionHandler;
        mousePosition.performed -= mousePositionHandler;
    }



    void clickActionHandler(InputAction.CallbackContext context) {
        if (hitObject == null)
            return;

        SendMessage("ClearMenu");
        switch (hitObject.tag) {
            case "Ground":
                SendMessage("SpawnObjectAt", hitPoint);
                break;
            
            case "Tower":
                // hitObject.parent.SendMessage("Click");
                SendMessage("ShowUpgradeMenu", hitObject.parent.gameObject);
                break;
            
            default:
                break;
        } 
    }

    void mousePositionHandler(InputAction.CallbackContext context) {
        Ray ray = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
        
        if (Physics.Raycast(ray, out hit)) {
            hitObject = hit.transform;
            hitPoint = hit.point;

            cursor.transform.position = hitPoint;
        } else {
            hitObject = null;
            hitPoint = Vector3.zero;

            cursor.transform.position = new Vector3(0, -100, 0);
        }
    }

    public void EnableClicks() {
        enabled = true;
    }

    public void DisableClicks() {
        enabled = false;
        cursor.transform.position = new Vector3(0, -100, 0);
    }
}
