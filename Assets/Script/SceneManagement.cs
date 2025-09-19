using UnityEngine;

public class SceneManagement : SingleTon<SceneManagement> { 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string SceneTransitionName { get; private set; }
    public void SetTransitionName(string sceneTransitionName)
    {
        this.SceneTransitionName = sceneTransitionName;
    }
}
