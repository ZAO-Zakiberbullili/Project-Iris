using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class UIWindowHandler : EditorWindow
{
    VisualElement container;

    [MenuItem("Test/Test Window")]

    public static void ShowWindow()
    {
        UIWindowHandler window = GetWindow<UIWindowHandler>();
        window.titleContent = new GUIContent("Test Window");
    }

    public void CreateGUI()
    {
        container = rootVisualElement;
        VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/CharacterCreationUI/CharacterCreationUI.uxml");
        container.Add(visualTreeAsset.Instantiate());
        
        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/CharacterCreationUI/style.uss");
        container.styleSheets.Add(styleSheet);
    }
}
