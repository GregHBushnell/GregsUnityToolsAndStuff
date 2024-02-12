using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPlacer : MonoBehaviour
{

    public GameObject PrefabToPlace; // Assign this in the editor
    public Transform PrefabParent; // Assign this in the editor
    public float prefabSpacing = 2.0f; // Space between prefabs

    private List<GameObject> LastPlacedPrefabs;
    public Vector3[] Corners = new Vector3[4]
    {
        new Vector3(0, 0, 0), //  corner 1
        new Vector3(0, 0, 1), // corner 2
        new Vector3(1, 0, 1), //  corner 3 
        new Vector3(1, 0, 0)  // corner 4
    };  

    public void PlacePrefabs()
    {

       ClearLastPlacedPrefabs();
        if (PrefabToPlace == null || Corners.Length != 4) return;

        for (int i = 0; i < Corners.Length; i++)
        {
            Vector3 startCorner = Corners[i];
            Vector3 endCorner = Corners[(i + 1) % Corners.Length];
            PlacePrefabsAlongSide(startCorner, endCorner);
        }
    }
    public void ConfirmPlacement(){
        LastPlacedPrefabs.Clear();
    }
    public void ClearLastPlacedPrefabs(){
        if(LastPlacedPrefabs != null){
            foreach (GameObject prefab in LastPlacedPrefabs)
            {
                DestroyImmediate(prefab);
            }
            LastPlacedPrefabs.Clear();
        }
    }
     void PlacePrefabsAlongSide(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);
        int prefabCount = Mathf.Max(1, Mathf.FloorToInt(distance / prefabSpacing));
        float actualSpacing = distance / prefabCount;

        for (int i = 0; i < prefabCount; i++)
        {
            Vector3 position = start + direction * (actualSpacing * i + actualSpacing / 2);
            Quaternion rotation = Quaternion.LookRotation(direction);
            GameObject NewPrefab;
            Vector3 worldPosition = PrefabParent != null ? PrefabParent.TransformPoint(position) : position; // Convert local position to world position
             Quaternion worldRotation = PrefabParent != null ? PrefabParent.rotation * rotation: rotation; // Convert local rotation to world rotation
             NewPrefab = Instantiate(PrefabToPlace, worldPosition, worldRotation);
             LastPlacedPrefabs.Add(NewPrefab);
            NewPrefab.transform.SetParent(PrefabParent, true);
        }
    }

}
