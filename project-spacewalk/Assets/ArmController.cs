using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] public Transform ik;
    [SerializeField] private Vector2 constraintCenter;
    [SerializeField] private Vector2 constraintSize;
    [SerializeField] private Vector2 ikOffset;

    [SerializeField] private float maxSpeed = 2f;
    public Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = ik.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 deltaPosition = targetPosition - (Vector2)ik.position;
        rb2D.AddForce(Vector2.ClampMagnitude(deltaPosition * .5f, maxSpeed));

        Vector2 position = ik.position;
        if (position.x < constraintCenter.x - constraintSize.x * .5f || position.x > constraintCenter.x + constraintSize.x * .5f)
        {
            rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
            rb2D.angularVelocity = 0f;
            ik.position = new Vector2(
                Mathf.Clamp(position.x, constraintCenter.x - constraintSize.x * .5f, constraintCenter.x + constraintSize.x * .5f),
                position.y
            );
        }
        if (position.y < constraintCenter.y - constraintSize.y * .5f || position.y > constraintCenter.y + constraintSize.y * .5f)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.angularVelocity = 0f;
            ik.position = new Vector2(
                position.x,
                Mathf.Clamp(position.y, constraintCenter.y - constraintSize.y * .5f, constraintCenter.y + constraintSize.y * .5f)
            );
        }
        // ik.position = new Vector2(
        //     Mathf.Clamp(position.x, constraintCenter.x - constraintSize.x * .5f, constraintCenter.x + constraintSize.x * .5f),
        //     Mathf.Clamp(position.y, constraintCenter.y - constraintSize.y * .5f, constraintCenter.y + constraintSize.y * .5f)
        // );

    }
}
