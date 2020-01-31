using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public float grabRange = .5f;
    public float maxSpeed = 1f;
    public float minDistance = 1f;
    private Rigidbody2D rb2D;

    private bool isGrabbed = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 deltaPosition = mousePosition - (Vector2)transform.position;
            if (deltaPosition.magnitude < grabRange)
            {
                isGrabbed = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isGrabbed = false;
        }
    }

    void FixedUpdate()
    {
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
