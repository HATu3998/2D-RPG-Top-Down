using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    private int currentHealth;
    private KnockBack knockBack;
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
         
        currentHealth = startingHealth;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockBack.GetKnockBack(PlayerController.Instance.transform, 15f);
        DetectDeath();
    }
    private void DetectDeath()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
