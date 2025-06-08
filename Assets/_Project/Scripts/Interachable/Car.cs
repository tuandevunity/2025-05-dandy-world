using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    [SerializeField] float timeDelay;
    [SerializeField] Transform seats;
    bool _isTrigger;
    void Start()
    {
        
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other) {
        if (_isTrigger == true) return;
        _isTrigger = true;
        if (other.gameObject.CompareTag(Preconsts.Tag_Player)) {
            other.gameObject.GetComponent<PlayerMovement>().SetDrive(true);
            other.transform.parent = seats;
            other.transform.localPosition = Vector3.zero;
            transform.DOLocalMove(target.position, timeDelay).
                SetEase(Ease.InOutCubic).
                OnComplete(
                    () => {
                        other.transform.localPosition += new Vector3 (0, 0, 4);
                        other.gameObject.GetComponent<PlayerMovement>().SetDrive(false);
                    }
                );
        }
    }
}
