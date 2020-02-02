using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPosition : MonoBehaviour
{
    public Vector2 clamp;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -clamp.x * .5f, clamp.x * .5f),
            Mathf.Clamp(transform.position.y, -clamp.y * .5f, clamp.y * .5f),
            transform.position.z
        );
    }
}
