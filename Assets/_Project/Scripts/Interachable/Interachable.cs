using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interachable : MonoBehaviour
{
    [SerializeField] LayerMask layerInclude;
    [SerializeField] float distanceCheck = 2.5f;

    private void OnMouseDown() {
        PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
        if (player == null) {
            Debug.LogError("player being null");
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer > distanceCheck) return;

        Debug.Log("goi"); 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, layerInclude)) {
            Debug.Log(hit.transform.name);
            HandleInterachable(hit);
        }
    }
    
    // use event
    // khi onenable thi dang ky event cho no, khong can check switch case
    // sau do goi ham handle cua game object
    void HandleInterachable(RaycastHit hit) {
        GameObject gameObject = hit.collider.gameObject;
        string layer = LayerMask.LayerToName(hit.collider.gameObject.layer);
        switch (layer) {
            case Preconsts.Layer_Door: 
            {

                Door doorScript = gameObject.GetComponent<Door>();
                doorScript.HandleDoor();
                break;
            }
            case Preconsts.Layer_Step_Button:
            {
                StepButton stepScript = gameObject.GetComponent<StepButton>();
                stepScript.HandleStep();
                break;
            }
            case Preconsts.Layer_Shovel: 
            {
                Shovel shovelScript = gameObject.GetComponent<Shovel>();
                shovelScript.HandleShovel();
                break;
            }
            case Preconsts.Layer_Mine: {
                Mine mineScript = gameObject.GetComponent<Mine>();
                mineScript.HandleMine();
                break;
            }
            case Preconsts.Layer_Wheel: {
                Wheel wheelScript = gameObject.GetComponent<Wheel>();
                wheelScript.HandleWheel();
                break;
            }
            case Preconsts.Layer_Lever: {
                Lever leverScript = gameObject.GetComponent<Lever>();
                leverScript.HandleLever();
                break;
            }
            case Preconsts.Layer_Button: {
                LonNuocBTN buttonScript = gameObject.GetComponent<LonNuocBTN>();
                buttonScript.HandleButton();
                break;
            }
            case Preconsts.Layer_Item: {
                Pickup pickScript = gameObject.GetComponent<Pickup>();
                pickScript.HandlePickup();
                break;
            }
            case Preconsts.Layer_Ladder_Button: {
                LadderButton ladderButton = gameObject.GetComponent<LadderButton>();
                ladderButton.HandleLadderButton();
                break;
            }

            case Preconsts.Layer_GetLadder_Button: {
                GetLadderButton getLadderButton = gameObject.GetComponent<GetLadderButton>();
                getLadderButton.Handle();
                break;
            }

            case Preconsts.Layer_Move_Platform_Button: {
                MovePlatformButton movePlatformButton = gameObject.GetComponent<MovePlatformButton>();
                movePlatformButton.Handle();
                break;
            }

            case Preconsts.Layer_Reset_Platform_Button: {
                RespawnButton respawnButton = gameObject.GetComponent<RespawnButton>();
                respawnButton.Handle();
                break;
            }
        }
    }



}
