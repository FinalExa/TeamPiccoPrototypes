using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLevelWall : MonoBehaviour
{
    public GameObject room1, room2;

    private void OnTriggerEnter(Collider other)
    {
        if (room2.activeSelf == true && room1.activeSelf == true && other.gameObject.tag == "Player")
        {
            room1.SetActive(false);
            room2.SetActive(true);
        }

        if (room1.activeSelf && room2.activeSelf == true && other.gameObject.tag == "Player")
        {
            room1.SetActive(true);
            room2.SetActive(false);
        }

        //if (room2.activeSelf == false && other.gameObject.tag == "Player")
        //{
        //    room1.SetActive(false);
        //    room2.SetActive(true);
        //}
    }
}
