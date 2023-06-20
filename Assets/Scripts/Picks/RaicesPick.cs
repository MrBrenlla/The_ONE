using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class RaicesPick : Pick
{
    public PlayableDirector derrumbe;
    public int color;
    public override void Activate()
    {

        player.GetComponentInChildren<FeatherManager>().AddFeather(color);
        derrumbe.Play();

    }

    override public void OtherCollision()
    {
        if(other.tag=="Decor") Destroy(this.gameObject);
    }

}
