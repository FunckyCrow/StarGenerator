using UnityEngine;

public class StarObject : MonoBehaviour
{
    private StarData m_starData;
    private MeshRenderer m_meshRenderer;
    private MeshFilter m_meshFilter;

    public StarData StarData { get => m_starData; set => m_starData = value; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_starData.Color * Color.grey;
        Gizmos.DrawWireSphere(transform.position, m_starData.GravityRadius);
    }

    public void Initialize(StarData starData)
    {
        m_starData = starData;
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_meshFilter = GetComponent<MeshFilter>();

        gameObject.name = m_starData.Name;
        transform.localScale = new Vector3(m_starData.Radius * 2, m_starData.Radius * 2, m_starData.Radius * 2);
        m_meshRenderer.sharedMaterial.color = m_starData.Color;
        m_meshFilter.mesh = m_starData.Mesh;
    }
}
