using UnityEngine;

public class Shooter : MonoBehaviour,IEnemy
{
    [SerializeField] private GameObject bulletPrefabs;
    public void Attack()
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        GameObject newBullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
        newBullet.transform.right = targetDirection;
    }
}
