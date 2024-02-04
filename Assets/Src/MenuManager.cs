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

        UIDocument ui = upgradeMenu.GetComponent<UIDocument>();
        ui.rootVisualElement.Q<Label>("Name").text = tower.name;
        ui.rootVisualElement.Q<Button>("Upgrade").clicked += () => tower.SendMessage("Upgrade");
        ui.rootVisualElement.Q<Button>("Sell").clicked += () => {
            tower.SendMessage("Sell");
            ClearMenu();
        };
        ui.rootVisualElement.Q<Button>("Close").clicked += ClearMenu;
    }

    public void UpdateDebugCount((int count, GameObject tower) data) {
        UIDocument ui = debugUI.GetComponent<UIDocument>();

        ui.rootVisualElement.Q<Label>("TowerIndex").text = $"Tower: {data.count} - {data.tower.name}";
    }

    public void ClearMenu() {
        upgradeMenu.SetActive(false);
        SendMessage("EnableClicks");
    }
}
