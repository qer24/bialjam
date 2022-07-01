using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(DropdownMenu))]
public class DropdownMenuEditor : OdinEditor
{
    private SerializedObject so;
    private SerializedProperty propContent;
    private SerializedProperty propEmpty;
    private SerializedProperty propMenuItems;

    protected override void OnEnable()
    {
        so = new SerializedObject(target);
        
        var dropdownMenu = (DropdownMenu)target;
        propContent = so.FindProperty(nameof(dropdownMenu.content));
        propEmpty = so.FindProperty(nameof(dropdownMenu.emptySpacesIndexes));
        propMenuItems = so.FindProperty(nameof(dropdownMenu.menuItems));
    }

    public override void OnInspectorGUI()
    {
        so.Update();

        EditorGUILayout.PropertyField(propContent);
        EditorGUILayout.PropertyField(propEmpty);
        EditorGUILayout.Space();
        
        var style = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold
        };
        EditorGUILayout.LabelField("Menu Items", style, GUILayout.ExpandWidth(true));
        DrawUILine(Color.grey);
        
        EditorGUILayout.PropertyField(propMenuItems, GUIContent.none, true);

        // for (var i = 0; i < dropdownMenu.menuItems.Length; i++)
        // {
        //     var menuItem = dropdownMenu.menuItems[i];
        //     EditorGUILayout.Space();
        //     
        //     EditorGUILayout.BeginHorizontal();
        //     EditorGUILayout.LabelField($"Item {i}:", GUILayout.MaxWidth(70));
        //     //EditorGUILayout.LabelField("Label", GUILayout.MaxWidth(70));
        //     menuItem.label = EditorGUILayout.TextField(menuItem.label);
        //     EditorGUILayout.EndHorizontal();
        //
        //     // var onPressProp = so.FindProperty($"menuItems[{i}].onPress");
        //     // EditorGUILayout.PropertyField(onPressProp);
        //
        //     EditorGUILayout.ObjectField(menuItem.onPress, typeof(UnityEvent), true);
        // }

        so.ApplyModifiedProperties();
    }
    
    public static void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
        r.height = thickness;
        r.y+=(float)padding/2;
        r.x-=2;
        r.width +=6;
        EditorGUI.DrawRect(r, color);
    }
}