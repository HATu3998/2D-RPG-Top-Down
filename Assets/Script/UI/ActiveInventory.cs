using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayController playController;
    private void Awake()
    {
        playController = new PlayController();

    }
    private void Start()
    {
        playController.Inventory.KeyBoard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHightight(0);
        
    }
    private void OnEnable()
    {
        playController.Enable();
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHightight(numValue - 1);
    }
    private void ToggleActiveHightight(int indexNum)
    {
        activeSlotIndexNum = indexNum;
        foreach(Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon()
    {
       if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }
        // if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        //if(transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo()==null)
        // {
        //     ActiveWeapon.Instance.WeaponNull();
        //     return;
        // }
        //GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).
        //    GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        if(weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
             
    }
}
