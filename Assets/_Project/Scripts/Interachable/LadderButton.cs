using System.Collections.Generic;
using UnityEngine;

public class LadderButton : MonoBehaviour {
    public Material newMaterial;
    [SerializeField] LadderButton otherLadderButton;
    public GameObject line;
    public GameObject circle;
    public GameObject door;
    public bool isActive;
    void Start()
    {
    
    }
    public void HandleLadderButton() {

        isActive = true;
        if (otherLadderButton.isActive) {
            door.SetActive(false);
        }
        circle.GetComponent<MeshRenderer>().material = newMaterial;
        line.GetComponent<MeshRenderer>().material = newMaterial;
    }
}