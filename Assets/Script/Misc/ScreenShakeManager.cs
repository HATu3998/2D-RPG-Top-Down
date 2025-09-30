using Unity.Cinemachine;
using UnityEngine;
 
public class ScreenShakeManager : SingleTon<ScreenShakeManager>
{
    private CinemachineImpulseSource source;

    protected override void Awake()
    {
        base.Awake();
        source = GetComponent<CinemachineImpulseSource>();

    }
    public void ShakeScreen()
    {
        source.GenerateImpulse();
    }
}
