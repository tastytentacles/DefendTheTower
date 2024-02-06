using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour {
    public GameObject towerCache;
    public MenuManager menuManager;

    public void CallUpgrade() {
        towerCache.SendMessage("Upgrade");
    }

    public void CallSell() {
        towerCache.SendMessage("Sell");
        menuManager.SendMessage("ClearMenu");
    }
}
