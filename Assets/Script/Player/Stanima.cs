using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Stanima : SingleTon<Stanima>
{
    public int CurrentStanima { get; private set; }
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private int timeBetweenStaminaRefresh = 3;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;
    const string STAMINA_CONTAINER_TEXT = "Stamina Container";

  protected override void  Awake()
    {
        base.Awake();

        maxStamina = startingStamina;
        CurrentStanima = startingStamina;

    }
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void UseStamina()
    {
        CurrentStanima--;
        UpdateStaminaImages();
    }
    public void RefreshStamima()
    {
        if(CurrentStanima < maxStamina)
        {
            CurrentStanima++;
        }
        UpdateStaminaImages();
    }
    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
           
            RefreshStamima();
        }
    }
    private void UpdateStaminaImages()
    {
        for(int i=0;i < maxStamina; i++)
        {
            if(i <= CurrentStanima - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }

        }
        if(CurrentStanima < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
