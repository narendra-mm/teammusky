using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private Sprite handOpen;
    [SerializeField] private Sprite handClosed;
    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            renderer.sprite = handClosed;
        }
        if (Input.GetMouseButtonUp(0))
        {
            renderer.sprite = handOpen;
        }
    }
}
