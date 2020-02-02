using UnityEngine;

public class GrabbableItem : MonoBehaviour
{
    [SerializeField] private float _activeIntensity = 8.5f;
    [SerializeField] private Transform _anchor;

    public bool AlwaysOn = false;

    public LightFader lightFader;

    private Rigidbody2D rb2D;
    private Collider2D collider;


    private bool _isGrabbing = false;
    private bool _isOverlappingHand = false;
    private bool _isGrabbed = false;

    private Transform _grabbedBy;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        collider = gameObject.GetComponent<Collider2D>();
        if (lightFader != null && AlwaysOn)
        {
            lightFader.SetTargetIntensity(_activeIntensity);
        }
    }

    public bool IsGrabbed() {
        return _isGrabbed;
    }

    private void TryGrab()
    {
        if (_isGrabbing && _isOverlappingHand)
        {
            if (lightFader != null && !AlwaysOn)
            {
                lightFader.SetTargetIntensity(_activeIntensity);
            }
            _isGrabbed = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hand")
        {
            _isOverlappingHand = true;
            _grabbedBy = other.transform;
        }
        TryGrab();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hand")
        {
            _isOverlappingHand = false;
        }
    }

    void OnMouseDown()
    {
        _isGrabbing = true;
        TryGrab();
    }

    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            if (_grabbedBy != null && _isGrabbed)
            {
                var arm = FindObjectsOfType<ArmControllerTight>()[0].rb2D;
                rb2D.velocity = arm.velocity;
                rb2D.angularVelocity = arm.angularVelocity;
                if (lightFader != null && !AlwaysOn)
                {
                    lightFader.SetTargetIntensity(0);
                }
            }
            _isGrabbing = false;
            _isGrabbed = false;
        }
    }

    void FixedUpdate()
    {
        if (_isGrabbed && _grabbedBy != null)
        {
            Vector3 offset = Vector3.zero;
            Vector3 rotation = transform.rotation.eulerAngles;
            Vector3 rotationOffset = Vector3.zero;
            if(_anchor != null) {
                offset = transform.position - _anchor.position;
                rotationOffset = transform.rotation.eulerAngles - _anchor.rotation.eulerAngles;
            }
            transform.eulerAngles = _grabbedBy.rotation.eulerAngles + rotationOffset;
            transform.position = new Vector3(_grabbedBy.position.x, _grabbedBy.position.y, transform.position.z) + offset;
        }
    }
}
