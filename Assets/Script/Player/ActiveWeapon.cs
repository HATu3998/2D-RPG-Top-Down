using UnityEngine;

public class ActiveWeapon : SingleTon<ActiveWeapon> 
{
   // [SerializeField] private MonoBehaviour currentActiveWeapon;
   public MonoBehaviour CurrentActiveWeapon  { get; private set; }
    private PlayController playController;
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
    }

    // Update is called once per frame
    void Update()
    {
        Attack(); 
    }
    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
    }
    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }
    public void ToggleIsAttacking(bool Value)
    {
        isAttacking = Value;
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
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack();
        }
        
    }
}
