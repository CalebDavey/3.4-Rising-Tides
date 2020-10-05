using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Animator animator;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float speed = 5;
    public float rotationSpeed = 0.15f;
    public float gravity;
    public float jumpHeight = 12;
    public float checkDistance = 0.4f;

    public int lowerLimit = -10;

    float velocitySmoothing;
    float ySpeed = 0;

    Vector3 initPosition;

    bool isGrounded;

    private void Awake()
    {
        initPosition = transform.position;
    }

    void Update()
    {
        move();
        triggerAnimations();
        resetCheck();
    }

    void resetCheck()
    {
        if (transform.position.y <= lowerLimit || Input.GetKeyDown(KeyCode.R)) 
        {
            transform.position = initPosition;
        }
    }

    void move()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        isGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, groundMask);

        if (isGrounded && ySpeed < 0)
        {
            ySpeed = -gravity * Time.deltaTime;
        }

        Vector3 direction = new Vector3(horiz, 0, vert).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocitySmoothing, rotationSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            ySpeed = Mathf.Sqrt(jumpHeight * gravity);
        }

        ySpeed -= gravity * Time.deltaTime;
        controller.Move(new Vector3(0.0f, ySpeed, 0.0f));
    }

    void triggerAnimations()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if (isGrounded && horiz != 0 || vert != 0)
        {
            animator.SetBool("running", true);
            animator.SetBool("jumping", false);
        }
        if (ySpeed > 0)
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", true);
        }

        if(isGrounded && horiz == 0 && vert ==0)
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
        }
    }
}