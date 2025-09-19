using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SingleTon<T> : MonoBehaviour where T :SingleTon<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }
     protected virtual void Awake()
    {
        if(instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = (T)this;
        }
        DontDestroyOnLoad(gameObject);
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
