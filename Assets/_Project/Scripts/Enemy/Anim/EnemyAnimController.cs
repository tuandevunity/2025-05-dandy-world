using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : TickBehaviour
{
    // Start is called before the first frame update
    [SerializeField] EnemyAnimator enemyAnimator;
    [SerializeField] EnemyAnimBehavior enemyAnimBehavior;

    public EnemyAnimator EnemyAnimator { get { return enemyAnimator; } }

    private void OnValidate()
    {
        if (enemyAnimator == null)
            enemyAnimator = GetComponent<EnemyAnimator>();
        if (enemyAnimBehavior == null)
            enemyAnimBehavior = GetComponent<EnemyAnimBehavior>();
    }
}



