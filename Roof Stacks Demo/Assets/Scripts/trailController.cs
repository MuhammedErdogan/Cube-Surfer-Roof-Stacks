using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trailController : MonoBehaviour
{

    public GameObject forward;
    public GameObject left;
    public GameObject right;
    void Start()
    {
        Physics.IgnoreCollision(forward.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(left.GetComponent<Collider>(), GetComponent<Collider>());
        Physics.IgnoreCollision(right.GetComponent<Collider>(), GetComponent<Collider>());
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        if(other.CompareTag("Player"))
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = true;
        }
        else if(other.CompareTag("ObstacleTag"))
        {
            this.gameObject.GetComponent<TrailRenderer>().enabled = false;
        }

    }
}
