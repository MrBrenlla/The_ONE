using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fungus
{
    public class EmitSound : MonoBehaviour
    {

        public void PlaySound(string FMODEvent)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/" + FMODEvent, transform.position);
        }
    }
}
