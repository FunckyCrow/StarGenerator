using UnityEngine;

[ExecuteInEditMode]
public class StarObject : MonoBehaviour
{
    [SerializeField]
    private StarData m_starData;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    public StarData StarData => m_starData;
    public MeshFilter MeshFilter => m_meshFilter;
    public MeshRenderer MeshRenderer => m_meshRenderer;

    public void Initialize(StarData starData)
    {
        m_starData = new StarData(starData);
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        UpdateValues();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_starData.Color * Color.gray;
        Gizmos.DrawWireSphere(transform.position, m_starData.GravityRadius);
    }

    public void UpdateName()
    {
        gameObject.name = m_starData.Name;
    }
    
    public void UpdateRadius()
    {
        transform.localScale = new Vector3(m_starData.Radius * 2, m_starData.Radius * 2, m_starData.Radius * 2);
    }
    
    public void UpdateMesh()
    {
        m_meshFilter.sharedMesh = m_starData.Mesh;
    }
    
    public void UpdateColor()
    {
        m_meshRenderer.sharedMaterial.color = m_starData.Color;
    }

    public void UpdateValues()
    {
        UpdateName();
        UpdateRadius();
        UpdateMesh();
        UpdateColor();
    }

}
