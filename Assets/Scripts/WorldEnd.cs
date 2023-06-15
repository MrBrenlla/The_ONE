using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEnd : MonoBehaviour
{

    public static Vector3 lastGround;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            lastGround += (lastGround - other.transform.position).normalized *10;
            other.transform.position = lastGround;
        }
    }
}
