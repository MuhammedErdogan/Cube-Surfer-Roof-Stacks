using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rgbd;

    public GameObject cubePrefabs;

    public GameObject tampon;

    GameObject clone;

    GameObject[] cubes;
    public Text score;
    public Text panelScore;

    bool stabil = false;
    bool isGravity = false;
    bool isUp = false;

    float time = 0;

    void Start()
    {
        rgbd = this.GetComponent<Rigidbody>();
        tampon = this.gameObject;
        cubes = GameObject.FindGameObjectsWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (stabil)
            stabilizer();

        if (isGravity)
            gravity();
        if (isUp)
            up();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MiniCubeTag")
        {
            cubes = GameObject.FindGameObjectsWithTag("Player");
            Vector3 pos = new Vector3(tampon.transform.position.x, tampon.transform.position.y + cubes.Length, tampon.transform.position.z);
            tampon = this.gameObject;
            clone = Instantiate(cubePrefabs, pos, Quaternion.identity);
            stabil = true;
            Destroy(other.gameObject);
            clone.GetComponent<PlayerController>().enabled = true;
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 1); // oyun bittiginde sifirla
            score.text = "score: " + PlayerPrefs.GetInt("score");

        }
        if (other.gameObject.tag == "finish")
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            panelScore.text = "score: " + PlayerPrefs.GetInt("score");
        }
        if (other.gameObject.tag == "bridgeOne")
        {
            isGravity = true;
        }
        if (other.gameObject.tag == "bridgeTwo")
        {
            isUp = true;
        }
    }

    void stabilizer()
    {
        cubes = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < cubes.Length; i++)
        {
            cubes = GameObject.FindGameObjectsWithTag("Player");
            cubes[cubes.Length - 1 - i].transform.position = new Vector3(cubes[0].transform.position.x, cubes[cubes.Length - 1 - i].transform.position.y, cubes[0].transform.position.z);
        }
        stabil = false;
    }

    void gravity()
    {
        time += Time.deltaTime;
        if (time > 0.01f)
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                cubes = GameObject.FindGameObjectsWithTag("Player");
                float x = cubes[i].transform.position.x;
                float y = cubes[i].transform.position.y;
                float z = cubes[i].transform.position.z;
                cubes[i].transform.position = new Vector3(x, 0.5f + i, z);
            }
            time = 0;
            isGravity = false;
        }

    }
    void up()
    {
        time += Time.deltaTime;
        if (time > 0.01f)
        {
            for (int i = 0; i < cubes.Length; i++)
            {
                cubes = GameObject.FindGameObjectsWithTag("Player");
                float x = cubes[i].transform.position.x;
                float y = cubes[i].transform.position.y;
                float z = cubes[i].transform.position.z;
                cubes[cubes.Length - 1 - i].transform.position = new Vector3(x, 1.4f + i, z);
                time = 0;
                isUp = false;
            }

        }
    }
}
