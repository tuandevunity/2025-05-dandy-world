using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TriggerCloseDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] DoorBoss doorBoss;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag(Preconsts.Tag_Player)) {
            doorBoss.CloseDoor();
        }
    }
}
