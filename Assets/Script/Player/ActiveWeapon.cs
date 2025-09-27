using System.Collections;
using UnityEngine;

public class ActiveWeapon : SingleTon<ActiveWeapon> 
{
   // [SerializeField] private MonoBehaviour currentActiveWeapon;
   public MonoBehaviour CurrentActiveWeapon  { get; private set; }
    private PlayController playController;
    private float timeBetweenAttack; 
    private bool attackButtonDown, isAttacking = false;
    protected override void Awake()
    {
        base.Awake();
        playController = new PlayController();
    }
    private void OnEnable()
    {
        playController.Enable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playController.Combat.Attack.started += _ => StartAttacking();
        playController.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    // Update is called once per frame
    void Update()
    {
        Attack(); 
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        AttackCooldown();
        timeBetweenAttack = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
    //public void ToggleIsAttacking(bool Value)
    //{
    //    isAttacking = Value;
    //}

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine()); 
    }
  private  IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        isAttacking = false;
    }
    private void StartAttacking()
    {
        attackButtonDown = true;
    }
    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    private void Attack()
    {
        if (attackButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            //   isAttacking = true;
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
        
    }
}
