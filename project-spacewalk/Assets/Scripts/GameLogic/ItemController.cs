using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private float _activeIntensity = 8.5f;

    public LightFader lightFader;
    public bool alwaysActive = true;

    public float maxSpeed = 1f;
    public float minDistance = 1f;
    private Rigidbody2D rb2D;

    private bool _isGrabbed = false;
    private bool _allowInputs = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    public void EnableInputs()
    {
        _isGrabbed = false;
        _allowInputs = true;
    }

    public void DisableInputs()
    {
        _isGrabbed = false;
        _allowInputs = false;
    }

    void OnMouseDown()
    {
        if (!_allowInputs)
        {
            return;
        }
        _isGrabbed = true;
        if (!alwaysActive && lightFader != null)
        {
            lightFader.SetTargetIntensity(_activeIntensity);
        }
    }

    void Update()
    {
        if (!_allowInputs)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isGrabbed = false;
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
        if (!_isGrabbed)
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
