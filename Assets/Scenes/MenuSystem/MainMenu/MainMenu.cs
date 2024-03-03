using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour {
    // Start is called before the first frame update
    UIDocument menu;

    Label startButton;
    Label settingsButton;
    Label aboutButton;

    StyleColor defaultColor;

    void Start() {
        menu = GetComponent<UIDocument>();
        
        var root = menu.rootVisualElement;
        startButton = root.Q<Label>("StartGame");
        startButton.RegisterCallback<MouseUpEvent>(ev => OnPlayButtonClicked());
        startButton.RegisterCallback<MouseOverEvent>(ev => OnMouseIn(startButton));
        startButton.RegisterCallback<MouseOutEvent>(ev => OnMouseOut(startButton));

        settingsButton = root.Q<Label>("Settings");
        settingsButton.RegisterCallback<MouseUpEvent>(ev => OnSettingsButtonClicked());
        settingsButton.RegisterCallback<MouseOverEvent>(ev => OnMouseIn(settingsButton));
        settingsButton.RegisterCallback<MouseOutEvent>(ev => OnMouseOut(settingsButton));

        aboutButton = root.Q<Label>("About");
        aboutButton.RegisterCallback<MouseUpEvent>(ev => OnAboutButtonClicked());
        aboutButton.RegisterCallback<MouseOverEvent>(ev => OnMouseIn(aboutButton));
        aboutButton.RegisterCallback<MouseOutEvent>(ev => OnMouseOut(aboutButton));

        defaultColor = startButton.style.color;
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnMouseIn(Label l) {
        Debug.Log("Mouse over");
        l.style.color = new StyleColor(Color.red);
        l.style.unityFontStyleAndWeight = FontStyle.Italic;
    }   

    void OnMouseOut(Label l) {
        Debug.Log("Mouse exit");
        l.style.color = defaultColor;
        l.style.unityFontStyleAndWeight = FontStyle.Normal;
    }

    void OnPlayButtonClicked() {
        Debug.Log("Play button clicked");
    }

    void OnSettingsButtonClicked() {
        Debug.Log("Settings button clicked");
    }

    void OnAboutButtonClicked() {
        Debug.Log("About button clicked");
    }
}
