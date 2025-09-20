using UnityEngine;

public class AreaEnchance : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private string transittionName;

    void Start()
    {
        if(transittionName == SceneManagement.Instance.SceneTransitionName)
        {
            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            UIFade.Instance.FadeToClear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
