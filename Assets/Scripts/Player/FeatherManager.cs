using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public int[] startFeathers;
    public Feather White, Black, Gold;
    public  Material[] feathersMaterials;
    private Queue<int> readyFeathers, jumpedFeathers;
    private List<Feather> shotedFeathers;

    public Transform featherSpawn;

    public bool inGround = false ;

    public FeathersUI feathersUI;

    public Rigidbody rb;

    public GameObject wings;
    public float waitTime;
    private bool flaying = false;

    public float maxGroundDistance;

    private Feather nextShot;

    private Animator animator;

    private void Update()
    {
        GroundCheck();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        readyFeathers = new Queue<int>();
        foreach (int feather in startFeathers) readyFeathers.Enqueue(feather);

        feathersUI.Refresh(readyFeathers.ToArray());

        shotedFeathers = new List<Feather>();
        jumpedFeathers = new Queue<int>();
    }


    public bool Jump(float jumpForce)
    {
        int feather;
        if (inGround)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
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
        int[] feathers = readyFeathers.ToArray();
        feathersUI.Refresh(feathers);
        yield return new WaitForSeconds(waitTime);
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        yield return new WaitForSeconds(waitTime*8);
        flaying =false;
    }

    void RestoreJumps()
    {
        if(jumpedFeathers.Count == 0) return;
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

    public bool TryShot()
    {
        int f;
        if (readyFeathers.TryDequeue(out f))
        {
            nextShot = GetFeather(f);
            nextShot.manager = this;
            animator.SetTrigger("Throw");
            return true;
        }
        return false; 
    }

    public void Shot()
    {
        shotedFeathers.Add(Instantiate(nextShot, featherSpawn.position, Quaternion.identity));
        feathersUI.Refresh(readyFeathers.ToArray());
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

    private void GroundCheck()
    {
        LayerMask mask;
        mask = (3);
        mask |= (9);
        Vector3 origin = transform.position + (Vector3.up * 0.2f);
        Debug.DrawRay(origin,Vector3.down * maxGroundDistance);
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit,maxGroundDistance,mask,QueryTriggerInteraction.Ignore))
        {
            print("hit " + hit.collider.tag+"    "+hit.collider.gameObject.name);
            if (hit.collider.tag != "NotJumpeable" && hit.collider.tag != "Whistle" && hit.collider.tag != "Player" && hit.distance < maxGroundDistance) inGround = true; 
            else inGround = false; 
        }
        else inGround=false;
        animator.SetBool("Ground", inGround);
        if (inGround)
        {
            RestoreJumps();
            WorldEnd.lastGround=transform.position;
        }
    }
}



