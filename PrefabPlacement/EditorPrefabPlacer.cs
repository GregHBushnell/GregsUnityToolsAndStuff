using UnityEngine;
using UnityEditor;
using UnityEngine.U2D;
namespace FellOnline.WorldBuilding
{
[CustomEditor(typeof(PrefabPlacer))]
public class EditorPrefabPlacer : Editor
{
  public override void OnInspectorGUI()
    {
          DrawDefaultInspector();
             PrefabPlacer prefabPlacer = (PrefabPlacer)target;
        
   
     
       
        if (GUILayout.Button("Place Prefabs"))
        {
            prefabPlacer.PlacePrefabs();
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(prefabPlacer);
            }
        }
         if (GUILayout.Button("Clear Prefabs"))
        {
            prefabPlacer.ClearLastPlacedPrefabs();
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(prefabPlacer);
            }
        }
         
        if (GUILayout.Button("Confirm Prefabs"))
        {
            prefabPlacer.ConfirmPlacement();
            if (!Application.isPlaying)
            {
                EditorUtility.SetDirty(prefabPlacer);
            }
        }


        
          
    }

       void OnSceneGUI()
         {
        PrefabPlacer prefabPlacer = (PrefabPlacer)target;

        Vector3 ParentPos = prefabPlacer.PrefabParent != null ? prefabPlacer.PrefabParent.position : prefabPlacer.transform.position;
        // Convert the start position from tile coordinates to world space
        Vector3 CornerOneWorld = new Vector3(prefabPlacer.Corners[0].x, prefabPlacer.Corners[0].y, prefabPlacer.Corners[0].z) + ParentPos;
        
        Vector3 CornerTwoWorld = new Vector3(prefabPlacer.Corners[1].x, prefabPlacer.Corners[1].y, prefabPlacer.Corners[1].z) +ParentPos;
        Vector3 CornerThreeWorld = new Vector3(prefabPlacer.Corners[2].x, prefabPlacer.Corners[2].y, prefabPlacer.Corners[2].z) + ParentPos;
        Vector3 CornerFourWorld = new Vector3(prefabPlacer.Corners[3].x, prefabPlacer.Corners[3].y, prefabPlacer.Corners[3].z) + ParentPos;

        // Draw a sphere handle at the end position
        Handles.color = Color.green; // Set the color to green, or any color you prefer
        Handles.SphereHandleCap(0, CornerOneWorld, Quaternion.identity, 0.5f, EventType.Repaint); // Adjust the size (0.5f) as needed
        // Draw a label at the start position
        GUIStyle CornerOnelabelStyle = new GUIStyle();
        CornerOnelabelStyle.normal.textColor = Color.white; // Set the text color
        CornerOnelabelStyle.fontSize = 15; // Set the font size
        Handles.Label(CornerOneWorld + Vector3.up * 0.5f, "Corner One", CornerOnelabelStyle); // Adjust the label position as needed

        Handles.color = Color.yellow; // Set the color to red, or any color you prefer
        Handles.SphereHandleCap(0, CornerTwoWorld, Quaternion.identity, 0.5f, EventType.Repaint); // Adjust the size (0.5f) as needed
        GUIStyle CornerTwolabelStyle = new GUIStyle();
        CornerTwolabelStyle.normal.textColor = Color.white; // Set the text color
        CornerTwolabelStyle.fontSize = 15; // Set the font size
        Handles.Label(CornerTwoWorld + Vector3.up * 0.5f, "Corner Two", CornerTwolabelStyle); // Adjust the label position as needed

        Handles.color = Color.magenta; // Set the color to red, or any color you prefer
        Handles.SphereHandleCap(0, CornerThreeWorld, Quaternion.identity, 0.5f, EventType.Repaint); // Adjust the size (0.5f) as needed
         GUIStyle CornerThreelabelStyle = new GUIStyle();
        CornerThreelabelStyle.normal.textColor = Color.white; // Set the text color
        CornerThreelabelStyle.fontSize = 15; // Set the font size
        Handles.Label(CornerThreeWorld + Vector3.up * 0.5f, "Corner Three", CornerThreelabelStyle); // Adjust the label position as needed

        Handles.color = Color.red; // Set the color to red, or any color you prefer
        Handles.SphereHandleCap(0, CornerFourWorld, Quaternion.identity, 0.5f, EventType.Repaint); // Adjust the size (0.5f) as needed
        GUIStyle CornerFourlabelStyle = new GUIStyle();
         CornerFourlabelStyle.normal.textColor = Color.white; // Set the text color
        CornerFourlabelStyle.fontSize = 15; // Set the font size
        Handles.Label(CornerFourWorld + Vector3.up * 0.5f, "Corner Four", CornerFourlabelStyle); // Adjust the label position as needed
    

        // Optionally, allow the user to drag the start position handle in the Scene view
        EditorGUI.BeginChangeCheck();
        Vector3 NewCornerOneWorld = Handles.PositionHandle(CornerOneWorld, Quaternion.identity);
         Vector3 NewCornerTwoWorld = Handles.PositionHandle(CornerTwoWorld, Quaternion.identity);
           Vector3 NewCornerThreeWorld = Handles.PositionHandle(CornerThreeWorld, Quaternion.identity);
         Vector3 NewCornerFourWorld = Handles.PositionHandle(CornerFourWorld, Quaternion.identity);
       
        if (EditorGUI.EndChangeCheck())
        {
            // Convert the world position back to tile coordinates and update the start position
            Undo.RecordObject(prefabPlacer, "Change Corner 1 Position");
            Vector3 delta1 = NewCornerOneWorld -ParentPos;
            prefabPlacer.Corners[0] = new Vector3(delta1.x ,delta1.y ,delta1.z);

             Undo.RecordObject(prefabPlacer, "Change Corner 2 Position");
            Vector3 delta2 = NewCornerTwoWorld - ParentPos;
            prefabPlacer.Corners[1] = new Vector3(delta2.x ,delta2.y ,delta2.z);

             Undo.RecordObject(prefabPlacer, "Change Corner 3 Position");
            Vector3 delta3 = NewCornerThreeWorld - ParentPos;
            prefabPlacer.Corners[2] = new Vector3(delta3.x ,delta3.y ,delta3.z);

             Undo.RecordObject(prefabPlacer, "Change Corner 4 Position");
            Vector3 delta4 = NewCornerFourWorld - ParentPos;
            prefabPlacer.Corners[3] = new Vector3(delta4.x ,delta4.y ,delta4.z);

          


        }
    }

}
}