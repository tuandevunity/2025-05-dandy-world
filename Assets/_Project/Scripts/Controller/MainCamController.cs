using Cinemachine;
using UnityEngine;

public class MainCamController : TickBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCameraFollowPlayer;
    CinemachineBrain cinemachineBrain;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        cinemachineBrain = GetComponent<CinemachineBrain>();
    }
    public void SetTimeChangeCam(float timeChangeCam)
    {
        cinemachineBrain.m_DefaultBlend.m_Time = timeChangeCam;
    }
    public void SetFollowPlayer(Transform targetFollow)
    {
        virtualCameraFollowPlayer.Follow = targetFollow;
    }
    public void SetFollowAndTimeChangeCam(Transform targetFollow, float timeChangeCam)
    {
        SetFollowPlayer(targetFollow);
        SetTimeChangeCam(timeChangeCam);
    }
}
