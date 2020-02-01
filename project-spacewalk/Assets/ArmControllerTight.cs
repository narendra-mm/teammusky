using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmControllerTight : MonoBehaviour
{
    [SerializeField] private Transform ik;
    [SerializeField] private Vector2 constraintCenter;
    [SerializeField] private Vector2 constraintSize = new Vector2(14f, 7f);
    [SerializeField] private Vector2 ikOffset;

    [SerializeField] private float speed = 2f;
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
        // if(deltaPosition.magnitude > 2f) {
        //     rb2D.AddForce(deltaPosition.normalized * speed);
        // }

        rb2D.velocity = deltaPosition * speed;
        Vector2 position = ik.position;
        // if (position.x < constraintCenter.x - constraintSize.x * .5f || position.x > constraintCenter.x + constraintSize.x * .5f)
        // {
        //     ik.position = new Vector2(
        //         Mathf.Clamp(position.x, constraintCenter.x - constraintSize.x * .5f, constraintCenter.x + constraintSize.x * .5f),
        //         position.y
        //     );
        //     rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
        //     rb2D.angularVelocity = 0f;
        // }
        // if (position.y < constraintCenter.y - constraintSize.y * .5f || position.y > constraintCenter.y + constraintSize.y * .5f)
        // {
        //     ik.position = new Vector2(
        //         position.x,
        //         Mathf.Clamp(position.y, constraintCenter.y - constraintSize.y * .5f, constraintCenter.y + constraintSize.y * .5f)
        //     );
        //     rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
        //     rb2D.angularVelocity = 0f;
        // }
        ik.position = new Vector2(
            Mathf.Clamp(position.x, constraintCenter.x - constraintSize.x * .5f, constraintCenter.x + constraintSize.x * .5f),
            Mathf.Clamp(position.y, constraintCenter.y - constraintSize.y * .5f, constraintCenter.y + constraintSize.y * .5f)
        );

    }
}
