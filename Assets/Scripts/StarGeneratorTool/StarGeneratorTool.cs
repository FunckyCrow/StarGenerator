using UnityEngine;
using UnityEditor;

public class StarGeneratorTool : EditorWindow
{
    StarData m_starData = new StarData();

    [MenuItem("Tools/Star Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(StarGeneratorTool));
    }

    private void OnEnable()
    {
        m_starData.StarMesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
    }

    private void OnGUI()
    {
        GUILayout.Label("\nCreate New Star Preset", EditorStyles.boldLabel);

        m_starData.StarName = EditorGUILayout.TextField("Star Name", m_starData.StarName);
        m_starData.StarRadius = EditorGUILayout.FloatField("Star Radius", m_starData.StarRadius);
        m_starData.GravityWellRadius = EditorGUILayout.FloatField("Gravity Well Radius", m_starData.GravityWellRadius);
        m_starData.StarColor = EditorGUILayout.ColorField("Star Color", m_starData.StarColor);
        m_starData.StarMesh = EditorGUILayout.ObjectField("Star Mesh", m_starData.StarMesh, typeof(Mesh), false) as Mesh;

        if (GUILayout.Button("Create Start Preset"))
        {
            GeneratePreset();
        }

    }

    private void GeneratePreset()
    {
    }
}
