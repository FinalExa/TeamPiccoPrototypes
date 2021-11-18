using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour

{
    public GameObject PauseMenuPanel, Player;
    public bool IsStopped;


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
}
