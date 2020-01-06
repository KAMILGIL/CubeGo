using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(BlockController))]
[InitializeOnLoad]
class LabelHandle : Editor
{
    private BlockController blockController; 
    void OnEnable()
    {
        SceneView.onSceneGUIDelegate += (SceneView.OnSceneFunc)Delegate.Combine(SceneView.onSceneGUIDelegate, new SceneView.OnSceneFunc(CustomOnSceneGUI));
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
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(blockController.transform.position + Vector3.up * 0.5f, BlockTypeExtension.ToFriendlyString(blockController.blockType), style);

    }
    
    void OnSceneGUI()
    {
        BlockController blockController = (BlockController)target;
        if (blockController == null)
        {
            return;
        }

        Handles.color = Color.blue;
        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(blockController.transform.position + Vector3.up * 0.5f, BlockTypeExtension.ToFriendlyString(blockController.blockType), style);

        //Handles.BeginGUI();
        /*if (GUILayout.Button("Reset Area", GUILayout.Width(100)))
        {
            handleExample.shieldArea = 5;
        }
        Handles.EndGUI();


        Handles.DrawWireArc(handleExample.transform.position,
            handleExample.transform.up,
            -handleExample.transform.right,
            180,
            handleExample.shieldArea);
        handleExample.shieldArea =
            Handles.ScaleValueHandle(handleExample.shieldArea,
                handleExample.transform.position + handleExample.transform.forward * handleExample.shieldArea,
                handleExample.transform.rotation,
                1,
                Handles.ConeHandleCap,
                1);*/
    }
}