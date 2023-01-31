using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class SpiderMonkeyUnit : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyHealth health;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        AIManager.Instance.SUnits.Add(this);
    }
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            AIManager.Instance.SUnits.Remove(this);
        }
    }

    public void MoveTo(Vector3 Position)
    {
        agent.SetDestination(Position);
    }
}
