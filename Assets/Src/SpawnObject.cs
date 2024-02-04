using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnObject : MonoBehaviour {
    public int spawnIndex = 0;
    public InputAction cycleAction;

    public List<GameObject> prefabs = new List<GameObject>();
    


    void OnEnable() {
        cycleAction.Enable();

        cycleAction.performed += cycleActionHandler;

        SendMessage("UpdateDebugCount", spawnIndex);
    }

    void OnDisable() {
        cycleAction.Disable();

        cycleAction.performed -= cycleActionHandler;
    }



    void cycleActionHandler(InputAction.CallbackContext context) {
        spawnIndex = (spawnIndex + 1) % prefabs.Count;

        SendMessage("UpdateDebugCount", spawnIndex);
    }

    public void SpawnObjectAt(Vector3 position) {
        if (prefabs.Count == 0)
            return;
        
        Instantiate(prefabs[spawnIndex], position, Quaternion.identity);
    }
}
