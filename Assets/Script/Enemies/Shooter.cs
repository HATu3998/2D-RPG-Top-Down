using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour,IEnemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projecttilesPerBurst;
    [SerializeField] [Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;

    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [SerializeField] private bool oscillate;
    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projecttilesPerBurst < 1) { projecttilesPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if(timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if(restTime < 0.1f) { restTime = 0.1f; }
        if(startingDistance < 0.1f) { startingDistance = 0.1f; }
        if(angleSpread ==0) { projecttilesPerBurst = 1; }
        if(bulletMoveSpeed <= 0) { bulletMoveSpeed = 0.1f; }


    }

    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(ShootRoutine());
  
        }
    }
    //ban 5 phat
    //private IEnumerator ShootRoutine()
    //{
    //    isShooting = true;
    //    for(int i=0; i< burstCount; i++)
    //    {
    //        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
    //        GameObject newBullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity);
    //        newBullet.transform.right = targetDirection;

    //        if(newBullet.TryGetComponent(out Projecttile projecttile))
    //        {
    //            projecttile.UpdateMoveSpeed(bulletMoveSpeed);
                
    //        }
    //        yield return new WaitForSeconds(timeBetweenBursts);
    //    }
    //    yield return new WaitForSeconds(restTime);
    //    isShooting = false;
    //}

    //ban vong tron
    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;
        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
        if (stagger) { timeBetweenProjectiles = timeBetweenBursts / projecttilesPerBurst; }
        for(int i=0;i< burstCount; i++)
        {
            if (!oscillate)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            if (oscillate && i % 2 != 1)
            {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

            }else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1; 
            }

                for (int j = 0; j < projecttilesPerBurst; j++)
                {
                    Vector2 pos = FindBulletSpawnPos(currentAngle);
                    GameObject newBullet = Instantiate(bulletPrefabs, pos, Quaternion.identity);
                    newBullet.transform.right = newBullet.transform.position - transform.position;
                    if (newBullet.TryGetComponent(out Projecttile projecttile))
                    {
                        projecttile.UpdateMoveSpeed(bulletMoveSpeed);
                    }
                    currentAngle += angleStep;
                if (stagger) { yield return new WaitForSeconds(timeBetweenProjectiles); }
                }
            currentAngle = startAngle;
            if (!stagger) { yield return new WaitForSeconds(timeBetweenBursts); }
        }
        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }
    private void TargetConeOfInfluence(out float startAngle,out float currentAngle,out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
         endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0;
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projecttilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }
    private Vector2 FindBulletSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
