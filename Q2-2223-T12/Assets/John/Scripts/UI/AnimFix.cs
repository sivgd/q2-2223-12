using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFix : MonoBehaviour
{
    private Animator animator;
    private bool idle; 
    public Vector3 normalOffset = new Vector3(1.85f, 0.7f, 6.25f);
    public Vector3 idleOffset = new Vector3(0.55f, 0.28f, 3.25f); 
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        idle = !(animator.GetBool("IsShotgunFire") && animator.GetBool("IsLilyFling") && animator.GetBool("IsCharge") && animator.GetBool("IsSlimeballFire"));
        Debug.Log($"idle: {idle}"); 
        if (!idle) transform.localPosition = normalOffset;
        else transform.localPosition = idleOffset; 
    }
}
