using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Boton> Botons = new List<Boton>();

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool open = true;
        foreach (Boton boton in Botons)
        {
            open &= boton.Check();
        }
        if(open)
        {
            animator.SetTrigger("Open");
            Destroy(this);
        }
    }
}
