using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float _activeIntensity;

    public LightFader lightFader;
    public bool alwaysActive = true;

    public float maxSpeed = 1f;
    public float minDistance = 1f;
    private Rigidbody2D rb2D;

    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnMouseDown()
    {
        isGrabbed = true;
        if (!alwaysActive && lightFader != null)
        {
            lightFader.SetTargetIntensity(_activeIntensity);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
            if (!alwaysActive && lightFader != null)
            {
                lightFader.SetTargetIntensity(0);
            }
        }
    }

    void FixedUpdate()
    {
        if (alwaysActive && lightFader != null)
        {
            lightFader.SetTargetIntensity(_activeIntensity);
        }
        if (!isGrabbed)
        {
            return;
        }
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 deltaPosition = targetPosition - (Vector2)transform.position;
        // if(deltaPosition.magnitude > minDistance) {
        rb2D.AddForce(Vector2.ClampMagnitude(deltaPosition * .5f, maxSpeed));
        // }
    }
}
