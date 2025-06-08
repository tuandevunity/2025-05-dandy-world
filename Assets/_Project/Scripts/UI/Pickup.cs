using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerMovement player;
    [SerializeField] float boostSpeed = 5f;
    private bool picked;
    public void HandlePickup()
    {
        if (picked == true) return;
        picked = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.BoostSpeed(boostSpeed);
        Destroy(gameObject);
    }

    // Update is called once per frame
    
}
