using Managers;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using Tank;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAI : NetworkBehaviour
{
    [SerializeField] private float chaseRange = 50f;
    [SerializeField] private float attackRange = 10f;
    
    private Health[] targets;
    private Transform mainTarget = null;
    private NavMeshAgent navMeshAgent;
    
    private float timeSinceLastAttack = Mathf.Infinity;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        GetSoldierTargets();
    }

   

    void Update()
    {
        GetSoldierTargets();

        if (mainTarget == null) { return; }
        if (GetIsInAttackRange(mainTarget))
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.ResetPath();
            
        }
        else
        {
            navMeshAgent.SetDestination(mainTarget.position);
        }
        
    }

    private bool GetIsInRange(Transform targetTransform)
    {
        return (Vector3.Distance(transform.position, targetTransform.position)) < chaseRange;
    }
    private bool GetIsInAttackRange(Transform targetTransform)
    {
        return Vector3.Distance(transform.position, targetTransform.position) < attackRange;
    }
    private void GetSoldierTargets()
    {
        targets = FindObjectsOfType<Health>();
        for (int i = 0; i < targets.Length; i++)
        {
            mainTarget = targets[i].transform;
            if (CheckTeamColors()) { Debug.Log(1); break; }
            else if (!GetIsInRange(mainTarget.transform)) { Debug.Log(2); break; }    
            else if (mainTarget.tag == "Enemy") { Debug.Log(3); break; }
            else if (mainTarget.tag == "Building") { Debug.Log(4); break; }
            else if (mainTarget.tag == "Player")
            {
                
                
                return;
            }
        }
    }

    private bool CheckTeamColors()
    {
        return GetComponent<TeamColorSetter>().GetTeamColor() == mainTarget.GetComponent<TeamColorSetter>().GetTeamColor();
    }

    void OnDrawGizmosSelected()
    {
        // Draw a red sphere at the transform's position to show the firing range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
