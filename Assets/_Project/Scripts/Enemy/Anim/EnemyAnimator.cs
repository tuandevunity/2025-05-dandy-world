using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : TickBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Animator animator;

    private void OnValidate()
    {
        if(animator == null)
            animator = GetComponent<Animator>();
    }

    public void SetIdle()
    {
        animator.SetBool(Preconsts.Anim_Idle, true);
        
    }

    public void ExitIdle()
    {
        animator.SetBool(Preconsts.Anim_Idle, false);
    }

    public void SetWalk() {
        animator.SetBool(Preconsts.Anim_Walk, true);
    }

    public void ExitWalk() 
    {
        animator.SetBool(Preconsts.Anim_Walk, false);
    }

    public void SetAttack()
    {
        animator.SetTrigger(Preconsts.Anim_Attack);
    }
    
}
