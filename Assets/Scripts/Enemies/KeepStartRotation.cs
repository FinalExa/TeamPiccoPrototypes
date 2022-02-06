using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepStartRotation : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;

    private void Update()
    {
        this.transform.rotation = Quaternion.Euler(rotation);
    }
}
