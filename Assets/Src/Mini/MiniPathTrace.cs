using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MiniPathTrace : MonoBehaviour {
    public MiniPointMind mind;

    // Start is called before the first frame update
    void Start() {
        mind = GetComponent<MiniPointMind>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnDrawGizmos() {
        if (mind != null) {
            foreach (var link in mind.links) {
                if (link != null) {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(transform.position, transform.position + (link.transform.position - transform.position) * .5f);
                }
            }
        }
    }
}
