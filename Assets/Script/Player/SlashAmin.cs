using UnityEngine;

public class SlashAmin : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private ParticleSystem ps;
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();

    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ps && !ps.IsAlive())
        {
            DestroySelf();
        }
    }
}
