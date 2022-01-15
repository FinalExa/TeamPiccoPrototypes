using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP;
    [SerializeField] private bool isPlayer;
    private float currentHP;
    [SerializeField] private Text UIText;
    [SerializeField] private float invincibilityTime;
    private float invincibilityTimer;
    private bool invincibility;

    private void OnEnable()
    {
        currentHP = maxHP;
        invincibilityTimer = invincibilityTime;
        invincibility = false;
    }

    private void Update()
    {
        UpdateHPInUI();
    }

    private void FixedUpdate()
    {
        if (invincibility) InvincibilityTimer();
    }

    private void UpdateHPInUI()
    {
        UIText.text = currentHP + "/" + maxHP;
    }

    public void DecreaseHP(float damageReceived)
    {
        if (!invincibility)
        {
            currentHP = Mathf.Clamp(currentHP - damageReceived, 0, 100);
            if (currentHP == 0)
            {
                if (!isPlayer) this.gameObject.SetActive(false);
                else SceneManager.LoadScene(0);
            }
            else
            {
                if (isPlayer) invincibility = true;
            }
        }
    }

    public void InvincibilityTimer()
    {
        if (invincibilityTimer > 0) invincibilityTimer -= Time.fixedDeltaTime;
        else
        {
            invincibility = false;
            invincibilityTimer = invincibilityTime;
        }
    }

    public void IncreaseHP(float healReceived)
    {
        currentHP = Mathf.Clamp(currentHP + healReceived, 0, 100);
    }
}
