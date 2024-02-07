using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MobMind : MonoBehaviour {
    public GameObject target;
    
    CharacterController controller;
    private Vector3 velocity = Vector3.zero;



    void Start() {
        // target = new Vector3(0, 0, 0);
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        // velocity += Vector3.down * 9.5f * Time.fixedDeltaTime;

        Vector3 direction = target.transform.position - transform.position;
        // if (direction.magnitude > 1) {
        //     velocity += direction.normalized * 1 * Time.fixedDeltaTime;
        // } else {
        //     velocity -= direction.normalized * 1 * Time.fixedDeltaTime;
        // }

        // var hit = controller.Move(velocity * Time.fixedDeltaTime);
        // if (hit == CollisionFlags.Below) {
        //     velocity *= .99f;
        // }

        controller.SimpleMove(direction.normalized);
    }

    // void OnControllerColliderHit(ControllerColliderHit hit) {
    //     velocity -= hit.normal * (velocity.magnitude * Vector3.Dot(velocity, hit.normal)) * Time.fixedDeltaTime;
    //     // try {
    //     //     hit.gameObject.GetComponent<MobMind>().velocity += hit.normal * velocity.magnitude * Vector3.Dot(velocity, hit.normal) * Time.fixedDeltaTime;
    //     // }
    //     // catch {
    //     //     // Do nothing
    //     // }

    //     if (hit.gameObject.tag == "Goal") {
    //         Destroy(gameObject);
    //     }
    // }
}
