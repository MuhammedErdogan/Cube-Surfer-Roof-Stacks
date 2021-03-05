using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomPath : MonoBehaviour
{

    public GameObject left;
    public GameObject right;
    public GameObject forward;
    public GameObject nextLevel;

    private static int tampon = 0;
    private static int rotateCounter = 0;
    private static int pathLongerCounter = 0;
    private static int turningCounter = 0;

    private static float scaleZ = 0;
    private static float scaleX = 0;
    private static float tamponScaleZ = 0;
    private static float tamponScaleX = 0;

    private static float scaleCounterZ = 0;
    private static float scaleCounterX = 0;
    private static float positionZ = 0;
    private static float positionX = 0;

    private static float time = 0;

    bool next = false;

    public GameObject clone;

    void Start()
    {
        scaleZ = Random.Range(10, 16);
        forward.transform.localScale = new Vector3(0.5f, 1, scaleZ);
        Instantiate(forward, new Vector3(0, 0, 0), Quaternion.identity);
        tamponScaleZ = scaleZ;
    }

    void Update()
    {
        time += Time.fixedDeltaTime;
        if (time > 0.1f && pathLongerCounter <= 10)
        {
            generateRandom();
            time = 0;
        }
        if (pathLongerCounter >= 10 && !next)
        {
            next = true;
            generateRandom();
        }
    }

    public void generateRandom()
    {

        tampon = rotateCounter;
        rotateCounter = Random.Range(1, 4);

        //if turn left => -90 // if turn right => 90 || if generate left or rigt <=> straight || if straight no condition
        // 1 = right | 2 = straight | 3 = left 
        // width of the road is constante(X Axis) and it's 5(five)
        if (rotateCounter == 1 && tampon == 2)
        {
            scaleZ = Random.Range(4, 16);
            right.transform.localScale = new Vector3(0.5f, 1, scaleZ);

            tamponScaleX = scaleX;
            scaleX = scaleZ; // because of rotate 90 degre

            scaleCounterZ += tamponScaleZ; //The distance we traveled on the Y axis
            scaleCounterX += tamponScaleX; //The distance we traveled on the X axis

            positionZ = (scaleCounterZ * 5) + 2.5f; //_|
            positionX = ((scaleX * 5) - 2.5f + scaleCounterX * 10);


            if (!next)
            {
                clone = Instantiate(right, new Vector3(positionX, 0, positionZ), Quaternion.Euler(0, 90, 0));
            }
            if (next)
            {
                Instantiate(nextLevel, new Vector3(clone.transform.position.x, 0.01f, clone.transform.position.z + clone.transform.localScale.z * 5 + 25), Quaternion.Euler(0, 0, 0));
            }

            tamponScaleZ = 0f;
            pathLongerCounter++;
            turningCounter++;
        }

        if (rotateCounter == 2)
        {
            scaleZ = Random.Range(4, 16);
            forward.transform.localScale = new Vector3(0.5f, 1, scaleZ);

            scaleCounterZ += tamponScaleZ + scaleZ;
            positionZ = scaleCounterZ * 5;
            positionX = (scaleCounterX * 10) + scaleX * 10;

            if (!next)
            {
                clone = Instantiate(forward, new Vector3(positionX, 0, positionZ), Quaternion.Euler(0, 0, 0));
            }
            if (next)
            {
                if (tampon == 1) Instantiate(nextLevel, new Vector3(clone.transform.position.x + 25 + clone.transform.localScale.x * 5, 0.01f, clone.transform.position.z), Quaternion.Euler(0, 90, 0));
                if (tampon == 3) Instantiate(nextLevel, new Vector3(clone.transform.position.x - 25 - clone.transform.localScale.x * 5, 0.01f, clone.transform.position.z), Quaternion.Euler(0, -90, 0));
            }

            tamponScaleZ = scaleZ;
            pathLongerCounter++;

        }

        if (rotateCounter == 3 && tampon == 2)
        {
            scaleZ = Random.Range(4, 16);
            left.transform.localScale = new Vector3(0.5f, 1, scaleZ);

            tamponScaleX = scaleX;
            scaleX = -scaleZ; // because of rotate 90 degre

            scaleCounterZ += tamponScaleZ; //The distance we traveled on the Y axis
            scaleCounterX += tamponScaleX; //The distance we traveled on the X axis

            positionZ = (scaleCounterZ * 5) + 2.5f; //_|
            positionX = ((scaleX * 5) + 2.5f + scaleCounterX * 10);

            if (!next)
            {
                clone = Instantiate(left, new Vector3(positionX, 0, positionZ), Quaternion.Euler(0, -90, 0));
            }
            if (next)
            {
                Instantiate(nextLevel, new Vector3(clone.transform.position.x, 0.01f, clone.transform.position.z + clone.transform.localScale.z * 5 + 25), Quaternion.Euler(0, 0, 0));
            }

            tamponScaleZ = 0f;
            pathLongerCounter++;
            turningCounter++;
        }

    }
}
