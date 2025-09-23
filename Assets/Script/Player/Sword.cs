using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour,IWeapon
{

    // private PlayController playerControls;

    //   [SerializeField] private PlayerController playerController;
    //  [SerializeField] private ActiveWeapon activeWeapon;
    // [SerializeField] private Transform weaponCollider;
     
    [SerializeField] private GameObject slashAminPrefab;
    [SerializeField] private Transform slashAminSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f;
    private GameObject slashAmin;
    private Animator myAnimator;
    private Transform weaponCollider;
    //  private bool attackButtonDown, isAttacking = false;

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAminSpawnPoint = GameObject.Find("SlashSpawnPoint").transform; 
    }
    private void Awake()
    {
     //   playerController = GetComponent<PlayerController>();
     //   activeWeapon = GetComponent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
  //      playerControls = new PlayController();
   
    }
    //private void OnEnable()
    //{
    //    playerControls.Enable();
    //}
    //void Start()
    //{
    //    playerControls.Combat.Attack.started += _ => StartAttacking();
    //    playerControls.Combat.Attack.canceled += _ => StopAttacking();
    //}
    //private void StartAttacking()
    //{
    //    attackButtonDown = true;
    //}
    //private void StopAttacking()
    //{
    //    attackButtonDown = false;
    //}
    // Update is called once per frame
    //private void Attack()
    //{
    //     if(attackButtonDown && !isAttacking)
    //    {
    //        isAttacking = true;
    //        myAnimator.SetTrigger("Attack");
    //        weaponCollider.gameObject.SetActive(true);
    //        slashAmin = Instantiate(slashAminPrefab, slashAminSpawnPoint.position, Quaternion.identity);
    //        slashAmin.transform.parent = this.transform.parent;
    //        StartCoroutine(AttackCDRoutine());
    //    }
    //}

    public void Attack()
    {
        myAnimator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slashAmin = Instantiate(slashAminPrefab, slashAminSpawnPoint.position, Quaternion.identity);
        slashAmin.transform.parent = this.transform.parent;
        StartCoroutine(AttackCDRoutine());

    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        // isAttacking = false;   
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    public void swingUpFlipAnim()
    {
        slashAmin.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        // if (playerController.FacingLeft)
        if (PlayerController.Instance.FacingLeft)
        {
            slashAmin.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void swingDownFlipAnim()
    {
        slashAmin.gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0);
        // if (playerController.FacingLeft)
        if (PlayerController.Instance.FacingLeft) 
        {
            slashAmin.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    void Update()
    {
        MouseFollowWithOffSet();
       // Attack();
    }
    private void MouseFollowWithOffSet()
    {
        Vector3 mousePos = Input.mousePosition;
        //  Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (mousePos.x < playerScreenPoint.x)
        {
          ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0 , 0);
        }


    }
}
