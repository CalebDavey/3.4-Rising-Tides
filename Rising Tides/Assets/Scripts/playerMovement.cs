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
    public float lowerLimit = -30;
    public bool movementEnabled = true;
    public bool reset = false;

    float velocitySmoothing;
    Vector3 Velocity = new Vector3(0,0,0);

   public Vector3 initPosition;

    bool isGrounded;

    GameObject prevCollisionObject;

    private void Start()
    {
        initPosition = transform.position;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, checkDistance, groundMask);
        if (movementEnabled)
        {
            move();
        }
        triggerAnimations();
        resetCheck();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "Pillar" && hit.gameObject != prevCollisionObject) {
            if (hit.transform.position.y < transform.position.y - (hit.transform.localScale.y / 2))
            {
                hit.gameObject.SendMessage("triggerPillarDestroy", SendMessageOptions.DontRequireReceiver);
                prevCollisionObject = hit.gameObject;
            }
        }
    }

    public void resetCheck()
    {
        if (transform.position.y <= lowerLimit || Input.GetKeyDown(KeyCode.R) || reset == true)
        {
            resetPlayer();
            reset = false;
        }
    }

    public void resetPlayer()
    {
        this.transform.position = initPosition;
    }

    void move()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float vert = Input.GetAxisRaw("Vertical");

        if(isGrounded != true)
        {
            isGrounded = controller.isGrounded;
        }
        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = gravity * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        Vector3 direction = new Vector3(horiz, 0, vert).normalized;

        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref velocitySmoothing, rotationSpeed);
            transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir * speed * Time.deltaTime);
        }

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
        if (Velocity.y > 0 && isGrounded == false)
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", true);
        }

        if (isGrounded && horiz == 0 && vert == 0)
        {
            animator.SetBool("running", false);
            animator.SetBool("jumping", false);
        }
    }
}
