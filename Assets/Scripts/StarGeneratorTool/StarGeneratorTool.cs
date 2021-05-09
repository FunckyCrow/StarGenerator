using UnityEngine;
using UnityEditor;

public class StarGeneratorTool : EditorWindow
{
    private const string k_DefaultStarsDatabaseLocation = "Assets/Databases/Default Stars Database.asset";
    private const string k_ActiveStarsDatabaseLocationKey = "ActiveStarsDatabaseLocation";

    private StarsDatabase m_starsDatabase = null;
    private StarData m_starData = null;
    private Vector2 m_scrollVector = Vector2.zero;
    private StarData m_selectedStar = null;

    [MenuItem("Tools/Star Generator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(StarGeneratorTool));
    }

    private void OnEnable()
    {
        ReinitializeNewPresetToDefault();
        if (EditorPrefs.HasKey(k_ActiveStarsDatabaseLocationKey))
        {
            m_starsDatabase = AssetDatabase.LoadAssetAtPath<StarsDatabase>(EditorPrefs.GetString(k_ActiveStarsDatabaseLocationKey));
            if (m_starsDatabase == null)
            {
                LoadDefaultStarsDatabase();
            }
        }
        else
        {
            LoadDefaultStarsDatabase();
        }
    }

    private void OnGUI()
    {
        #region Stars Database
        GUILayout.Label("Select Stars Database", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");


        EditorGUI.BeginChangeCheck();
        m_starsDatabase = EditorGUILayout.ObjectField("Stars Database", m_starsDatabase, typeof(StarsDatabase), false) as StarsDatabase;
        if (EditorGUI.EndChangeCheck())
        {
            EditorPrefs.SetString(k_ActiveStarsDatabaseLocationKey, AssetDatabase.GetAssetPath(m_starsDatabase.GetInstanceID()));
        }

        EditorGUILayout.EndVertical();
        #endregion

        #region Star Preset
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
        #endregion

        #region Spawn Star
        GUILayout.Label("\nAdd Star To Scene", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");

        m_scrollVector = EditorGUILayout.BeginScrollView(m_scrollVector, GUILayout.MinHeight(100), GUILayout.MaxHeight(300));

        for (int i = 0; i < m_starsDatabase.StarsPresets.Length; i++)
        {
            GUI.backgroundColor = (m_selectedStar != null && m_selectedStar == m_starsDatabase.StarsPresets[i]) ? Color.white : Color.grey;
            GUI.contentColor = m_starsDatabase.StarsPresets[i].Color;

            if (GUILayout.Button(m_starsDatabase.StarsPresets[i].Name))
            {
                m_selectedStar = m_starsDatabase.StarsPresets[i];
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
        #endregion
        
    }

    private void GeneratePreset()
    {
        ArrayUtility.Add<StarData>(ref m_starsDatabase.StarsPresets, m_starData);
        EditorUtility.SetDirty(m_starsDatabase);
        ReinitializeNewPresetToDefault();
    }

    private void ReinitializeNewPresetToDefault()
    {
        m_starData = new StarData();
        m_starData.Mesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
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

    private void LoadDefaultStarsDatabase()
    {
        m_starsDatabase = AssetDatabase.LoadAssetAtPath<StarsDatabase>(k_DefaultStarsDatabaseLocation);
        if (m_starsDatabase == null)
        {
            CreateDefaultStarsDatabase();
            m_starsDatabase = AssetDatabase.LoadAssetAtPath<StarsDatabase>(k_DefaultStarsDatabaseLocation);
        }
        EditorPrefs.SetString(k_ActiveStarsDatabaseLocationKey, k_DefaultStarsDatabaseLocation);
    }

    private void CreateDefaultStarsDatabase()
    {
        StarsDatabase StarsDatabaseAsset = ScriptableObject.CreateInstance<StarsDatabase>();
        AssetDatabase.CreateAsset(StarsDatabaseAsset, k_DefaultStarsDatabaseLocation);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}
