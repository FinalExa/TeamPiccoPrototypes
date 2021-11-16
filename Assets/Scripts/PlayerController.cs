using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerReferences playerReferences;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public string curState;

    private void Awake()
    {
        playerReferences = this.gameObject.GetComponent<PlayerReferences>();
    }
}
