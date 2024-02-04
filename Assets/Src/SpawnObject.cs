using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObject : MonoBehaviour {
    int spawnIndex = 0;
    public List<GameObject> prefabs = new List<GameObject>();


    public InputAction cycleAction;
    


    void OnEnable() {
        cycleAction.Enable();

        cycleAction.performed += cycleActionHandler;

        SendMessage("UpdateDebugCount", (spawnIndex, prefabs[spawnIndex]));
    }

    void OnDisable() {
        cycleAction.Disable();

        cycleAction.performed -= cycleActionHandler;
    }



    void cycleActionHandler(InputAction.CallbackContext context) {
        spawnIndex = (spawnIndex + 1) % prefabs.Count;

        SendMessage("UpdateDebugCount", (spawnIndex, prefabs[spawnIndex]));
    }

    public void SpawnObjectAt(Vector3 position) {
        if (prefabs.Count == 0)
            return;
        
        Instantiate(prefabs[spawnIndex], position, Quaternion.identity);
    }
}
