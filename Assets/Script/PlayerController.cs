using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;
    private bool facingLeft = false;

    [SerializeField] private float moveSpeed = 1f;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void OnEnable()
    {
        playerControl.Enable();
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
