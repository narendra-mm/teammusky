using UnityEngine;

public class SortRenderer : MonoBehaviour
{

	public ParticleSystem ParticleSystem;
	public string LayerName;
	public int LayerOrder;

	private ParticleSystemRenderer particleRenderer;
    // Start is called before the first frame update
    void Start()
    {
	    particleRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
		UpdateSorting();
    }

	[ContextMenu("UpdateSorting")]
	public void UpdateSorting()
	{
		particleRenderer.sortingLayerName = LayerName;
		//particleRenderer.sortingLayerID = LayerID;
		particleRenderer.sortingOrder = LayerOrder;
	}

}
