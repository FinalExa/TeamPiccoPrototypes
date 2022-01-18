using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour

{
    public GameObject PauseMenuPanel, Player;
    public bool IsStopped;
    public PlayerData playerData;
    public Text MovementSpeed;

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

    public void IncreaseSpeed()
    {
        playerData.defaultMovementSpeed += 1f;
    }

    public void SpeedDecrease()
    {
        playerData.defaultMovementSpeed -= 1f;
    }
}
