using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    [SerializeField] private bool expected;

    private bool state;

    private int count;

    private Animator animator;

    private void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Feather")
        {
            if(!state) FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/ButtonOn",transform.position);
            count++;
            state = true;
            this.animator.SetBool("State", true);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Feather" )
        {
            print("exit "+count);
            count--;
            if (count == 0)
            {
                state = false;
                this.animator.SetBool("State", false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/ButtonOff", transform.position);
            }
        }
    }

    public bool Check()
    {
        return state == expected;
    }
}
