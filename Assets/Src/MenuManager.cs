using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour {
    public GameObject debugUI;
    public GameObject upgradeMenu;    



    public void ShowUpgradeMenu(GameObject tower) {
        SendMessage("DisableClicks");
        upgradeMenu.SetActive(true);

        UpgradeMenuManager data = upgradeMenu.GetComponent<UpgradeMenuManager>();
        data.towerCache = tower;

        UIDocument ui = upgradeMenu.GetComponent<UIDocument>();
        ui.rootVisualElement.Q<Label>("Name").text = tower.name;
        ui.rootVisualElement.Q<Button>("Upgrade").clicked += data.CallUpgrade;
        ui.rootVisualElement.Q<Button>("Sell").clicked += data.CallSell;
        ui.rootVisualElement.Q<Button>("Close").clicked += ClearMenu;

        CameraFocus focus = Camera.main.GetComponent<CameraFocus>();
        focus.towerCache = tower;
    }

    public void UpdateDebugCount((int count, GameObject tower) data) {
        UIDocument ui = debugUI.GetComponent<UIDocument>();

        ui.rootVisualElement.Q<Label>("TowerIndex").text = $"Tower: {data.count} - {data.tower.name}";
    }

    public void ClearMenu() {
        upgradeMenu.SetActive(false);

        UpgradeMenuManager data = upgradeMenu.GetComponent<UpgradeMenuManager>();
        data.towerCache = null;

        CameraFocus focus = Camera.main.GetComponent<CameraFocus>();
        focus.towerCache = null;

        SendMessage("EnableClicks");
    }
}
