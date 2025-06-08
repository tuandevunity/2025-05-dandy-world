using DG.Tweening;
using UnityEngine;

public class DoorBoss : MonoBehaviour {
    [SerializeField] GameObject doorLeft;
    [SerializeField] GameObject doorRight;
    [SerializeField] float offsetOpen = 4f;

    private Vector3 doorLeftPos;
    private Vector3 doorRightPos;

    private bool isOpen;
    void Start()
    {
        doorLeftPos = doorLeft.transform.position;
        doorRightPos = doorRight.transform.position;
    }

    public void OpenDoor() {
        doorLeft.transform.DOLocalMove(doorLeft.transform.position + new Vector3(offsetOpen, 0, 0), 1f).SetEase(Ease.Linear);
        doorRight.transform.DOLocalMove(doorRight.transform.position + new Vector3(-offsetOpen, 0, 0), 1f).SetEase(Ease.Linear);
        isOpen = true;
    }

    public void CloseDoor() {
        doorLeft.transform.DOLocalMove(doorLeftPos, 0.5f).SetEase(Ease.Linear);
        doorRight.transform.DOLocalMove(doorRightPos, 0.5f).SetEase(Ease.Linear);
        isOpen = false;
    }

    public void ResetDoor() {
        Debug.Log("resset door");
        isOpen = false;
        doorLeft.transform.position = doorLeftPos;
        doorRight.transform.position = doorRightPos;
    }
}