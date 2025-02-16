using UnityEditor;
using UnityEngine;

public class MissingMeshFinder : EditorWindow
{
    [MenuItem("Tools/Find Missing Meshes")]
    public static void ShowWindow()
    {
        GetWindow<MissingMeshFinder>("Missing Mesh Finder");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing Meshes"))
        {
            FindMissingMeshes();
        }
    }

    private void FindMissingMeshes()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (PrefabUtility.IsPartOfAnyPrefab(obj) || obj.hideFlags == HideFlags.NotEditable || obj.hideFlags == HideFlags.HideAndDontSave)
                continue;

            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh == null)
            {
                Debug.LogWarning($"Missing mesh in object: {obj.name}", obj);
                // ���⿡ �ڵ� ���� ���� �߰�
            }
        }
        EditorUtility.DisplayDialog("Missing Mesh Finder", "Missing mesh �˻� �Ϸ�!", "OK");
    }
}
