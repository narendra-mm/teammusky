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
            Vector3 myRotation = transform.rotation.eulerAngles;
            Vector3 handRotation = _grabbedBy.rotation.eulerAngles;
            Vector3 offsetRotation = _anchor.rotation.eulerAngles;
            Vector3 newRotation = new Vector3(0f, 0f, handRotation.z - myRotation.z - (myRotation.z - offsetRotation.z));
            Debug.Log("My: "+ myRotation + ", Hand: "+ handRotation + ", New: "+ newRotation);
            
            transform.Rotate(newRotation, Space.World);

            Vector3 transformOffset = transform.position - _anchor.position;

            // Snap to position
            transform.position = new Vector3(_grabbedBy.position.x, _grabbedBy.position.y, transform.position.z) + transformOffset;

            // Move to position, so we get some physic interactions
            // rb2D.MovePosition(new Vector3(_grabbedBy.position.x, _grabbedBy.position.y, transform.position.z) + transformOffset);
        }
    }
}
