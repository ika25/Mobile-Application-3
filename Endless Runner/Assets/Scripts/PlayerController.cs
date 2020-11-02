using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;

    private int correctLane = 1;//left,middle,right
    public float laneDistance = 2.5f;//The distance between tow lanes

    public float jumpForce;
    public float Gravity = -20;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    // Update is called once per frame
    void Update()
    {
        direction.z = forwardSpeed;

        if (controller.isGrounded)
        {
            direction.y = -1;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime;

        }

        //getting inpits on which lane we should be on
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            correctLane++; //incriment by 1
            if (correctLane == 3)//Right Arrow
                correctLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            correctLane--; //decrice by 1
            if (correctLane == -1)//Left Arrow
                correctLane = 0;
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (correctLane == 0)//need to be on the left
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (correctLane == 2)//need to be on the right
        {
            targetPosition += Vector3.right * laneDistance;

        }

        //transform.position = Vector3.Lerp(trasform.position, targetPosition, 80 * Time.deltaTime);
        transform.position = targetPosition;

    }

    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }
}
