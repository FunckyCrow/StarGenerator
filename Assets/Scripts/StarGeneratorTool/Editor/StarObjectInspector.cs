using UnityEngine;
using UnityEditor;

/// <summary>
/// Class representing the custom inspector for the StarObject.
/// </summary>
[CustomEditor(typeof(StarObject))]
public class StarObjectInspector : Editor
{
    private StarObject m_starObject;

    private void OnEnable()
    {
        m_starObject = (StarObject)target;
    }

    // Draws the Inspector.
    public override void OnInspectorGUI()
    {
        // Update representation
        serializedObject.Update();

        // Name field update
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.Name = EditorGUILayout.TextField("Name", m_starObject.StarData.Name);
        if (EditorGUI.EndChangeCheck())
        {
            m_starObject.UpdateName();
        }
        if (m_starObject.name != m_starObject.StarData.Name)
        {
            m_starObject.StarData.Name = m_starObject.name;
        }

        // Color field update
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.Color = EditorGUILayout.ColorField("Star Color", m_starObject.StarData.Color);
        if (EditorGUI.EndChangeCheck())
        {
            m_starObject.UpdateColor();
        }
        if (m_starObject.MeshRenderer.sharedMaterial.color != m_starObject.StarData.Color)
        {
            m_starObject.StarData.Color = m_starObject.MeshRenderer.sharedMaterial.color;
        }

        // Mesh field update
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.Mesh = EditorGUILayout.ObjectField("Star Mesh", m_starObject.StarData.Mesh, typeof(Mesh), false) as Mesh;
        if (EditorGUI.EndChangeCheck())
        {
            m_starObject.UpdateMesh();
        }
        if (m_starObject.MeshFilter.sharedMesh != m_starObject.StarData.Mesh)
        {
            m_starObject.StarData.Mesh = m_starObject.MeshFilter.sharedMesh;
        }

        // Radius field update
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.Radius = EditorGUILayout.FloatField("Star Radius", m_starObject.StarData.Radius);
        if (EditorGUI.EndChangeCheck())
        {
            m_starObject.UpdateRadius();
        }

        // Gravity Well field update
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.GravityRadius = EditorGUILayout.FloatField("Gravity Well Radius", m_starObject.StarData.GravityRadius);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(m_starObject);
        }

        // Apply modifications to the object
        serializedObject.ApplyModifiedProperties();
    }
}
