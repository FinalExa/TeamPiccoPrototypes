using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHP;
    public bool isPlayer;
    private float currentHP;
    [SerializeField] private Text UIText;
    public float invincibilityTime;
    private float invincibilityTimer;
    private bool invincibility;
    [SerializeField] private GameObject dropHealth;
    [SerializeField] private GameObject dropAmmo;
    [SerializeField] private float dropChance;
    private EnemyPattern thisEnemyPattern;
    private bool godMode;

    private void Start()
    {
        if (!isPlayer) thisEnemyPattern = this.gameObject.GetComponent<EnemyPattern>();
    }

    private void OnEnable()
    {
        godMode = false;
        currentHP = maxHP;
        invincibilityTimer = invincibilityTime;
        invincibility = false;
    }

    private void Update()
    {
        UpdateHPInUI();
        if (Input.GetKeyDown(KeyCode.G) && isPlayer) godMode = !godMode;
    }

    private void FixedUpdate()
    {
        if (invincibility) InvincibilityTimer();
    }

    private void UpdateHPInUI()
    {
        if (!godMode) UIText.text = "HP: " + System.Math.Round(currentHP, 2) + "/" + maxHP;
        else UIText.text = "O M G !";
    }

    public void DecreaseHP(float damageReceived)
    {
        if (!godMode && !invincibility)
        {
            currentHP = Mathf.Clamp(currentHP - damageReceived, 0, 100);
            if (!isPlayer)
            {
                thisEnemyPattern.alerted = true;
            }
            if (currentHP == 0)
            {
                if (!isPlayer)
                {
                    SpawnDrop();
                    this.gameObject.SetActive(false);
                }
                else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        currentHP = Mathf.Clamp(currentHP + healReceived, 0, maxHP);
    }

    public void FullyHeal()
    {
        currentHP = maxHP;
    }

    private void SpawnDrop()
    {
        if (Random.Range(0, 99) < dropChance)
        {
            if (Random.Range(0, 2) == 0) Instantiate(dropHealth, this.gameObject.transform.position, Quaternion.identity);
            else Instantiate(dropAmmo, this.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPlayer && other.gameObject.CompareTag("Projectile"))
        {
            DecreaseHP(other.gameObject.GetComponent<Projectile>().damage);
            this.gameObject.GetComponent<PlayerReferences>().damageTaken.TakeDamage();
            other.gameObject.GetComponent<Projectile>().DestroyProjectile();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Laser"))
        {
            if (isPlayer) DecreaseHP(other.GetComponentInParent<LaserOptions>().laserDamageToPlayer);
            else DecreaseHP(other.GetComponentInParent<LaserOptions>().laserDamageToEnemies * Time.deltaTime);
        }
    }
}
