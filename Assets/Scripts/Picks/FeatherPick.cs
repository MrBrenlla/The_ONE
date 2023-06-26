using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherPick : Pick
{
    public int color;
    public override void Activate()
    {
        
        player.GetComponentInChildren<FeatherManager>().AddFeather(color);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/Collect");

    }

}
