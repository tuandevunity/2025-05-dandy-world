using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LonNuocBTN : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject lonNuoc;
    [SerializeField] Transform posSpawn;

    [SerializeField] GameObject tutor;
    public Material[] newMaterials;
    public void HandleButton()
    {
        if (lonNuoc == null) return;
        tutor.SetActive(false);
        lonNuoc.SetActive(false);
        lonNuoc.transform.position = posSpawn.position;
        lonNuoc.SetActive(true);
    }
}
