using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goinCoinPrefabs;
    public void DropItems()
    {
        Instantiate(goinCoinPrefabs, transform.position, Quaternion.identity);
    }
}
