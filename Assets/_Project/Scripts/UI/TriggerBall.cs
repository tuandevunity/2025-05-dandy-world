using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject lava;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Preconsts.Tag_Player)) {
            lava.SetActive(true);
        }
    }
}
