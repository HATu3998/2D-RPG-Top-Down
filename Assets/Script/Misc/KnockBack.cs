using System.Collections;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockBack { get; private set; }
    [SerializeField] private float knockBackTime = 2f;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetKnockBack(Transform damageSource,float knockBackThrust)
    {
        gettingKnockBack = true;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(knockRouTime());
    }
    private IEnumerator knockRouTime()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.linearVelocity = Vector2.zero;
        gettingKnockBack = false;
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
