using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float speedMultiplier;
    public float jumpForce;
    public float maxRotation;
    public float maxGroundDistance;
    public GameObject cam;
    public GameObject lookAt;
    public GameObject model;

    Animator animator;
    Rigidbody rb;

    bool stop=false;

    float speedX,speedZ;

    public FeathersUI feathersUI;

    FeatherManager featherManager;

    public bool torch;

    private void Awake()
    {
        featherManager = GetComponentInChildren<FeatherManager>();
        featherManager.feathersUI = feathersUI;
        featherManager.maxGroundDistance = maxGroundDistance;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        rb = GetComponentInChildren<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }


    // Update is called once per frame
    void Update()
    {
        float mult = 1;
        animator.SetBool("Torch", torch);
        if (!stop)
        {
            speedZ = 0;
            if (Input.GetKey(KeyCode.W)) speedZ = 1;
            else if (Input.GetKey(KeyCode.S)) speedZ = -1;

            speedX = 0;
            if (Input.GetKey(KeyCode.D)) speedX = 1;
            else if (Input.GetKey(KeyCode.A)) speedX = -1;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetBool("CanRun", true);
                mult= speedMultiplier;
            }else animator.SetBool("CanRun", false);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.E) && !animator.GetCurrentAnimatorStateInfo(1).IsName("Throw")) animator.SetTrigger("Whistle");

            animator.SetBool("Moving", speedX != 0 || speedZ != 0);
 

            //Girar personaje

            Vector3 right = transform.position-cam.transform.position;
            Vector3 front = new Vector3(right.z, right.y, -right.x);

            Vector3 movement = (speedX * front) + (speedZ  * right);
            movement.y = 0;

            Vector3 target = Vector3.RotateTowards(model.transform.forward, movement, Time.deltaTime*maxRotation,99999);
            lookAt.transform.localPosition = target;
            model.transform.LookAt(lookAt.transform);    

            //Andar

            movement = lookAt.transform.position - transform.position;
            movement = movement.normalized * mult * walkSpeed;
            movement.y=rb.velocity.y;

            rb.velocity = movement;

            //transform.Translate(movement.normalized * mult * walkSpeed * Time.deltaTime);



            //Disparo

            if (Input.GetMouseButtonDown(0))
            {
                if (animator.GetCurrentAnimatorStateInfo(1).IsName("None")) Shot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReturnShot();
            }

        }
    }

    void Shot()
    {
        featherManager.TryShot();

    }

    void ReturnShot()
    {
        featherManager.ReturnShot();
    }

    void Jump()
    {
        if(featherManager.Jump(jumpForce))animator.SetTrigger("Jump");
    }

    public void StopOrStart()
    {
        stop = !stop;
    }
}
