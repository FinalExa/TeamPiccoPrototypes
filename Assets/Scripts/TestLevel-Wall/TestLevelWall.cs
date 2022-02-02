using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelWall : MonoBehaviour
{
    public GameObject roomToActive, room2ToDeactive, triggetToActive;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomToActive.SetActive(true);
            room2ToDeactive.SetActive(false);
            triggetToActive.SetActive(true);
            this.gameObject.SetActive(false);
        }

    }
}
