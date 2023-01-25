using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnEnemies : MonoBehaviour
{
    private List<Collider> collidersHit = new List<Collider>();
    private Collider thisCol; 
    bool active = false;
    Ray checkForWall;
    public string levelTag;
    RaycastHit hit; 
    private void Start()
    {
        thisCol = GetComponent<Collider>(); 
    }
    private void OnTriggerEnter(Collider other)
    {

        if (active)
        {
            if (!other.gameObject.layer.Equals("player") && !other.gameObject.layer.Equals(levelTag))
            {

                checkForWall.origin = transform.position;
                checkForWall.direction = other.transform.position;
                
                if (Physics.Raycast(checkForWall,out hit, thisCol.bounds.size.z))
                {
                    if(!hit.collider.CompareTag(levelTag))
                    {
                        collidersHit.Add(other); 
                    }
                }
            }
        }

    }
    public Collider[] getColliders()
    {
        Debug.Log(collidersHit.ToString());
        return collidersHit.ToArray(); 
    }
    public void clearColliderList()
    {
        collidersHit.Clear(); 
    }
    public void setActive(bool newActive)
    {
        active = newActive; 
    }
    public bool getActive()
    {
        return active; 
    }
}
