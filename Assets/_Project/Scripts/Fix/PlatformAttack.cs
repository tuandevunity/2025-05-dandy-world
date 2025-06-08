using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Preconsts.Tag_Player)) {
            other.gameObject.transform.parent = transform;
            Debug.Log("vao pl");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Preconsts.Tag_Player)) {
            other.gameObject.transform.parent = null;
            Debug.Log("ra pl");
        }
    }
    
}
