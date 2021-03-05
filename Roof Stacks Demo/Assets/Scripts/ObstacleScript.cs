using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameObject[] players;

    public int first = 0;
    public int end = 0;

    float time = 0;

    bool isGravity;

    void FixedUpdate()
    {
        if (isGravity)
        {
            gravity();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            players = GameObject.FindGameObjectsWithTag("Player");
            first = players.Length;
            Destroy(other.gameObject);
            end = players.Length;
            isGravity = true;
        }
    }

    void gravity()
    {
        int offset = first - end;
        time += Time.deltaTime;
        if (time > 0.2f)
        {
            for (int i = 0; i < players.Length; i++)
            {
                players = GameObject.FindGameObjectsWithTag("Player");
                float x = players[i].transform.position.x;
                float y = players[i].transform.position.y;
                float z = players[i].transform.position.z;
                players[i].transform.position = new Vector3(x, 0.5f + i, z);
            }
            time = 0;
            isGravity = false;
        }

    }
}
