using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour
{

    public float wobbleSpeed = .1f;
    public float maxWobbleVelocity = .3f;
    public Vector2 wobbleVelocity;

    // Update is called once per frame
    void Update()
    {
        wobbleVelocity += new Vector2(
            (Random.value - .5f) * wobbleSpeed * Time.deltaTime, 
            (Random.value - .5f) * wobbleSpeed * Time.deltaTime
        );
        wobbleVelocity = Vector2.ClampMagnitude(wobbleVelocity, maxWobbleVelocity);

        transform.position += (Vector3)wobbleVelocity;
    }
}
