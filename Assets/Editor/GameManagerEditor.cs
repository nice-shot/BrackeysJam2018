using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        GameManager manager = (GameManager)target;

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load")) {
            manager.LoadLevel();
        }

        if (GUILayout.Button("Clear")) {
            manager.ClearLevel();
        }

        if (GUILayout.Button("Save")) {
            manager.SaveLevel();
        }

        GUILayout.EndHorizontal();
    }
}
