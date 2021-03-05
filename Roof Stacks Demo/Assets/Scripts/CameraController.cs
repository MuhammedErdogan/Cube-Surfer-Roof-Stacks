using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject sphere;
    public GameObject[] players;
    Vector3 offset;
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        offset = sphere.transform.position - players[players.Length - 1].transform.position;
    }

    void LateUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        sphere.transform.position = players[players.Length - 1].transform.position + offset;
        sphere.transform.position = new Vector3(sphere.transform.position.x, 4, transform.position.z);
        bool turn = players[players.Length - 1].GetComponent<Movement>().turning;
        bool forward = players[players.Length - 1].GetComponent<Movement>().forwardCntrl;
        float vel = players[players.Length - 1].GetComponent<Movement>().velocity;
        if (turn)
        {
            if (vel > 0)
            {
                sphere.transform.rotation = Quaternion.Slerp(sphere.transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5);
                sphere.transform.position = new Vector3(players[players.Length - 1].transform.position.x - 4, sphere.transform.position.y, players[players.Length - 1].transform.position.z);

            }
            if (vel < 0)
            {
                sphere.transform.rotation = Quaternion.Slerp(sphere.transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5);
                sphere.transform.position = new Vector3(players[players.Length - 1].transform.position.x + 4, sphere.transform.position.y, players[players.Length - 1].transform.position.z);
            }
        }
        if (forward)
        {
            sphere.transform.rotation = Quaternion.Slerp(sphere.transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);
            sphere.transform.position = new Vector3(players[players.Length - 1].transform.position.x, sphere.transform.position.y, players[players.Length - 1].transform.position.z - 4);
        }

        players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 5)
        {
            if (this.GetComponent<Camera>().fieldOfView < 60 + players.Length * 3.5f)
                this.GetComponent<Camera>().fieldOfView += (players.Length - 5) * Time.deltaTime * 1.6f;
        }
        else
            this.GetComponent<Camera>().fieldOfView = 60;
    }

    void rotate(float angle)
    {
        Quaternion newRot = new Quaternion(sphere.transform.rotation.x, sphere.transform.rotation.y, sphere.transform.rotation.z, sphere.transform.rotation.w);
        newRot *= Quaternion.Euler(0, angle, 0);
        sphere.transform.rotation = Quaternion.Slerp(sphere.transform.rotation, newRot, 10 * Time.deltaTime);
    }
}
