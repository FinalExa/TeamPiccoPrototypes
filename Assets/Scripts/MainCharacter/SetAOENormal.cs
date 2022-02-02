using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAOENormal : MonoBehaviour
{
    private PlayerReferences myPlayer;
    public Weapon MyAoe;
    public float ColliderAoe;
    void Start()
    {
        myPlayer = FindObjectOfType<PlayerReferences>();
        MyAoe.projectileAoe = ColliderAoe;

    }

    public void Update()
    {
        if (myPlayer.playerInputs.LeftClickInput == false)
        {
            MyAoe.projectileAoe = ColliderAoe;
        }
    }
}
