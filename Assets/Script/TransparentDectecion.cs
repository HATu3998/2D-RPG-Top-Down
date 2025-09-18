using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TransparentDectecion : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float fadeTime = .4f;
    private SpriteRenderer spriteRenderer;
    private Tilemap tilmap;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilmap = GetComponent<Tilemap>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));

            }else if (tilmap)
            {
                StartCoroutine(FadeRoutine(tilmap, fadeTime, tilmap.color.a, transparencyAmount));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (spriteRenderer)
            {
                StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a,1f));

            }
            else if (tilmap)
            {
                StartCoroutine(FadeRoutine(tilmap, fadeTime, tilmap.color.a, 1f));
            }
        }

    }
   private IEnumerator FadeRoutine(SpriteRenderer spriteRendere, float fadeTime, float startValue,float targetTransparency)
    {
        float elapsedTime = 0;
        while(elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRendere.color = new Color(spriteRendere.color.r, spriteRendere.color.g, spriteRendere.color.b, newAlpha);
            yield return null;
        }
    }
    private IEnumerator FadeRoutine(Tilemap tile, float fadeTime, float startValue, float targetTransparency)
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tile.color = new Color(tile.color.r, tile.color.g, tile.color.b, newAlpha);
            yield return null;
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
