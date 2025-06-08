using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wheel : TickBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject door;
    [SerializeField] Transform pivot;
    private bool isOpen = false;
    [SerializeField] float duration = 0.5f;
    private Quaternion openRotation;

    [SerializeField] Vector3 rotateTarget = Vector3.zero;
    void Start()
    {
        openRotation = Quaternion.Euler(pivot.transform.eulerAngles + rotateTarget);
    }

    public void HandleWheel() {
        if (isOpen) return;
        isOpen = true;
        door.SetActive(false);
        pivot.DORotateQuaternion(openRotation, duration);
    }
    // Update is called once per frame
    
}
