using System.Collections;
using UnityEngine;

public class PlayerHeath : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockback;
    private Flash flash;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<KnockBack>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }
     
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAi enemyAi = other.gameObject.GetComponent<EnemyAi>();
        if (enemyAi && canTakeDamage)
        {
            TakeDamage(1);
            knockback.GetKnockBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());

        }
    }
    private void TakeDamage(int damageAmount)
    {
        canTakeDamage = false; 
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
