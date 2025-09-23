using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        Debug.Log("Staff");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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
