using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerTrap : MonoBehaviour
{
    [SerializeField] private GameObject Martello;
    public float HammerDamageToPlayer;
    public float HammerDamageToEnemies;
    public BoxCollider ActivationRange;
    public GameObject CorpoMartello;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            HammerDown();
        }
        else if (other.gameObject.tag == "Enemy")
        {
            HammerDown();
        }
    }

    private void HammerDown()
    {
        CorpoMartello.transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
        Debug.Log("HAMMER TIME");
    }
}
