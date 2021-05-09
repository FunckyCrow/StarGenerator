using UnityEngine;

[ExecuteInEditMode]
public class StarObject : MonoBehaviour
{
    [SerializeField]
    private StarData m_starData;
    private MeshRenderer m_meshRenderer;
    private MeshFilter m_meshFilter;

    public void Initialize(StarData starData)
    {
        m_starData = starData;
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_meshFilter = GetComponent<MeshFilter>();

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
        m_meshRenderer.sharedMaterial.color = m_starData.Color;
        m_meshFilter.mesh = m_starData.Mesh;
    }
}
