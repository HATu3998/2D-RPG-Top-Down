using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;
    private bool facingLeft = false;
    private bool isDashing = false; 

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;
    private PlayController playerControl;
    private Vector2 movement;
    private Rigidbody2D rb;

    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private void Awake()
    {
        Instance = this;
        playerControl = new PlayController();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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
        playerControl.Enable();

        playerControl.Combat.Dash.performed += OnDashPerformed;
    }
    private void OnDisable()
    {
        playerControl.Combat.Dash.performed -= OnDashPerformed;
        playerControl.Disable();
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
    private void PlayerInput()
    {
        movement = playerControl.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);

    }
    private void  Move()    
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.deltaTime));
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false; 
        }
    }
}
