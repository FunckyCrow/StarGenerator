using UnityEngine;

[ExecuteInEditMode]
public class StarObject : MonoBehaviour
{
    [SerializeField]
    private StarData m_starData;
    private MeshFilter m_meshFilter;
    private MeshRenderer m_meshRenderer;

    public void Initialize(StarData starData)
    {
        m_starData = new StarData(starData);
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        UpdateValues();
    }

    private void Update()
    {
        UpdateValues();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_starData.Color * Color.grey;
        Gizmos.DrawWireSphere(transform.position, m_starData.GravityRadius);
    }

    private void UpdateValues()
    {
        gameObject.name = m_starData.Name;
        transform.localScale = new Vector3(m_starData.Radius * 2, m_starData.Radius * 2, m_starData.Radius * 2);
        m_meshFilter.mesh = m_starData.Mesh;
        m_meshRenderer.sharedMaterial.color = m_starData.Color;
    }
}
