using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFix : MonoBehaviour
{
    private Animator animator;
    private bool idle, shotGun, isCharge, isSlimeballFire, isLilyFling;
    private LilypadSniper lilypad; 

    /// <summary>
    /// normal offset applies to the lilypad, shotgun
    /// </summary>
    public Vector3 normalOffset = new Vector3(1.85f, 0.7f, 6.25f);
    public Vector3 idleOffset = new Vector3(0.55f, 0.28f, 3.25f);
    public Vector3 chargeOffset = new Vector3(1.6f, -0.4f, 5.2f);
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        lilypad = GetComponent<LilypadSniper>(); 
    }

    // Update is called once per frame
    void Update()
    {
      
        isLilyFling = lilypad.GetAnimating();
        idle = !(shotGun && isLilyFling && isCharge && isSlimeballFire);
        if (isLilyFling) transform.localPosition = normalOffset;
        
    }
}
