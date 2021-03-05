using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCubeBehaviour : MonoBehaviour
{
    void Start()
    {

    }

    void FixedUpdate()
    {
        rot();
    }

    private void rot()
    {
        this.transform.Rotate(0, 5, 0,Space.World);
    }
}
