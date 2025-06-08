using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StepButton : TickBehaviour
{
    // Start is called before the first frame update
    private bool isOpen = false;
    [SerializeField] float duration = 1f;
    [SerializeField] Vector3 rotateTarget = Vector3.zero;
    [SerializeField] Transform[] pivots;
    [SerializeField] GameObject tutor;
    public Material[] newMaterials;
    void Start()
    {

    }

    public void HandleStep()
    {
        AudioManager _audio = AudioManager.Instance;
        _audio.SpawnAndPlay(transform, _audio.clickButton, 0.5f);
        GetComponent<MeshRenderer>().materials = newMaterials;

        if (isOpen) return;
        
        tutor.SetActive(false);
        _audio.SpawnAndPlay(pivots[0], _audio.openDoorSound, duration);
        for (int i = 0; i < pivots.Length; i++)
        {
            pivots[i].DORotateQuaternion(Quaternion.Euler(pivots[i].transform.eulerAngles + rotateTarget), duration);
        }
        
        isOpen = true;
    }

    // Update is called once per frame
}
