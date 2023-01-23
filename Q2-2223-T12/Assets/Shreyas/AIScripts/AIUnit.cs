using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class AIUnit : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyHealth health;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AIManager.Instance.Units.Add(this);
    }
    void Update()
    {
        if(health.currentHealth <= 10)
        {
            AIManager.Instance.Units.Clear();
        }
    }

    public void MoveTo(Vector3 Position)
    {
        agent.SetDestination(Position);
    }
}
