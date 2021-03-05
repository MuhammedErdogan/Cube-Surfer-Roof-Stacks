using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCubeCreateur : MonoBehaviour
{

    public GameObject miniCubes;

    float velocity = 20;

    public bool turning = false;
    public bool forwardCntrl = true;

    Rigidbody rgbd;


    int miniCubeNumber = 0;
    int ObstacleCubeNumber = 0;

    public GameObject createur;
    float time = 0;

    float clock = 0;

    Vector3 pos;

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        forward();
        time += Time.deltaTime;
        if (PlayerPrefs.GetInt("level") == 0)
        {
            clock = 0.9f;
        }
        else
        {
            clock = ((PlayerPrefs.GetInt("level") / (PlayerPrefs.GetInt("level") - 1)) * (PlayerPrefs.GetInt("level") / (PlayerPrefs.GetInt("level") - 1))) / 1.5f;
        }
        if (time > clock)
        {
            generate();
            time = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "right")
        {
            pos = other.gameObject.transform.parent.transform.position;
            turning = true;
            forwardCntrl = false;
            velocity = 20f;
        }
        if (other.gameObject.tag == "left")
        {
            pos = other.gameObject.transform.parent.transform.position;
            turning = true;
            forwardCntrl = false;
            velocity = -20f;
        }
        if (other.gameObject.tag == "forward")
        {
            pos = other.gameObject.transform.parent.transform.position;
            turning = false;
            forwardCntrl = true;
            velocity = 20f;
        }
        if (other.gameObject.tag == "finish") Destroy(this.gameObject);
    }
    private void forward()
    {

        if (forwardCntrl)
        {
            rgbd.velocity = transform.forward * velocity * 2;
        }
        if (turning)
        {
            rgbd.velocity = transform.right * velocity * 2;
        }
    }

    void generate()
    {
        float random = Random.Range(-2.4f, 2.4f);
        if (forwardCntrl)
            Instantiate(miniCubes, new Vector3(pos.x + random, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        if (turning)
            Instantiate(miniCubes, new Vector3(this.transform.position.x, this.transform.position.y, pos.z + random), Quaternion.identity);
    }


}
