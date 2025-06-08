using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(BoxCollider))]
public class Shovel : TickBehaviour
{
    // Start is called before the first frame update
    PlayerMovement player;
    Mine[] mines;
    [SerializeField] GameObject tutor;
    bool attached = false;
    void Start()
    {
        
    }

    void Update()
    {

        if (!FindFirstObjectByType<Mine>()) Destroy(gameObject);
    }

    public void HandleShovel()
    {
        if (attached) return;
        attached = true;
        tutor.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        SetPosShovel();
        SetAllCanDig();
    }
    // Update is called once per frame
    void SetPosShovel() {
        player = FindFirstObjectByType<PlayerMovement>();
        transform.SetParent(player.posShovel.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(-19.004f,149.895f,102.593f);
        
    }

    void SetAllCanDig() {
        mines = FindObjectsOfType<Mine>();
            for (int i = 0; i< mines.Length; i++) {
                mines[i].SetCanDestroy();
            } 
    }
}
