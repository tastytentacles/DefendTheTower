using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour {
    public void Click() {
        Debug.LogWarning("TowerBase.Click");
    }

    public void Upgrade() {
        Debug.LogWarning("TowerBase.Upgrade");
    }

    public void Sell() {
        Destroy(gameObject);
    }
}
