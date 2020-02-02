﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections.Generic;
using SpaceWalk.GameLogic;

public class RivetGun : MonoBehaviour
{

    public float MinimumDistanceBetweenRivets = 1.5f;

    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject rivetPrefab;
    [SerializeField] private GameObject gassPrefab;

    public Action<List<ShipMaterial>> OnRivetPlaced;
    public Action<int> OnCorrectRivetPlaced;

    List<Transform> correctRivets;
    List<Transform> allRivets;

    public bool EnableInputs = false;

    public void Init() {
        if(allRivets != null) {
            foreach(var rivet in allRivets) {
                Destroy(rivet);
            }
        }
        correctRivets = new List<Transform>();
        allRivets = new List<Transform>();
    }

    void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EnableInputs) {
            return;
        }

        var grabbableItem = GetComponent<GrabbableItem>();
        if (Input.GetKeyDown("space") && grabbableItem != null && grabbableItem.IsGrabbed())
        {
            PlaceRivet();
        }
    }

    void PlaceRivet()
    {
        Vector2 position = muzzle.position;
        bool isValidPosition = true;
        foreach (var rivet in correctRivets)
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
        Collider2D[] col = Physics2D.OverlapPointAll(position);
        allRivets.Add(go.transform);
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
        if(hitOxygen || hitElectric || !hitFrame) {
            // Wrong Rivet
            go.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else {
            // Correctly placed rivet
            correctRivets.Add(go.transform);
            OnCorrectRivetPlaced(correctRivets.Count);
        }
        var hitTypes = new List<ShipMaterial>();

        if (hitOxygen)
        {
            hitTypes.Add(ShipMaterial.Pipe);
            GameObject gass = Instantiate(gassPrefab, position, Quaternion.identity);
            gass.transform.eulerAngles = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f,20f), 0f);
        }
        if (hitElectric)
        {
            hitTypes.Add(ShipMaterial.Wire);
        }
        if (hitTypes.Count > 0)
        {
            OnRivetPlaced?.Invoke(hitTypes);
        }
    }
}
