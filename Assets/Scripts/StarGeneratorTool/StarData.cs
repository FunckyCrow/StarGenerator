using UnityEngine;

public class StarData
{
    private string m_starName = "Star";
    private float m_starRadius = 1f;
    private float m_gravityRadius = 5f;
    private Color m_starColor = Color.white;
    private Mesh m_starMesh = null;

    public string Name { get => m_starName; set => m_starName = value; }
    public float Radius { get => m_starRadius; set => m_starRadius = value; }
    public float GravityRadius { get => m_gravityRadius; set => m_gravityRadius = value; }
    public Color Color { get => m_starColor; set => m_starColor = value; }
    public Mesh Mesh { get => m_starMesh; set => m_starMesh = value; }
}
