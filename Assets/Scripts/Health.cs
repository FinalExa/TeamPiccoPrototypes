using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP;
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
        if (currentHP == 0) this.gameObject.SetActive(false);
    }

    public void IncreaseHP(float healReceived)
    {
        currentHP = Mathf.Clamp(currentHP + healReceived, 0, 100);
    }
}
