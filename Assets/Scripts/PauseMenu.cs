using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{
    public GameObject PauseMenuPanel, Player;
    public bool IsStopped;
    public PlayerData playerData;
    public Text MovementSpeed,MeleeColldown,MeleeDuration;


    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsStopped == false)
            {
                Time.timeScale = 0;
                PauseMenuPanel.SetActive(true);
                Player.SetActive(false);
                IsStopped = true;
            }
            else
            {
                ResumeGame();
            }
        }
        MovementSpeed.text = playerData.defaultMovementSpeed.ToString();
        MeleeColldown.text = playerData.meleeCooldown.ToString();
        MeleeDuration.text = playerData.meleeDuration.ToString();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        PauseMenuPanel.SetActive(false);
        Player.SetActive(true);
        IsStopped = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void increaseSpeed()
    {
        playerData.defaultMovementSpeed += 1f;
    }

    public void speedDecrease()
    {
        playerData.defaultMovementSpeed -= 1f;
    }


    public void increaseMeleeCooldown()
    {
        playerData.meleeCooldown += 0.1f;
    }

    public void MeleeCooldownDecrease()
    {
        playerData.meleeCooldown -= 0.1f;
    }

    public void increaseMeleeDuration()
    {
        playerData.meleeDuration += 0.1f;
    }

    public void MeleeDurationDecrease()
    {
        playerData.meleeDuration -= 0.1f;
    }
}
