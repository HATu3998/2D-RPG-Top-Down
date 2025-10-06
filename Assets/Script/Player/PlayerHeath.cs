using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHeath : SingleTon<PlayerHeath>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockback;
    private Flash flash;
    const string HEALTH_SLIDER_TEXT = "Health Slider";

    protected override void Awake()
    {
        base.Awake();
   
        flash = GetComponent<Flash>();
        knockback = GetComponent<KnockBack>();
        
    }
    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
     
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAi enemyAi = other.gameObject.GetComponent<EnemyAi>();
        if (enemyAi) {
            TakeDamage(1, other.transform);
        }
    }
 public  void TakeDamage(int damageAmount, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }
         
        ScreenShakeManager.Instance.ShakeScreen();

        knockback.GetKnockBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());

        canTakeDamage = false; 
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }
    private void CheckIfPlayerDeath()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player die");
        }
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    // Update is called once per frame
    private void UpdateHealthSlider()
    {
        if(healthSlider == null)
        {
            // healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
            healthSlider = GameObject.Find(HEALTH_SLIDER_TEXT).GetComponent<Slider>();

        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
}
