using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetLadderButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject ladder;
    public GameObject circle;
    public Material newMaterial;
    [SerializeField] GameObject textPro;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Handle() {
        
        ladder.transform.localPosition = new Vector3(3.422f,42.924f,317.38f);
        ladder.transform.localRotation = Quaternion.Euler(90,0,90);
        ladder.layer = LayerMask.NameToLayer(Preconsts.Layer_Ladder);
        circle.GetComponent<MeshRenderer>().material = newMaterial;
        textPro.SetActive(false);
    }
}
