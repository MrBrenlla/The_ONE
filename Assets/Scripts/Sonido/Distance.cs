using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : MonoBehaviour
{

    private FMODUnity.StudioListener listener;

    // Start is called before the first frame update
    void Start()
    {
        listener = FindObjectOfType<FMODUnity.StudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = (transform.position - listener.transform.position).magnitude;
        if (distance > 60) return;
        else if (distance < 5) FMODUnity.RuntimeManager.StudioSystem.setParameterByName("DistanceMul", 0);
        else FMODUnity.RuntimeManager.StudioSystem.setParameterByName("DistanceMul", (distance - 5) / 55);

        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("DistanceMul", out float aux);
        print("distance " + aux);
    }
}
