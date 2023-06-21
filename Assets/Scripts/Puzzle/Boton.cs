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
            count++;
            state = true;
            this.animator.SetBool("State", true);
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Feather" )
        {
            count--;
            if (count == 0)
            {
                state = false;
                this.animator.SetBool("State", false);
            }
        }
    }

    public bool Check()
    {
        return state == expected;
    }
}
