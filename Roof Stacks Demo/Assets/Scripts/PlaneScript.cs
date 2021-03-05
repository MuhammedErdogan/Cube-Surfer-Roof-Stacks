using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject[] cubes;

    void Start()
    {
        cubes = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //mainCamera.transform.SetParent(collision.gameObject.GetComponent<Transform>());
        cubes = GameObject.FindGameObjectsWithTag("Player");
    }
}
