using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 5;
    public float rotationSpeed = 0.15f;
    public float gravity;
    public float jumpHeight = 12;
    public float checkDistance = 0.4f;

    float velocitySmoothing;
    float ySpeed = 0;

    bool isGrounded;

    void Update()
    {
        move();
    }

    void move()
    {
        float ySpeed =+ gravity * Time.deltaTime;
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horiz, ySpeed, vert).normalized;

        if(direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocitySmoothing, rotationSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
            
            gravity -= 9.81f * Time.deltaTime;
            controller.Move(new Vector3(0.0f, gravity, 0.0f));
            if (controller.isGrounded)
            {
                gravity = 0;
            }
        }

        if(horiz != 0 || vert != 0)
        {
            animator.SetBool("running", true);
        } else
        {
            animator.SetBool("running", false);
        }

    }
}
