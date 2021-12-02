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
    [SerializeField] private Text UItext;

    private void OnEnable()
    {
        currentHP = maxHP;
    }

    private void Update()
    {
        UpdateHPInUI();
    }

    private void UpdateHPInUI()
    {
        UItext.text = currentHP + "/" + maxHP;
    }

    public void DecreaseHP(float damageReceived)
    {
        currentHP = Mathf.Clamp(currentHP - damageReceived, 0, 100);
        if (currentHP == 0)
        {
            if (!isPlayer) this.gameObject.SetActive(false);
            else SceneManager.LoadScene(0);
        }
    }

    public void IncreaseHP(float healReceived)
    {
        currentHP = Mathf.Clamp(currentHP + healReceived, 0, 100);
    }
}
