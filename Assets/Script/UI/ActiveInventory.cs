using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;
    private PlayController playController;
    private void Awake()
    {
        playController = PlayerController.playerControl;

    }
    private void Start()
    { 
        if (playController == null)
        {
            playController = PlayerController.playerControl;
            if (playController == null)
            {
                Debug.LogError("PlayController v?n NULL trong ActiveInventory.Start()");
                return;
            }
        }
        playController.Inventory.KeyBoard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        ToggleActiveHightight(0);

    }
    private void Update()
    {
        if (playController != null && !playController.asset.enabled)
            Debug.LogWarning("Player input system is currently DISABLED!");
    }
    private void OnEnable()
    {
        if (playController == null)
        {
            playController = PlayerController.playerControl;

            if (playController == null)
            {
                Debug.LogError("PlayController is null in ActiveInventory.OnEnable()");
                return;
            }
        }
    }
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHightight(numValue - 1);
    }
    private void ToggleActiveHightight(int indexNum)
    {
        //activeSlotIndexNum = indexNum;
        //foreach(Transform inventorySlot in this.transform)
        //{
        //    inventorySlot.GetChild(0).gameObject.SetActive(false);
        //}
        //this.transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        //ChangeActiveWeapon();

        if (indexNum < 0 || indexNum >= transform.childCount)
        {
            Debug.LogWarning($"Slot {indexNum} kh?ng t?n t?i trong Inventory!");
            return;
        }

        activeSlotIndexNum = indexNum;

        // t?t h?t highlight
        foreach (Transform inventorySlot in this.transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        // b?t highlight slot ?ang ch?n
        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }
    //private void ChangeActiveWeapon()
    //{
    //   if(ActiveWeapon.Instance.CurrentActiveWeapon != null)
    //    {
    //        Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
    //    }
    //    // if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
    //    //if(transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo()==null)
    //    // {
    //    //     ActiveWeapon.Instance.WeaponNull();
    //    //     return;
    //    // }
    //    //GameObject weaponToSpawn = transform.GetChild(activeSlotIndexNum).
    //    //    GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;

    //    Transform childTransform = transform.GetChild(activeSlotIndexNum);
    //    InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();
    //    WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
    //    GameObject weaponToSpawn = weaponInfo.weaponPrefab;
    //    if(weaponInfo == null)
    //    {
    //        ActiveWeapon.Instance.WeaponNull();
    //        return;
    //    }

    //    GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
    //    ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
    //    newWeapon.transform.parent = ActiveWeapon.Instance.transform;
    //    ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

    //}
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum);
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>();

        // ? Check null tr??c
        if (inventorySlot == null)
        {
            Debug.LogWarning($"Slot {activeSlotIndexNum} kh?ng c? InventorySlot component.");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo();
        if (weaponInfo == null || weaponInfo.weaponPrefab == null)
        {
            Debug.LogWarning($"Slot {activeSlotIndexNum} r?ng ho?c ch?a g?n weaponPrefab.");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject weaponToSpawn = weaponInfo.weaponPrefab;
        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());

    }
}
