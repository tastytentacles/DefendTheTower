using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour {
    // public GameObject debugUI;
    public GameObject upgradeMenu;    
    public GameObject SpawnUi;



    void OnEnable() {
        // UIDocument ui = SpawnUi.GetComponent<UIDocument>();
        // ui.rootVisualElement.Q<VisualElement>("Bar").Add(
        //     new Button(() => Debug.Log("Trace!")) { text = "Spawn" });

        UIDocument ui = SpawnUi.GetComponent<UIDocument>();
        VisualElement bar = ui.rootVisualElement.Q<VisualElement>("Bar");
        bar.RegisterCallback<MouseEnterEvent>(e => MouseEnter());
        bar.RegisterCallback<MouseLeaveEvent>(e => MouseLeave());

        // ui = upgradeMenu.GetComponent<UIDocument>();
        // bar = ui.rootVisualElement.Q<VisualElement>("Bar");
        // bar.RegisterCallback<MouseEnterEvent>(e => MouseEnter());
        // bar.RegisterCallback<MouseLeaveEvent>(e => MouseLeave());
    }

    public void MouseEnter() {
        SendMessage("DisableClicks");
    }

    public void MouseLeave() {
        SendMessage("EnableClicks");
    }

    public void UpdateSpawnUI(SpawnObject data) {
        UIDocument ui = SpawnUi.GetComponent<UIDocument>();
        VisualElement bar = ui.rootVisualElement.Q<VisualElement>("Bar");
        bar.Clear();

        for (int i = 0; i < data.prefabs.Count; i++) {
            int index = i;
            bar.Add(
                new Button(() => data.spawnIndex = index)
                { text = data.prefabs[i].name });
        }
    }

    public void ShowUpgradeMenu(GameObject tower) {
        // SendMessage("DisableClicks");
        upgradeMenu.SetActive(true);

        UIDocument ui = upgradeMenu.GetComponent<UIDocument>();
        VisualElement bar = ui.rootVisualElement.Q<VisualElement>("Bar");
        bar.RegisterCallback<MouseEnterEvent>(e => MouseEnter());
        bar.RegisterCallback<MouseLeaveEvent>(e => MouseLeave());

        ui.rootVisualElement.Q<Label>("Name").text = tower.name;
        ui.rootVisualElement.Q<Button>("Upgrade").clicked += () => tower.SendMessage("Upgrade", tower);
        ui.rootVisualElement.Q<Button>("Sell").clicked += () => {
            tower.SendMessage("Sell", tower);
            ClearMenu();
        };
        ui.rootVisualElement.Q<Button>("Close").clicked += ClearMenu;

        CameraFocus focus = Camera.main.GetComponent<CameraFocus>();
        focus.target = tower;
    }

    // public void UpdateDebugCount((int count, GameObject tower) data) {
    //     UIDocument ui = debugUI.GetComponent<UIDocument>();

    //     ui.rootVisualElement.Q<Label>("TowerIndex").text = $"Tower: {data.count} - {data.tower.name}";
    // }

    public void ClearMenu() {
        try {
            UIDocument ui = upgradeMenu.GetComponent<UIDocument>();
            VisualElement bar = ui.rootVisualElement.Q<VisualElement>("Bar");
            bar.UnregisterCallback<MouseEnterEvent>(e => MouseEnter());
            bar.UnregisterCallback<MouseLeaveEvent>(e => MouseLeave());
        } catch (Exception e) {
            Debug.LogWarning($"Attempted to unregister invalid callback: {e}");
        }
        upgradeMenu.SetActive(false);

        CameraFocus focus = Camera.main.GetComponent<CameraFocus>();
        focus.target = null;

        SendMessage("EnableClicks");
    }
}
