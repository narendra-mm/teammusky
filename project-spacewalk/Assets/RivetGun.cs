using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;

public class RivetGun : MonoBehaviour
{

    public float MinimumDistanceBetweenRivets = 2f;

    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject rivetPrefab;

    public Action<List<String>> OnRivetPlaced;

    List<Transform> placedRivets;

    void Awake()
    {
        placedRivets = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlaceRivet();
        }
    }

    void PlaceRivet()
    {
        Vector2 position = muzzle.position;
        bool isValidPosition = true;
        foreach (var rivet in placedRivets)
        {
            if ((position - (Vector2)rivet.position).magnitude < MinimumDistanceBetweenRivets)
            {
                Debug.Log("Distance: " + (position - (Vector2)rivet.position).magnitude);
                isValidPosition = false;
            }
        }
        if (!isValidPosition)
        {
            Debug.Log("Too close!");
            return;
        }

        GameObject go = Instantiate(rivetPrefab, position, Quaternion.identity);
        placedRivets.Add(go.transform);
        Collider2D[] col = Physics2D.OverlapPointAll(position);
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
        var hitTypes = new List<String>();
        if (hitFrame)
        {
            hitTypes.Add("Frame");
        }
        if (hitOxygen)
        {
            hitTypes.Add("Pipe");
        }
        if (hitElectric)
        {
            hitTypes.Add("Wire");
        }
        if (hitTypes.Count > 0)
        {
            OnRivetPlaced?.Invoke(hitTypes);
        }
    }
}
