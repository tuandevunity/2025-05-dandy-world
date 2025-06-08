using DG.Tweening;
using UnityEngine;

public class MovePlatformButton : MonoBehaviour {
    public GameObject circle;
    public Material newMaterial;
    public Transform[] pivots;
    public Vector3 offset;
    bool _active;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Handle() {
        if (_active == true) return;
        for (int i = 0; i < pivots.Length; i++) {
            pivots[i].DOLocalMove(pivots[i].position + offset, 1f).
            SetEase(Ease.Linear);
        
        }
        _active = true;
        circle.GetComponent<MeshRenderer>().material = newMaterial;
    }
}