using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelWall : MonoBehaviour
{
    public GameObject roomToActive, room2ToDeactive, triggetToActive, Image1, Image2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomToActive.SetActive(true);
            Image1.SetActive(true);
            room2ToDeactive.SetActive(false);
            Image2.SetActive(false);
            triggetToActive.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
