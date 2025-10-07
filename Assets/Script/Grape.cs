using UnityEngine;

public class Grape : MonoBehaviour , IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefabs;

    private Animator myAnimator;
    private SpriteRenderer spriteRendere;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        spriteRendere = GetComponent<SpriteRenderer>();
    }
    public void Attack()
    {
        myAnimator.SetTrigger(ATTACK_HASH);
        if(transform.position.x - PlayerController.Instance.transform.position.x  < 0)
        {
            spriteRendere.flipX = false;
        }
        else 
        {
            spriteRendere.flipX = true;
        }
    }
    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjectilePrefabs, transform.position, Quaternion.identity);
    }
}
