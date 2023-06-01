using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public int[] startFeathers;
    public Feather White, Black, Gold;
    [SerializeField] private  Material[] feathersMaterials;
    private Queue<int> readyFeathers, jumpedFeathers;
    private List<Feather> shotedFeathers;

    public Transform featherSpawn;

    [SerializeField] public bool inGround { get; private set; } = false ;

    public FeathersUI feathersUI;

    Rigidbody rb;

    public GameObject wings;
    public float waitTime;
    private bool flaying = false;

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
        if (other.tag != "NotJumpeable")
        {
            inGround = true;
            RestoreJumps();
        }
    }
     
    public bool Jump(float jumpForce)
    {
        int feather;
        if (inGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            inGround = false;
            return true;
        }
        else if(!flaying) if(readyFeathers.TryDequeue(out feather))
        {
            StartCoroutine(Fly(feather, jumpForce));
            return true;
        }
        return false;
    }

    IEnumerator Fly(int feather, float jumpForce)
    {
        flaying = true;
        Renderer[] renderers = wings.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) { r.material = feathersMaterials[feather]; }
        wings.GetComponent<Animator>().SetTrigger("Fly");
        jumpedFeathers.Enqueue(feather);
        feathersUI.Refresh(readyFeathers.ToArray());
        yield return new WaitForSeconds(waitTime);
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        yield return new WaitForSeconds(waitTime*8);
        flaying =false;
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
