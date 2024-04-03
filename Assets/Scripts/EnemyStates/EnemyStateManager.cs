using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    private EnemyBaseState currentState;
    private EnemyBaseState previousState;
    private EnemyBaseState nextState;

   // private bool toAttack, toArena, toDead, toPrevious;

    private void Start()
    {
        InitializeTransitions();
    }

    private void InitializeTransitions() {
       // toAttack = false;
      //  toArena = false;
    //    toDead = false;
    //    toPrevious = false;
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    private void TransitionToAttack() {
        //toAttack = true;
        StartCoroutine(DelayTransition(0.0f));
    }

    private IEnumerator DelayTransition(float delay) {
        yield return new WaitForSeconds(delay);

      //  if (toArena) {
     //       toArena = false;
   //         nextState = EnemyArenaState.Instance;
    //    }
    }
}
