using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(DeckManager))]
public class DeckManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DeckManager deckManager = (DeckManager)target;
        if (GUILayout.Button("Draw Next Card"))
        {
            HandManager handManager = FindHandManagerInActiveScene();
            if (handManager != null)
            {
                deckManager.DrawCard(handManager);
            }
            else
            {
                Debug.LogWarning("HandManager not found in active scene.");
            }
        }
    }

    private HandManager FindHandManagerInActiveScene()
    {
        var scene = SceneManager.GetActiveScene();
        if (!scene.isLoaded) return null;

        foreach (var root in scene.GetRootGameObjects())
        {
            // includeInactive: true to match behavior that might have found disabled objects
            var hm = root.GetComponentInChildren<HandManager>(true);
            if (hm != null) return hm;
        }

        return null;
    }
}
#endif
