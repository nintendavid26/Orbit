using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class LevelScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Number Of Enemies", Enemy.NumberOfEnemies.ToString());
    }
}
