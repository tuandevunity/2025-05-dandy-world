using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Door : TickBehaviour
{
    [Header("SETTING DOOR")]
    [SerializeField] Vector3 RotationOffset;
    [SerializeField] float duration;
    [SerializeField] GameObject tutor;
    private bool isOpen;   
    public void HandleDoor() {
        AudioManager _audio = AudioManager.Instance;
        _audio.SpawnAndPlay(transform, _audio.openDoorSound, duration);
        if (isOpen) return;
        Transform pivot = transform.parent;
        
        if (tutor == null) {
            Debug.LogError("Tutor is not attached on script!");
            return;
        }
        if (pivot == null) {
            Debug.LogError("Pivot is not attached on door!");
            return;
        }

        tutor.SetActive(false);
        isOpen = true;
        Quaternion openRotation = Quaternion.Euler(pivot.eulerAngles + RotationOffset);
        pivot.DORotateQuaternion(openRotation, duration);
    }  

}


