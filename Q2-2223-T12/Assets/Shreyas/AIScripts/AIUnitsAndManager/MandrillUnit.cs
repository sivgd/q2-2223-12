using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class MandrillUnit : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyHealth health;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AIManager.Instance.MUnits.Add(this);
    }
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            AIManager.Instance.MUnits.Remove(this);
        }
    }

    public void MoveTo(Vector3 Position)
    {
        agent.SetDestination(Position);
    }
}
