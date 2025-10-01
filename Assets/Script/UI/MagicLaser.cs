using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    private bool isGrowing = true;
    [SerializeField] private float laserGrowTime = 2f;
    private float laserRange;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Indestructible>() && !other.isTrigger)
        {
            isGrowing = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LaserFaceMouse();
    }
    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }
    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;
        while(spriteRenderer.size.x < laserRange && isGrowing )
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;
            spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);
            boxCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), boxCollider.size.y);
            boxCollider.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, boxCollider.offset.y);
            yield return null;
        }
        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }
    private void LaserFaceMouse() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
