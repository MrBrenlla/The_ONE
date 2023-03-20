using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{

    public float mouseSensitivity;
    float rotX=0;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        rotX = cam.transform.eulerAngles.y;
        transform.rotation=Quaternion.Euler(0, rotX ,0);

    }

    // Update is called once per frame
    void Update()
    {
        float rotation= cam.transform.eulerAngles.y-rotX;
        rotX = cam.transform.eulerAngles.y;

        transform.Rotate(0,rotation,0);
    }
}
