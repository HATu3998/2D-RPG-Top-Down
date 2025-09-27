using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefabs;
    [SerializeField] private Transform arrowSpawnPoint;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");
    private Animator myAnimator;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();

    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefabs, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        newArrow.GetComponent<Projecttile>().UpdateProjectileRange(weaponInfo.weaponRange);
        //Projecttile proj = newArrow.GetComponent<Projecttile>();
        //if (proj != null)
        //{
        //    proj.UpdateWeaponInfo(weaponInfo);
        //}
        // ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
