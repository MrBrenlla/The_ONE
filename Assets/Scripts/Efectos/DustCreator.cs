using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCreator : MonoBehaviour
{
    public GameObject Dust;

    public bool enableDust = true;

    public string FMODEventName;

    private void OnTriggerEnter(Collider other)
    {
        if (enableDust)
        {
            GameObject newDust = Instantiate(Dust);
            newDust.transform.position = this.transform.position;
            Destroy(newDust, 5);
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/" + FMODEventName,transform.position);
    }
}
