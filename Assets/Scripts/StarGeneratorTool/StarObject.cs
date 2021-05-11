using UnityEngine;

/// <summary>
/// Class for the Star Object.
/// It is in charge of updating the star datas.
/// </summary>
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

    /// <summary>
    /// Method that has to be called manually after creating a StarObject to initialize its values.
    /// </summary>
    /// <param name="starData">Values for the initialization.</param>
    public void Initialize(StarData starData)
    {
        m_starData = new StarData(starData);
        m_meshFilter = GetComponent<MeshFilter>();
        m_meshRenderer = GetComponent<MeshRenderer>();

        UpdateValues();
    }

    // Draws the Gravity Well as a wire sphere gizmo when the Star GameObject is selected in the scene.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = m_starData.Color * Color.gray;
        Gizmos.DrawWireSphere(transform.position, m_starData.GravityRadius);
    }

    /// <summary>
    /// Method that updates the Name contained in the StarData.
    /// </summary>
    public void UpdateName()
    {
        gameObject.name = m_starData.Name;
    }

    /// <summary>
    /// Method that updates the Radius contained in the StarData.
    /// </summary>
    public void UpdateRadius()
    {
        transform.localScale = new Vector3(m_starData.Radius * 2, m_starData.Radius * 2, m_starData.Radius * 2);
    }

    /// <summary>
    /// Method that updates the Mesh contained in the StarData.
    /// </summary>
    public void UpdateMesh()
    {
        if (!m_meshFilter)
        {
            m_meshFilter = GetComponent<MeshFilter>();
        }
        m_meshFilter.sharedMesh = m_starData.Mesh;
    }

    /// <summary>
    /// Method that updates the Color contained in the StarData.
    /// </summary>
    public void UpdateColor()
    {
        if (!m_meshRenderer)
        {
            m_meshRenderer = GetComponent<MeshRenderer>();
        }
        m_meshRenderer.sharedMaterial.color = m_starData.Color;
    }

    /// <summary>
    /// Method that updates every data contained in the StarData.
    /// </summary>
    public void UpdateValues()
    {
        UpdateName();
        UpdateRadius();
        UpdateMesh();
        UpdateColor();
    }

}
