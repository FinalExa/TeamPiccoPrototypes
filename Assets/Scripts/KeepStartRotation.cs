using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepStartRotation : MonoBehaviour
{
    private Quaternion rotation;

    private void Awake()
    {
        rotation = this.transform.rotation;
    }

    private void Update()
    {
        this.transform.rotation = rotation;
    }
}
