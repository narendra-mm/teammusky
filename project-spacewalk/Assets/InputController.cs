using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public float maxSpeed = 1;
    public float wobbleSpeed = 1f;
    public float maxWobbleVelocity = 1f;

    Vector2 targetPosition;
    Vector2 velocity;
    float speed = 1;
    public Vector2 wobbleVelocity;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // calculate wobble
        wobbleVelocity += new Vector2(
            (Random.value - .5f) * wobbleSpeed * Time.deltaTime, 
            (Random.value - .5f) * wobbleSpeed * Time.deltaTime
        );
        wobbleVelocity = Vector2.ClampMagnitude(wobbleVelocity, maxWobbleVelocity);

        // calculate movement
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 deltaPosition = targetPosition - (Vector2) transform.position;
        velocity = Vector2.MoveTowards(velocity, deltaPosition, speed * Time.deltaTime);
        velocity = Vector2.ClampMagnitude(velocity, Mathf.Min(deltaPosition.magnitude * 0.1f, maxSpeed));

        // targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        transform.position += (Vector3)wobbleVelocity;
        transform.position += (Vector3)velocity;
    }
}
