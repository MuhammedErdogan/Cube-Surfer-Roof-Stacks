using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float velocity = 15;
    float coefficient = 2.5f;

    public GameObject mainCamera;

    public bool turning = false;
    public bool forwardCntrl = true;

    private Vector3 touchPosition;
    private Vector3 touchPositionFin;

    public float moveSpeed = 11;

    public bool control = true;


    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void Update()
    {
        forward();
        controller();
    }


    private void forward()
    {

        if (forwardCntrl)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + velocity * Time.fixedDeltaTime);
        }
        if (turning)
        {
            transform.position = new Vector3(transform.position.x + velocity * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
    }
    private void controller()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + coefficient * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - coefficient * Time.fixedDeltaTime, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            touchPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            touchPositionFin = Camera.main.ScreenToViewportPoint(touchPosition);

            if (touchPositionFin.x > 0.5f)
            {
                if (turning)
                {
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + touchPositionFin.x * moveSpeed * Time.fixedDeltaTime);
                }
                if (forwardCntrl)
                {
                    transform.position = new Vector3(transform.position.x + touchPositionFin.x * moveSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
                }
            }
            else
            {
                if (turning)
                {
                    if (velocity < 0)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - touchPositionFin.x * moveSpeed * Time.fixedDeltaTime);
                    if (velocity > 0)
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + touchPositionFin.x * moveSpeed * Time.fixedDeltaTime);
                }
                if (forwardCntrl)
                {
                    transform.position = new Vector3(transform.position.x - touchPositionFin.x * moveSpeed * Time.fixedDeltaTime, transform.position.y, transform.position.z);
                }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "right")
        {
            turning = true;
            forwardCntrl = false;
            velocity = 15;
        }
        if (other.gameObject.tag == "left")
        {
            turning = true;
            forwardCntrl = false;
            velocity = -15;
        }
        if (other.gameObject.tag == "forward")
        {
            turning = false;
            forwardCntrl = true;
            velocity = 15;
        }
    }
}
