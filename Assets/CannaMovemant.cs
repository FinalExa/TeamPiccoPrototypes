using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannaMovemant : MonoBehaviour
{

    public GameObject Barrel;
    public GameObject CrossHair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //Vector3 aimDirection = CrossHair.transform.position - Barrel.transform.position;
       //Barrel.transform.rotation = Quaternion.LookRotation(aimDirection);
    }
}
