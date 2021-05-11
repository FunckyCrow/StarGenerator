using UnityEngine;
using UnityEditor;

/// <summary>
/// Class for the custom editor tool made to generate star presets.
/// Can select the output database, create Star Preset, delete them and add them to the scene.
/// </summary>
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
        var window = GetWindow(typeof(StarGeneratorTool), false, "Star Generator");
        window.minSize = new Vector2(280, 300);
    }

    private void OnEnable()
    {
        ResetNewPresetToDefault();

        // Looking for the last used database.
        // If none is found, or if it fails to load, load/create the default database instead.
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

    // Draws the window.
    private void OnGUI()
    {
        GUILayout.Space(4f);
        
        DrawStarsDatabaseVerticalBox();
        
        GUILayout.Space(4f);
        
        DrawNewStarPresetVerticalBox();
        
        GUILayout.Space(4f);

        DrawExistingStarsVerticalBox();
    }

    /// <summary>
    /// Method displaying the Stars Database selection field.
    /// </summary>
    private void DrawStarsDatabaseVerticalBox()
    {
        GUILayout.Label("Select Stars Database", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");
        try
        {
            EditorGUI.BeginChangeCheck();
            m_starsDatabase = EditorGUILayout.ObjectField("Stars Database", m_starsDatabase, typeof(StarsDatabase), false) as StarsDatabase;
            if (EditorGUI.EndChangeCheck() && m_starsDatabase)
            {
                EditorPrefs.SetString(k_ActiveStarsDatabaseLocationKey, AssetDatabase.GetAssetPath(m_starsDatabase.GetInstanceID()));
            }
        }
        finally
        {
            EditorGUILayout.EndVertical();
        }
    }

    /// <summary>
    /// Method displaying the Star Presets creation section.
    /// </summary>
    private void DrawNewStarPresetVerticalBox()
    {
        GUILayout.Label("Create New Star Preset", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box");
        try
        {
            m_starData.Name = EditorGUILayout.TextField("Star Name", m_starData.Name);
            m_starData.Radius = EditorGUILayout.FloatField("Star Radius", m_starData.Radius);
            m_starData.GravityRadius = EditorGUILayout.FloatField("Gravity Well Radius", m_starData.GravityRadius);
            m_starData.Color = EditorGUILayout.ColorField("Star Color", m_starData.Color);
            m_starData.Mesh = EditorGUILayout.ObjectField("Star Mesh", m_starData.Mesh, typeof(Mesh), false) as Mesh;

            EditorGUILayout.BeginHorizontal();
            try
            {
                if (GUILayout.Button("Create Star Preset"))
                {
                    GeneratePreset();
                }

                if (GUILayout.Button("Reset to default"))
                {
                    ResetNewPresetToDefault();
                }
            }
            finally
            {
                EditorGUILayout.EndHorizontal();
            }

        }
        finally
        {
            EditorGUILayout.EndVertical();
        }
    }

    /// <summary>
    /// Method displaying the existing Star Presets section.
    /// </summary>
    private void DrawExistingStarsVerticalBox()
    {
        GUILayout.Label("Add Star To Scene", EditorStyles.boldLabel);
        EditorGUILayout.BeginVertical("Box", GUILayout.ExpandHeight(false));
        try
        {
            DrawStarsListScrollView();

            GUILayout.Space(4f);

            DrawSpawnStarButton();
        }
        finally
        {
            EditorGUILayout.EndVertical();
        }
    }

    /// <summary>
    /// Method displaying the scroll view containing the stars present in the active database and the buttons to delete them.
    /// </summary>
    private void DrawStarsListScrollView()
    {
        m_scrollVector = EditorGUILayout.BeginScrollView(m_scrollVector, GUILayout.MinHeight(00), GUILayout.MaxHeight(int.MaxValue), GUILayout.ExpandHeight(true));
        try
        {
            if (m_starsDatabase)
            {
                for (int i = 0; i < m_starsDatabase.StarsPresets.Length; i++)
                {
                    EditorGUILayout.BeginHorizontal();
                    try
                    {
                        GUI.backgroundColor = (m_selectedStar != null && m_selectedStar == m_starsDatabase.StarsPresets[i]) ? Color.white : Color.gray;
                        GUI.contentColor = m_starsDatabase.StarsPresets[i].Color;
                        if (GUILayout.Button(m_starsDatabase.StarsPresets[i].Name))
                        {
                            m_selectedStar = m_starsDatabase.StarsPresets[i];
                        }
                        ResetGuiColor();

                        GUI.color = Color.gray;
                        GUI.contentColor = Color.red;
                        GUI.backgroundColor = Color.gray;
                        if (GUILayout.Button("X", GUILayout.Width(25)))
                        {
                            DeleteStarPreset(m_starsDatabase.StarsPresets[i]);
                        }
                        ResetGuiColor();
                    }
                    finally
                    {
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
        finally
        {
            EditorGUILayout.EndScrollView();
        }
    }

    /// <summary>
    /// Method displaying the button to spawn stars in the scene.
    /// </summary>
    private void DrawSpawnStarButton()
    {
        GUI.color = m_selectedStar != null ? Color.white : Color.gray;
        if (GUILayout.Button("Spawn Selected Star Preset") && m_selectedStar != null)
        {
            SpawnSelectedStarPreset();
        }
        ResetGuiColor();
    }

    /// <summary>
    /// Method that resets the GUI colors to default (white).
    /// </summary>
    private void ResetGuiColor()
    {
        GUI.color = Color.white;
        GUI.contentColor = Color.white;
        GUI.backgroundColor = Color.white;
    }

    /// <summary>
    /// Method that resets the Star Presets creation section to the default values.
    /// </summary>
    private void ResetNewPresetToDefault()
    {
        m_starData = new StarData();
        m_starData.Mesh = Resources.GetBuiltinResource<Mesh>("New-Sphere.fbx");
    }

    /// <summary>
    /// Method that adds the new Star Preset to the active presets database.
    /// </summary>
    private void GeneratePreset()
    {
        ArrayUtility.Add<StarData>(ref m_starsDatabase.StarsPresets, m_starData);
        EditorUtility.SetDirty(m_starsDatabase);
        ResetNewPresetToDefault();
    }

    /// <summary>
    /// Method that instantiate the selected Star Preset to the scene.
    /// </summary>
    private void SpawnSelectedStarPreset()
    {
        GameObject SpawnedStar = new GameObject();
        var SpawnedStarData = SpawnedStar.AddComponent<StarObject>();
        SpawnedStar.AddComponent<MeshFilter>();
        var SpawnedStarMeshRenderer = SpawnedStar.AddComponent<MeshRenderer>();

        Material SpawnedStarMaterial = new Material(AssetDatabase.GetBuiltinExtraResource<Material>("Default-Diffuse.mat"));
        SpawnedStarMeshRenderer.sharedMaterial = SpawnedStarMaterial;

        SpawnedStarData.Initialize(m_selectedStar);
    }

    /// <summary>
    /// Method that deletes a specified Star Preset form the database.
    /// </summary>
    /// <param name="starData"></param>
    private void DeleteStarPreset(StarData starData)
    {
        ArrayUtility.Remove<StarData>(ref m_starsDatabase.StarsPresets, starData);
        EditorUtility.SetDirty(m_starsDatabase);
        if (m_selectedStar == starData)
        {
            m_selectedStar = null;
        }
    }

    /// <summary>
    /// Method that create the Default Star Database in the default path (cf. k_DefaultStarsDatabaseLocation).
    /// </summary>
    private void CreateDefaultStarsDatabase()
    {
        StarsDatabase StarsDatabaseAsset = ScriptableObject.CreateInstance<StarsDatabase>();
        AssetDatabase.CreateAsset(StarsDatabaseAsset, k_DefaultStarsDatabaseLocation);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// Method that loads the Default Star Database in the default path (cf. k_DefaultStarsDatabaseLocation).
    /// </summary>
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
}
