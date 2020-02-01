using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rivet : MonoBehaviour
{
    // Update is called once per frame
    void Awake()
    {
        Collider2D[] col = Physics2D.OverlapPointAll(transform.position);
        bool hitFrame = false;
        bool hitOxygen = false;
        bool hitElectric = false;
        if (col.Length > 0)
        {
            foreach (Collider2D c in col)
            {
                switch (c.tag)
                {
                    case "Frame":
                        hitFrame = true;
                        break;
                    case "Oxygen":
                        hitOxygen = true;
                        break;
                    case "Electric":
                        hitElectric = true;
                        break;
                }
            }
        }
        if (hitFrame)
        {
            onHitFrame();
        }
        if (hitOxygen)
        {
            onHitOxygen();
        }
        if (hitElectric)
        {
            onHitElectric();
        }
    }

    public void onHitFrame()
    {
        Debug.Log("Hit Frame!");
    }

    public void onHitOxygen()
    {
        Debug.Log("Hit Oxygen!");
    }

    public void onHitElectric()
    {
        Debug.Log("Hit Electric!");
    }
}
