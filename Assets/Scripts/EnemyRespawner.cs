using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private DamageEnemy[] enemies;

    private void Awake()
    {
        enemies = FindObjectsOfType<DamageEnemy>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) ReactivateEnemies();
    }

    private void ReactivateEnemies()
    {
        foreach (DamageEnemy enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }
}
