using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCreator : MonoBehaviour
{
    public GameObject Dust;

    private void OnTriggerEnter(Collider other)
    {
        GameObject newDust = Instantiate(Dust);
        newDust.transform.position = this.transform.position;
        Destroy(newDust,5);
    }
}
