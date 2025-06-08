using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : TickBehaviour
{
    [SerializeField] Animator animator;
    FixedJoystick joystick => UIManager.Panel<PanelGamePlay>().FixedJoystick;
    private void OnValidate()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }
    public void SetIdle()
    {
        animator.SetBool(Preconsts.Anim_Idle, true);
        ChangeFoot(Random.Range(0, 4));
    }
    public void ExitIdle()
    {
        animator.SetBool(Preconsts.Anim_Idle, false);
    }
    public void SetWalk()
    {
        animator.SetBool(Preconsts.Anim_Walk, true);
    }
    public void OnFixedUpdateWalk()
    {
        SetSpeedAnimMove(0.5f);
    }
    void SetSpeedAnimMove(float claimMin)
    {
        bool inputKey = false;
        float keyboardMagnitude = 0;
#if UNITY_EDITOR
        float keyboardInputX = Input.GetAxis("Horizontal");
        float keyboardInputY = Input.GetAxis("Vertical");
        Vector2 keyboardInput = new Vector2(keyboardInputX, keyboardInputY);
        keyboardMagnitude = keyboardInput.magnitude;
        if(keyboardInputX != 0 || keyboardInputY != 0)
            inputKey = true;

#endif
        animator.SetFloat(Preconsts.Anim_MultiplierMove, Mathf.Clamp(inputKey ? keyboardMagnitude : joystick.input.magnitude * 2, claimMin, 1f));
    }
    public void ExitWalk()
    {
        animator.SetBool(Preconsts.Anim_Walk, false);
    }
    public void SetRun()
    {
        animator.SetBool(Preconsts.Anim_Run, true);
    }
    public void OnFixedUpdateRun()
    {
        SetSpeedAnimMove(0);
    }
    public void ExitRun()
    {
        animator.SetBool(Preconsts.Anim_Run, false);
    }
    Coroutine corChangeFoot;
    public void ChangeFoot(float endValue)
    {
        if(corChangeFoot != null)
        {
            StopCoroutine(corChangeFoot);
        }
        corChangeFoot = StartCoroutine(ChangeFloatCoroutine(endValue));
    }

    private IEnumerator ChangeFloatCoroutine(float endValue)
    {
        float elapsedTime = 0f;
        string nameAnim = Preconsts.Anim_Foot;
        while(elapsedTime < 0.25f)
        {
            float currentValue = Mathf.Lerp(animator.GetFloat(nameAnim), endValue, elapsedTime / 0.25f);
            animator.SetFloat(nameAnim, currentValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        animator.SetFloat(nameAnim, endValue);
    }


    public void SetJump()
    {
        animator.SetTrigger(Preconsts.Anim_Jump);
    }

    public void SetFalling()
    {
        animator.SetTrigger(Preconsts.Anim_Falling);
    }
    public void SetLanding()
    {
        animator.SetTrigger(Preconsts.Anim_Landing);
    }

    public void SetLadder()
    {
        animator.SetBool(Preconsts.Anim_Ladder, true);
    }
    public void ExitLadder()
    {
        animator.SetBool(Preconsts.Anim_Ladder, false);
    }
    public void OnUpdateLadder()
    {
        bool inputKey = false;
#if UNITY_EDITOR
        inputKey = Input.GetAxis("Vertical") != 0;
#endif
        animator.SetFloat(Preconsts.Multiplier_Ladder, inputKey ? Input.GetAxis("Vertical") : joystick.Vertical);
    }

    public void SetJetPack()
    {
        animator.SetTrigger(Preconsts.Anim_Jetpack);
    }
    public void SetDie()
    {
        animator.SetTrigger(Preconsts.Anim_Die);
    }
    public void SetAttack()
    {
        animator.SetTrigger(Preconsts.Anim_Attack);
    }
}
