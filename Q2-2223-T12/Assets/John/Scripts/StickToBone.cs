using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToBone : MonoBehaviour
{
    public Transform bone;
    private void Update()
    {
        transform.position = bone.position; 
    }
}
