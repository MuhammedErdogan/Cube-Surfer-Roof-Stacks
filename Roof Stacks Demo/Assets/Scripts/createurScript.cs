using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createurScript : MonoBehaviour
{
    Rigidbody rgbd;

    float velocity = 20f;


    public bool turning = false;
    public bool forwardCntrl = true;

    public bool control = true;

    public GameObject[] obstacles;
    public GameObject[] bridge;
    public GameObject miniCub;

    int randomEasy;
    int randomBridge;

    Vector3 createPosition;

    float time = 0;

    public GameObject clone;
    public GameObject tamponClone;

    public float distance;
    public float distanceMiniCube;


    bool Left = false;
    bool right = false;

    float clock;

    void Start()
    {
        rgbd = GetComponent<Rigidbody>();
    }

    void Update()
    {

        time += Time.fixedDeltaTime;
        if (PlayerPrefs.GetInt("level") == 0)
        {
            clock = 0.9f;
        }
        else
        {
            clock = ((PlayerPrefs.GetInt("level") / (PlayerPrefs.GetInt("level") - 1)) * (PlayerPrefs.GetInt("level") / (PlayerPrefs.GetInt("level") - 1))) * 3.8f / 1.5f;
        }
        if (time > clock)
        {
            generate();
            time = 0;
        }
    }

    private void FixedUpdate()
    {
        forward();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "right")
        {
            createPosition = other.gameObject.transform.parent.transform.position;
            turning = true;
            forwardCntrl = false;
            Left = false;
            right = true;
            velocity = 20f;
        }
        if (other.gameObject.tag == "left")
        {
            createPosition = other.gameObject.transform.parent.transform.position;
            turning = true;
            forwardCntrl = false;
            Left = true;
            right = false;
            velocity = -20f;
        }
        if (other.gameObject.tag == "forward")
        {
            createPosition = other.gameObject.transform.parent.transform.position;
            turning = false;
            forwardCntrl = true;
            velocity = 20f;
        }
        if (other.gameObject.tag == "finish") Destroy(this.gameObject);
    }

    void generate()
    {
        int intervalUp;
        if ((4 + PlayerPrefs.GetInt("level")) < obstacles.Length)
        {
            if (PlayerPrefs.GetInt("level") % 2 == 0)
                intervalUp = (4 + PlayerPrefs.GetInt("level"));
            else intervalUp = (2 + PlayerPrefs.GetInt("level"));
        }
        else intervalUp = obstacles.Length - 1;

        randomEasy = Random.Range(0, intervalUp);
        randomBridge = Random.Range(0, (bridge.Length));
        int decision = Random.Range(1, 4);

        if (forwardCntrl)
        {
            tamponClone = clone;
            if (decision == 1 || decision == 2)
                clone = Instantiate(obstacles[randomEasy], new Vector3(createPosition.x, 0.5f, this.transform.position.z), Quaternion.identity);
            if (decision == 3)
                clone = Instantiate(bridge[randomBridge], new Vector3(createPosition.x, 0.5f, this.transform.position.z), Quaternion.identity);
        }
        if (turning)
        {
            tamponClone = clone;
            clone = Instantiate(obstacles[randomEasy], new Vector3(transform.position.x, 0.5f, createPosition.z), Quaternion.Euler(0, 90, 0));
        }

    }

    float distanceCalculateur(GameObject one, GameObject two)
    {
        float dist = Vector3.Distance(one.transform.position, two.transform.position);
        return dist;
    }

    //private void miniCubeGenerate()
    //{
    //    if (forwardCntrl)
    //    {
    //        miniCubTamponClone = miniCubClone;
    //        for (int i = 0; i < clone.transform.childCount / createdMiniCubCount; i++)
    //        {
    //            float Z = distance * 3 / clone.transform.childCount;
    //            float randomX = Random.Range(-2.4f, 2.4f);
    //            float FposZ = tamponClone.transform.position.z + 10 + Z * i;
    //            miniCubClone = Instantiate(miniCub, new Vector3(createPosition.x + randomX, 0.5f, FposZ), Quaternion.identity);
    //        }
    //    }
    //    if (turning)
    //    {
    //        miniCubTamponClone = miniCubClone;
    //        int k = clone.transform.childCount / createdMiniCubCount;
    //        for (int i = 0; i < k; i++)
    //        {
    //            float Z = distance * 3 / clone.transform.childCount;

    //            float randomZ = Random.Range(-2.4f, 2.4f);

    //            float RposX = tamponClone.transform.position.x + 25 + Z * i;
    //            float LposX = tamponClone.transform.position.x - 25 - Z * i;

    //            if (Left)
    //            {
    //                miniCubClone = Instantiate(miniCub, new Vector3(LposX, 0.5f, createPosition.z + randomZ), Quaternion.identity);
    //            }
    //            if (right)
    //            {
    //                miniCubClone = Instantiate(miniCub, new Vector3(RposX, 0.5f, createPosition.z + randomZ), Quaternion.identity);
    //            }

    //        }
    //    }
    //}
}
