using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatForce : MonoBehaviour
{

    private Rigidbody2D rb2D;

    public float floatSpeed = .01f;
    public float maxFloatVelocity = .03f;
    public Vector2 floatVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatVelocity += new Vector2(
            (Random.value - .5f) * floatSpeed,
            (Random.value - .5f) * floatSpeed
        );
        floatVelocity = Vector2.ClampMagnitude(floatVelocity, maxFloatVelocity);

        rb2D.AddForce(floatVelocity);
    }
}
