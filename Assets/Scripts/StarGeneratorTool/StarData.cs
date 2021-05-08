using UnityEngine;

public class StarData
{
    private string m_starName = "Star";
    private float m_starRadius = 1f;
    private float m_gravityWellRadius = 5f;
    private Color m_starColor = Color.white;
    private Mesh m_starMesh = null;

    public string StarName { get => m_starName; set => m_starName = value; }
    public float StarRadius { get => m_starRadius; set => m_starRadius = value; }
    public float GravityWellRadius { get => m_gravityWellRadius; set => m_gravityWellRadius = value; }
    public Color StarColor { get => m_starColor; set => m_starColor = value; }
    public Mesh StarMesh { get => m_starMesh; set => m_starMesh = value; }
}
