using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
[DefaultExecutionOrder(-100)]
public class PlayerController : SingleTon<PlayerController>
{
    public static PlayerController Instance;
    public static PlayController playerControl;
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
     private bool facingLeft = false;
    private bool isDashing = false;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    
    private Vector2 movement;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    protected override void Awake()
    {
        base.Awake();
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (playerControl == null)
        { playerControl = new PlayController();
              }
        playerControl.Enable();
         

        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();

      
    }
    private void Start()
    {
        playerControl.Combat.Dash.performed += _ => Dash();
    }
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCD = 0.25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed /= dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnEnable()
    {
        if (playerControl == null)
            playerControl = new PlayController();


        playerControl.Enable();
        playerControl.Combat.Dash.performed += OnDashPerformed;
    }
    private void OnDisable()
    {
        if (playerControl != null)
        {
            playerControl.Combat.Dash.performed -= OnDashPerformed;
            playerControl.Disable();
        }
    }
 
     
    private void OnDashPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Dash Pressed!");
        Dash();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {


        movement = playerControl.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);


    }
    private void Move()
    {
        if (knockBack.gettingKnockBack) { return; }
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (playerControl != null)
        {
            playerControl.Enable();
            Debug.Log($"PlayerController re-enabled inputs on scene load (scene {level})");
        }
    }
}
