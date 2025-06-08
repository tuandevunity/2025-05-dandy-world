using System.Collections;
using UnityEngine;

public class RespawnButton : MonoBehaviour
{
    [SerializeField] Lever lever;

    public Material[] newMaterials;

    private Material[] originalMaterials;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originalMaterials = meshRenderer.materials; 
    }

    public void Handle()
    {

        lever.Reset();
        StartCoroutine(ChangeColorTemporarily());
    }

    IEnumerator ChangeColorTemporarily()
    {
        meshRenderer.materials = newMaterials; 
        yield return new WaitForSeconds(0.1f); 
        meshRenderer.materials = originalMaterials; 
    }
}
