using UnityEditor;
using UnityEngine;

public class FindMaterialsByShader : EditorWindow
{
    string shaderName = "Lpk/LightModel/ToonLightBase";

    [MenuItem("Tools/Find Materials by Shader")]
    static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(FindMaterialsByShader), false, "Find Materials");
    }

    void OnGUI()
    {
        GUILayout.Label("Find Materials Using Shader", EditorStyles.boldLabel);
        shaderName = EditorGUILayout.TextField("Shader Name", shaderName);

        if (GUILayout.Button("Find Materials"))
        {
            FindMaterials();
        }
    }

    void FindMaterials()
    {
        Shader shader = Shader.Find(shaderName);
        if (shader == null)
        {
            Debug.LogError($"Shader '{shaderName}' not found in project. Make sure the name is correct.");
            return;
        }

        string[] guids = AssetDatabase.FindAssets("t:Material");
        int count = 0;
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat != null && mat.shader != null && mat.shader.name == shaderName)
            {
                Debug.Log($"Material: {path} uses shader: {shaderName}");
                count++;
            }
        }

        Debug.Log($"Found {count} materials using shader '{shaderName}'");
    }
}
