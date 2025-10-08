using UnityEngine;

public class GrapeLandSplatter : MonoBehaviour
{
    private SpriteFade spriteFade;
    private void Awake()
    {
        spriteFade = GetComponent<SpriteFade>();
    }
    private void Start()
    {
        StartCoroutine(spriteFade.SlowFadeRoutine());
        Invoke("DisableCollider", 0.2f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerHeath playerHeath = other.gameObject.GetComponent<PlayerHeath>();
        playerHeath?.TakeDamage(1, transform);

    }
    private void DisableCollider()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
