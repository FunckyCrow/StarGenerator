using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StarObject))]
public class StarObjectInspector : Editor
{
    private StarObject m_starObject;

    private void OnEnable()
    {
        m_starObject = (StarObject)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
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

        
        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.Radius = EditorGUILayout.FloatField("Star Radius", m_starObject.StarData.Radius);
        if (EditorGUI.EndChangeCheck())
        {
            m_starObject.UpdateRadius();
        }

        EditorGUI.BeginChangeCheck();
        m_starObject.StarData.GravityRadius = EditorGUILayout.FloatField("Gravity Well Radius", m_starObject.StarData.GravityRadius);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(m_starObject);
        }

        serializedObject.ApplyModifiedProperties();
    }

}
