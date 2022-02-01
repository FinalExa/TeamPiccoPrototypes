using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject[] positions;
    private PlayerReferences playerRef;

    private void Awake()
    {
        playerRef = FindObjectOfType<PlayerReferences>();
    }

    private void Update()
    {
        TeleportKeys();
    }

    private void TeleportKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TeleportPlayer(positions[0].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TeleportPlayer(positions[1].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TeleportPlayer(positions[2].transform.position);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TeleportPlayer(positions[3].transform.position);
        }
    }

    private void TeleportPlayer(Vector3 positionToTeleport)
    {
        playerRef.gameObject.transform.position = positionToTeleport;
    }
}
