using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Profiling.RawFrameDataView;

public class FungusSoundSolver : MonoBehaviour
{
    public WriterAudio writerAudio;

    private void Update()
    {
        print("next event "+writerAudio.nextEvent);
        if (writerAudio.nextEvent != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/" + writerAudio.nextEvent);
            writerAudio.nextEvent = null;
        }
    }



}
