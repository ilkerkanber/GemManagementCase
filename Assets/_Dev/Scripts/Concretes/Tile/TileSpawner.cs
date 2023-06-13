using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class TileSpawner : MonoBehaviour
{
    [SerializeField] float offSetDistance;
    [SerializeField] GameObject tilePlane;
    [field:SerializeField] public int ZSize { get; private set; }
    [field:SerializeField] public int XSize { get; private set; }
    //
    void Awake()
    {
        ObjectManager.TileSpawner = this;
    }
    public void CreateMap()
    {
        if (transform.childCount != 0)
        {
            return;
        }
 
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                var spawnPoint = transform.position + new Vector3(x*offSetDistance, 0, z*offSetDistance);
                var spawnedObj = Instantiate(tilePlane, spawnPoint, Quaternion.identity, transform);
                spawnedObj.gameObject.name = x + "_" + z;
            }
        }
    }
    public void ClearMap()
    {
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject go = transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
    }

    #region Editor
    [CustomEditor(typeof(TileSpawner))]
    public class MapInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var tileSpawner = (TileSpawner)target;
            if (GUILayout.Button("Create Map"))
            {
                tileSpawner.CreateMap();
            }
            if (GUILayout.Button("Clear Map"))
            {
                tileSpawner.ClearMap();
            }
        }
    }
    #endregion
}
