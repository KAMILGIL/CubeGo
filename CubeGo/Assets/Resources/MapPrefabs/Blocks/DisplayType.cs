using UnityEngine;
using UnityEditor;
using System;
using System.Runtime.InteropServices;

[CustomEditor(typeof(BlockController)), CanEditMultipleObjects]
[InitializeOnLoad]
class LabelHandle : Editor
{
    private BlockController blockController;
    private SerializedProperty blockType;

    void OnEnable()
    {
        blockType = serializedObject.FindProperty ("blockType");
        SceneView.duringSceneGui += CustomOnSceneGUI;
        blockController = (BlockController)target;
    }
     
    void CustomOnSceneGUI(SceneView sceneview)
    {
        blockController = (BlockController)target;
        if (blockController == null)
        {
            return;
        }

        Handles.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(blockController.transform.position, BlockTypeExtension.ToFriendlyString(blockController.blockType), style);
    }
    
    public override void OnInspectorGUI ()
    {
        serializedObject.Update ();
        EditorGUILayout.PropertyField (blockType);
        serializedObject.ApplyModifiedProperties ();
    }
    void OnSceneGUI()
    {
        /*
        BlockController blockController = (BlockController)target;
        if (blockController == null)
        {
            return;
        }

        Handles.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(blockController.transform.position, BlockTypeExtension.ToFriendlyString(blockController.blockType), style);*/
    }
}
