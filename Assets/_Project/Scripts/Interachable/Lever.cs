using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Lever : TickBehaviour
{
    // Start is called before the first frame update

    private Quaternion openRotation;
    [SerializeField] float duration = 0.8f;
    [SerializeField] Transform lever;
    [SerializeField] Vector3 rotateTarget = Vector3.zero;

    [SerializeField] Transform moveTarget;
    [SerializeField] Transform movePlatform;
    private Vector3 platformOrig;
    private Quaternion quaternionOrig;

    private Tween moveTween;
    private Tween rotateTween;

 
    void Start()
    {
        quaternionOrig = lever.rotation;
        platformOrig = movePlatform.position;
        openRotation = Quaternion.Euler(lever.transform.eulerAngles + rotateTarget);
    }
    public void HandleLever()
    {
        rotateTween = lever.DORotateQuaternion(openRotation, duration).
            OnComplete(
                ()=> {
                   moveTween = movePlatform.DOLocalMove(moveTarget.position, 10f).
                    SetEase(Ease.Linear);
            });
    }

    public void Reset()
    {
        rotateTween?.Kill();
        moveTween?.Kill();
        movePlatform.position = platformOrig;
        lever.rotation = quaternionOrig;   
    }
}
