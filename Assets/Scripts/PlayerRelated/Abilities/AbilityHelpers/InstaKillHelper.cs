using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillHelper : MonoBehaviour
{
    public delegate void InstaKillEvent(EnemyCombat combatObject);
    public static event InstaKillEvent SignupThisEnemy;

    void Start()
    {
        LuckNPCAbility.SendKillSignal += Signup;
    }

    private void Signup(int counter) {
        SignupThisEnemy?.Invoke(GetComponent<EnemyCombat>());
    }
}
