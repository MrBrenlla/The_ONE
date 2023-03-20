using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float speedMultiplier;
    public float jumpForce;


    bool stop=false;

    float speedX,speedZ;

    FeatherManager featherManager;

    void Start()
    {
        featherManager = GetComponent<FeatherManager>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            speedZ = 0;
            if (Input.GetKey(KeyCode.W)) speedZ = walkSpeed;
            else if (Input.GetKey(KeyCode.S)) speedZ = -walkSpeed;

            speedX = 0;
            if (Input.GetKey(KeyCode.D)) speedX = walkSpeed;
            else if (Input.GetKey(KeyCode.A)) speedX = -walkSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speedZ *= speedMultiplier;
                speedX *= speedMultiplier;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }


            transform.Translate(speedX * Time.deltaTime, 0, speedZ * Time.deltaTime);

            //Disparo

            if (Input.GetMouseButtonDown(0))
            {
                Shot();
            }

            if (Input.GetMouseButtonDown(1))
            {
                ReturnShot();
            }

        }
    }

    void Shot()
    {
        featherManager.Shot();

    }

    void ReturnShot()
    {
        featherManager.ReturnShot();
    }

    void Jump()
    {
        featherManager.Jump(jumpForce);
    }

    public void StopOrStart()
    {
        stop = !stop;
    }
}
