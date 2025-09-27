using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = 2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHeath;

    private void Awake()
    {
        enemyHeath = GetComponent<EnemyHealth>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;

    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        spriteRenderer.material = defaultMat;
        if (spriteRenderer != null)
            spriteRenderer.material = defaultMat;

        if (enemyHeath != null)
        {
            enemyHeath.DetectDeath();
        }
        else
        {
            Debug.LogWarning("EnemyHealth b? null khi g?i FlashRoutine tr?n " + gameObject.name);
        }
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
