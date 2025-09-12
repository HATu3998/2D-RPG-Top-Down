using System.Collections;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Coroutine coroutine = StartCoroutine(RoamingRouting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private enum State
    {
        Roaming
    }
    private State state;
    private EnemyPathfiding enemyPathFinding;
    private void Awake()
    {

        enemyPathFinding = GetComponent<EnemyPathfiding>();
        state = State.Roaming;
    }
    private IEnumerator RoamingRouting()
    {
        while(state== State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
         
    }
    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    
}
