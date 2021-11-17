using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerInputs>().gameObject;
    }

    private void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}
