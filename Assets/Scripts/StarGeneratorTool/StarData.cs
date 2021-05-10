using UnityEngine;

[System.Serializable]
public class StarData
{
    [SerializeField]
    private string m_starName = "Star";
    [SerializeField]
    private float m_starRadius = 1f;
    [SerializeField]
    private float m_gravityRadius = 5f;
    [SerializeField]
    private Color m_starColor = Color.white;
    [SerializeField]
    private Mesh m_starMesh = null;

    public string Name { get => m_starName; set => m_starName = value; }
    public float Radius { get => m_starRadius; set => m_starRadius = value; }
    public float GravityRadius { get => m_gravityRadius; set => m_gravityRadius = value; }
    public Color Color { get => m_starColor; set => m_starColor = value; }
    public Mesh Mesh { get => m_starMesh; set => m_starMesh = value; }
    
    public StarData() { }
    public StarData(StarData starData)
    {
        m_starName = starData.Name;
        m_starRadius = starData.Radius;
        m_gravityRadius = starData.GravityRadius;
        m_starColor = starData.Color;
        m_starMesh = starData.Mesh;
    }
}
