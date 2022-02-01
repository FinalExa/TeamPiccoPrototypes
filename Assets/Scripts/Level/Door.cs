using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToKill;
    public GameObject actualMission;
    public GameObject nextMission;

    private void Update()
    {
        if (enemiesToKill.Length > 0) CheckEnemies();
    }

    private void CheckEnemies()
    {
        bool check = false;
        for (int i = 0; i < enemiesToKill.Length; i++)
        {
            if (enemiesToKill[i].activeSelf) break;
            if (!enemiesToKill[i].activeSelf && i == enemiesToKill.Length - 1) check = true;
        }
        if (check)
        {
            this.gameObject.SetActive(false);
            actualMission.SetActive(false);
            nextMission.SetActive(true);
        }
    }
}
