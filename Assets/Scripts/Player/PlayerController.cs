using Cinemachine;
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

    int stop=0;

    float speedX,speedZ;

    public FeathersUI feathersUI;

    public GameObject textsMenu;

    public GameObject pauseMenu;

    public GameObject pointer;

    FeatherManager featherManager;

    public bool torch;

    public bool isTalking = false;

    private CinemachineFreeLook freeLook;

    private float mult = 1;

    private void Awake()
    {
        

        featherManager = GetComponentInChildren<FeatherManager>();
        featherManager.feathersUI = feathersUI;
        featherManager.maxGroundDistance = maxGroundDistance;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        freeLook = FindObjectOfType<CinemachineFreeLook>();
        animator = GetComponentInChildren<Animator>();

        rb = GetComponentInChildren<Rigidbody>();
        animator.SetBool("Torch", torch);
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q) && TextManager.HasAny() && !pauseMenu.activeSelf && !isTalking)
        {
            textsMenu.SetActive(!textsMenu.activeSelf);
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

        }

        
        
        if (IsGoing())
        {
            speedZ = 0;
            if (Input.GetKey(KeyCode.W)) speedZ = 1;
            else if (Input.GetKey(KeyCode.S)) speedZ = -1;

            speedX = 0;
            if (Input.GetKey(KeyCode.D)) speedX = 1;
            else if (Input.GetKey(KeyCode.A)) speedX = -1;

            if (Input.GetKeyDown(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                animator.SetBool("CanRun", true);
                mult= speedMultiplier;
            }

            if(!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
            {
                mult = 1;
                animator.SetBool("CanRun", false);
            }

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

    public void Stop(bool talking = false)
    {
        stop++;
        animator.SetBool("Moving", false);
        pointer.SetActive(false);
        freeLook.enabled = false;
        isTalking |= talking;
        rb.velocity = Vector3.zero;
        mult = 1;
        animator.SetBool("CanRun", false);
    }

    public void Go(bool talkingEnd = false)
    {
        stop--;
        if (stop <= 0)
        {
            freeLook.enabled = true;
            pointer.SetActive(true);
        }
        if(talkingEnd) isTalking = false;
    }

    public bool IsGoing()
    {
        return stop <= 0;
    }
}
