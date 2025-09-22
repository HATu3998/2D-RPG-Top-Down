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
    }
}
