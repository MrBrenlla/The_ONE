using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public int[] startFeathers;
    public Feather White, Black, Gold;
    private Queue<int> readyFeathers, jumpedFeathers;
    private List<Feather> shotedFeathers;

    public Transform featherSpawn;

    public bool inGround { get; private set; } = false ;
    int groundCont = 0;

    public FeathersUI feathersUI;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        readyFeathers = new Queue<int>();
        foreach (int feather in startFeathers) readyFeathers.Enqueue(feather);

        feathersUI.Refresh(readyFeathers.ToArray());

        shotedFeathers = new List<Feather>();
        jumpedFeathers = new Queue<int>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
            groundCont++;
            inGround = true;
            RestoreJumps();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            groundCont--;
            if(groundCont == 0) inGround = false;
        }
    }
     
    public bool Jump(float jumpForce)
    {
        int feather;
        if (inGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            return true;
        }
        else if(readyFeathers.TryDequeue(out feather))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpedFeathers.Enqueue(feather);
            feathersUI.Refresh(readyFeathers.ToArray());
            return true;
        }
        return false;
    }

    void RestoreJumps()
    {
        int f;
        while (jumpedFeathers.TryDequeue(out f))
        {
            readyFeathers.Enqueue(f);
        }
        feathersUI.Refresh(readyFeathers.ToArray());
    }

    Feather GetFeather(int f)
    {
        switch (f)
        {
            case 0: return White;
                case 1: return Black;
                  default: return Gold;
        }
    }

    public bool Shot()
    {
        int f;
        if (readyFeathers.TryDequeue(out f))
        {
            Feather feather = GetFeather(f);
            feather.manager = this;
            shotedFeathers.Add(Instantiate(feather, featherSpawn.position, Quaternion.identity));
            feathersUI.Refresh(readyFeathers.ToArray());
            return true;
        }
        return false; 
    }

    private bool Pop(out Feather f)
    {
        f = null;
        if (shotedFeathers.Count<1) return false;
        f = shotedFeathers[0];
        shotedFeathers.RemoveAt(0);
        return true;
    }

    public bool ReturnShot()
    {
        Feather f;
        if (Pop(out f))
        {
            f.Return();
            return true;
        }
        return false;
    }

    public void Restore(Feather f)
    {
        shotedFeathers.Remove(f);
        readyFeathers.Enqueue(f.color);
        feathersUI.Refresh(readyFeathers.ToArray());

    }

}
