using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(StarsDatabase))]
public class StarsDatabaseInspector : Editor
{
    private StarsDatabase m_starsDatabase;

    private void OnEnable()
    {
        m_starsDatabase = (StarsDatabase)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Space(4f);
        EditorGUILayout.BeginHorizontal();
        try
        {
            if (GUILayout.Button("Save as JSON"))
            {
                SaveAsJson();
            }
            if (GUILayout.Button("Load from JSON"))
            {
                LoadFromJson();
            }
        }
        finally
        {
            EditorGUILayout.EndHorizontal();
        }
    }

    private void SaveAsJson()
    {
        var path = EditorUtility.SaveFilePanel("Save database as Json", "", m_starsDatabase.name + ".json", "json");
        if (path.Length != 0)
        {
            string Json = JsonUtility.ToJson(m_starsDatabase);
            if (Json != null)
            File.WriteAllText(path, Json);
        }
    }

    private void LoadFromJson()
    {
        string path = EditorUtility.OpenFilePanel("Load database fron Json", "", "json");
        if (path.Length != 0)
        {
            var fileContent = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(fileContent, m_starsDatabase);
        }
    }

}
