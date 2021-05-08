using UnityEngine;
using UnityEditor;

public class StarGeneratorTool : EditorWindow
{
    private StarData m_starData = null;
    private Vector2 m_scrollVector = Vector2.zero;
    private StarData[] m_starArray = new StarData[0];
    private StarData m_selectedStar = null;

    [MenuItem("Tools/Star Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(StarGeneratorTool));
    }

    private void OnEnable()
    {
        StartNewPreset();
    }

    private void OnGUI()
    {
        GUILayout.Label("\nCreate New Star Preset", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");

        m_starData.Name = EditorGUILayout.TextField("Star Name", m_starData.Name);
        m_starData.Radius = EditorGUILayout.FloatField("Star Radius", m_starData.Radius);
        m_starData.GravityRadius = EditorGUILayout.FloatField("Gravity Well Radius", m_starData.GravityRadius);
        m_starData.Color = EditorGUILayout.ColorField("Star Color", m_starData.Color);
        m_starData.Mesh = EditorGUILayout.ObjectField("Star Mesh", m_starData.Mesh, typeof(Mesh), false) as Mesh;

        if (GUILayout.Button("Create Star Preset"))
        {
            GeneratePreset();
        }
        EditorGUILayout.EndVertical();


        GUILayout.Label("\nAdd Star To Scene", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");

        m_scrollVector = EditorGUILayout.BeginScrollView(m_scrollVector, GUILayout.MinHeight(100), GUILayout.MaxHeight(300));
        for (int i = 0; i < m_starArray.Length; i++)
        {
            GUI.backgroundColor = (m_selectedStar != null && m_selectedStar == m_starArray[i]) ? Color.white : Color.grey;
            GUI.contentColor = m_starArray[i].Color;

            if (GUILayout.Button(m_starArray[i].Name))
            {
                m_selectedStar = m_starArray[i];
            }
        }
        GUI.backgroundColor = Color.white;
        GUI.contentColor = Color.white;
        EditorGUILayout.EndScrollView();

        GUILayout.Label("");
        GUI.color = m_selectedStar != null ? Color.white : Color.grey;
        if (GUILayout.Button("Spawn Selected Star Preset") && m_selectedStar != null)
        {
            SpawnSelectedStarPreset();
        }

        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.EndVertical();
    }

    private void StartNewPreset()
    {
        m_starData = new StarData();
        m_starData.Mesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
    }

    private void GeneratePreset()
    {
        ArrayUtility.Add<StarData>(ref m_starArray, m_starData);
        StartNewPreset();
    }

    private void SpawnSelectedStarPreset()
    {
        GameObject SpawnedStar = new GameObject();
        SpawnedStar.AddComponent<MeshFilter>();
        var SpawnedStarMeshRenderer = SpawnedStar.AddComponent<MeshRenderer>();

        Material SpawnedStarMaterial = new Material(AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat"));
        SpawnedStarMeshRenderer.sharedMaterial = SpawnedStarMaterial;

        var SpawnedStarData = SpawnedStar.AddComponent<StarObject>();
        SpawnedStarData.Initialize(m_selectedStar);
    }

}
