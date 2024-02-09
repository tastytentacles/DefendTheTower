using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class TowerBase : MonoBehaviour {
    private List<GameObject> mobsInRange = new List<GameObject>();
    
    public void Click() {
        Debug.LogWarning("TowerBase.Click");
    }

    public void Upgrade() {
        Debug.LogWarning("TowerBase.Upgrade");
    }

    public void Sell() {
        Destroy(gameObject);
    }

    GameObject ClosestMob() {
        GameObject closest = null;
        float closestDistance = float.MaxValue;
        foreach (var mob in mobsInRange) {
            float distance = Vector3.Distance(mob.transform.position, transform.position);
            if (distance < closestDistance) {
                closest = mob;
                closestDistance = distance;
            }
        }
        return closest;
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log($"OnTriggerEnter: {other.gameObject.name}");
        if (other.gameObject.tag == "Mob") {
            mobsInRange.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        Debug.Log($"OnTriggerExit: {other.gameObject.name}");
        if (other.gameObject.tag == "Mob" && mobsInRange.Contains(other.gameObject)) {
            mobsInRange.Remove(other.gameObject);
        }
    }
}
