using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAbility : MonoBehaviour
{
    public SphereCollider myCollider;
    public float ColliderAoe, ColliderAoePlus;
    public Weapon MyAoe;
    private PlayerReferences myPlayer;
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerReferences>();
        MyAoe.projectileAoe = ColliderAoe;
        myCollider.GetComponent<SphereCollider>();
    }

    public void Update()
    {
        if (myPlayer.playerInputs.LeftClickInput == false)
        {
            MyAoe.projectileAoe = ColliderAoe;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ProjectilePlayer") && myPlayer.playerInputs.LeftClickInput == true)
        {
            MyAoe.projectileAoe = ColliderAoePlus;
        }

    }
}
