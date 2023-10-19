/*
MIT License
Copyright (c) [2023] [Muhammad Mukarram]
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS," WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

//using System.Collections.Generic;
//using System.Linq;
using UnityEditor;
using UnityEngine;
//using UnityEngine.UIElements;

public class CreateChildGameObjects : EditorWindow
{
    //you can assign any object you want.. doesnt matter what type of object it is.. but you must cast it into the type of object you want to use.
    //public Object source; 

    //here object type already specified
    GameObject Child_gameobject;
    //public List<GameObject> Child_gameobjects;

    bool transform_setting_enabled;
    Vector3 child_position;
    Vector3 child_rotation;
    Vector3 child_scale;

    Space child_position_space;
    Space child_rotation_space;

    [MenuItem("Mukarram Tools/ Create Child GameObjects")]
    public static void CustomEditorWindow()
    {
        //floating window enabled because first bool set to true
        GetWindow<CreateChildGameObjects>(true, "Create Child GameObjects"); //(false, "Custom Unity Editor Window", true);

        //var window = GetWindowWithRect<ExampleClass>(new Rect(0, 0, 165, 100));
        //window.titleContent = new GUIContent("My Custom Editor");
        //window.Show();
    }

    private void OnGUI()
    {
        Color default_background_color = GUI.backgroundColor;

        GUILayout.Label("Create Child of selected GameObjects.", EditorStyles.largeLabel);
        GUILayout.Space(15f);

        //here we need to cast object to GameObject
        Child_gameobject = (GameObject)EditorGUILayout.ObjectField("Child GameObject" , Child_gameobject, typeof(GameObject), true);

        //Child gameObjects List (WIP)
        //ScriptableObject scriptableObj = this;
        //SerializedObject serialObj = new SerializedObject(scriptableObj);
        //SerializedProperty serialProp = serialObj.FindProperty("Child_gameobjects");

        //EditorGUILayout.PropertyField(serialProp, true);
        //serialObj.ApplyModifiedProperties();
        //
        GUILayout.Space(15f);
        transform_setting_enabled = EditorGUILayout.BeginToggleGroup("Optional", transform_setting_enabled);

        GUILayout.Space(15f);
        GUILayout.Label("Child Transform Settings", EditorStyles.boldLabel);
        child_position = EditorGUILayout.Vector3Field("Position", child_position);
        child_rotation = EditorGUILayout.Vector3Field("Rotation", child_rotation);
        child_scale = EditorGUILayout.Vector3Field("Scale", child_scale);

        GUILayout.Space(15f);
        GUILayout.Label("Child Coordinate Space Settings", EditorStyles.boldLabel);
        child_position_space = (Space)EditorGUILayout.EnumPopup("Position Coordinate Space", child_position_space);
        child_rotation_space = (Space)EditorGUILayout.EnumPopup("Rotation Coordinate Space", child_rotation_space);
        EditorGUILayout.EndToggleGroup();

        //button
        GUILayout.Space(15f);
        GUI.backgroundColor = Color.cyan;
        if (GUILayout.Button("Create"))
        {
            Create_Child_GameObjects();
        }
        GUI.backgroundColor = default_background_color;
        //
    }

    private void Create_Child_GameObjects()
    {
        GameObject[] selectedGameObjects = Selection.gameObjects;

        if (selectedGameObjects.Length <= 0)
        {
            Debug.LogWarning("Failed to create Child GameObjects because No Gameobject selected!");
            return;
        }

        if (Child_gameobject == null)
        {
            Debug.LogWarning("Failed to create Child GameObjects because Child Gameobject not assigned.");
            return;
        }

        foreach (GameObject selectedGameObject in selectedGameObjects)
        {
            //foreach (GameObject child in Child_gameobjects)
            //{
            //    GameObject tempObject = Instantiate(child, selectedGameObject.transform, false); //Instantiate(child, Vector3.zero, Quaternion.identity, selectedGameObject.transform);
            //}

            GameObject tempObject = Instantiate(Child_gameobject, selectedGameObject.transform, false); //Instantiate(child, Vector3.zero, Quaternion.identity, selectedGameObject.transform);
            if (transform_setting_enabled)
            {
                if (child_position_space == Space.World)
                {
                    tempObject.transform.position = child_position;
                }
                else
                {
                    tempObject.transform.localPosition = child_position;
                }

                if (child_rotation_space == Space.World)
                {
                    tempObject.transform.rotation = Quaternion.Euler(child_rotation.x, child_rotation.y, child_rotation.z);
                }
                else
                {
                    tempObject.transform.localRotation = Quaternion.Euler(child_rotation.x, child_rotation.y, child_rotation.z);
                }
            }
        }

        Debug.Log(selectedGameObjects.Length + " Child GameObjects Created.");
    }
}