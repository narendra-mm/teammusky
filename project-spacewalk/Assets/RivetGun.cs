using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivetGun : MonoBehaviour
{

    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject rivetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            PlaceRivet();
        }
    }

    void PlaceRivet() {
        Instantiate(rivetPrefab, muzzle.position, Quaternion.identity);
    }
}
