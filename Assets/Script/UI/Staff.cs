using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaser;
    [SerializeField] private Transform magicLaserSpawnPoint;
    private Animator myAnimator;
    readonly int AttackHash = Animator.StringToHash("Attack");
    private void Awake()
    {
        myAnimator = GetComponent<Animator>(); 
    }
    public void Attack()
    {

        //  ActiveWeapon.Instance.ToggleIsAttacking(false);
        myAnimator.SetTrigger(AttackHash);
    }
    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newLaser = Instantiate(magicLaser, magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
  
     
    void Start()
    {
        
    }

    // Update is called once per frame
   private  void Update()
    {
        MouseFollowWithOffset();
    }
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if(mousePos.x< playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);

        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

    }
}
