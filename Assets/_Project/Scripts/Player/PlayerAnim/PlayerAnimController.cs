using UnityEngine;

public class PlayerAnimController : TickBehaviour
{
    [SerializeField] PlayerAnimator playerAnimator;
    [SerializeField] PlayerAnimBehavior playerAnimBehavior;

    public PlayerAnimator PlayerAnimator { get { return playerAnimator; } }

    private void OnValidate()
    {
        if (playerAnimator == null)
            playerAnimator = GetComponent<PlayerAnimator>();
        if (playerAnimBehavior == null)
            playerAnimBehavior = GetComponent<PlayerAnimBehavior>();
    }
}
