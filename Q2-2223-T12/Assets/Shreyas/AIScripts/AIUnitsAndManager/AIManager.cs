using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class AIManager : MonoBehaviour
{
    private static AIManager _instance;
    public static AIManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    public Transform target;

    [Header("Radius")]
    public float SRadius = 0.5f;
    public float MRadius = 0.5f;
    public float KRadius = 0.5f;
    public float GRadius = 0.5f;


    [Header("Unit Types")]
    public List<SpiderMonkeyUnit> SUnits = new List<SpiderMonkeyUnit>();
    public List<MandrillUnit> MUnits = new List<MandrillUnit>();
    public List<KamikazeUnit> KUnits = new List<KamikazeUnit>();
    public List<GibbonUnit> GUnits = new List<GibbonUnit>();
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        if(gameObject != null)
        {
            MakeAgentsCircleTarget();
        }
    }

    private void MakeAgentsCircleTarget()
    {
        for(int i = 0; i < SUnits.Count; i++)
        {
            SUnits[i].MoveTo(new Vector3(
                target.position.x + SRadius * Mathf.Cos(2 * Mathf.PI * i / SUnits.Count),
                target.position.y,
                target.position.z + SRadius * Mathf.Sin(2 * Mathf.PI * i / SUnits.Count)));
        }

        for (int i = 0; i < KUnits.Count; i++)
        {
            KUnits[i].MoveTo(new Vector3(
                target.position.x + KRadius * Mathf.Cos(2 * Mathf.PI * i / KUnits.Count),
                target.position.y,
                target.position.z + KRadius * Mathf.Sin(2 * Mathf.PI * i / KUnits.Count)));
        }

        for (int i = 0; i < GUnits.Count; i++)
        {
            GUnits[i].MoveTo(new Vector3(
                target.position.x + GRadius * Mathf.Cos(2 * Mathf.PI * i / GUnits.Count),
                target.position.y,
                target.position.z + GRadius * Mathf.Sin(2 * Mathf.PI * i / GUnits.Count)));
        }

        for (int i = 0; i < MUnits.Count; i++)
        {
            MUnits[i].MoveTo(new Vector3(
                target.position.x + MRadius * Mathf.Cos(2 * Mathf.PI * i / MUnits.Count),
                target.position.y,
                target.position.z + MRadius * Mathf.Sin(2 * Mathf.PI * i / MUnits.Count)));
        }
    }

}
