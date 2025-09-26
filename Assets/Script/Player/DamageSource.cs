using UnityEngine;

public class DamageSource : MonoBehaviour
{
    //[SerializeField] private int damgeAmount = 1;
    private int damgeAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MonoBehaviour currentActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damgeAmount = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damgeAmount);
        }
    }
}
