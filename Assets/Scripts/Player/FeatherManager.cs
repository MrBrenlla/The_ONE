using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FeatherManager : MonoBehaviour
{
    public int[] startFeathers;
    public Feather White, Black, Gold;
    public  Material[] feathersMaterials;
    public static Queue<int> allFeathers {get; private set;} 
    private Queue<int> readyFeathers;
    private List<int> jumpedFeathers;
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

    public GameObject tooMuchFeathers;

    private void Update()
    {
        GroundCheck();
    }

    private void Start()
    {
        if(allFeathers!=null) foreach (int f in allFeathers) print("feather " + f);
        StartQueues();
        foreach (int f in allFeathers) print("feather 2ª "+f);
        animator = GetComponent<Animator>();
        
        foreach (int feather in startFeathers) AddFeather(feather);

        feathersUI.Refresh(readyFeathers.ToArray());

        shotedFeathers = new List<Feather>();
        jumpedFeathers = new List<int>();
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
            foreach (int f in readyFeathers.ToArray()) print($"{f}");
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
        jumpedFeathers.Add(feather);
        int[] feathers = readyFeathers.ToArray();
        feathersUI.Refresh(feathers);
        yield return new WaitForSeconds(waitTime);
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/Volar", transform.position);
        yield return new WaitForSeconds(waitTime*8);
        flaying =false;
    }

    void RestoreJumps()
    {
        if(jumpedFeathers.Count == 0) return;
        foreach(int f in jumpedFeathers)readyFeathers.Enqueue(f);
        jumpedFeathers.Clear();
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
        bool lastGround = inGround;
        LayerMask mask;
        mask = (3);
        mask |= (9);
        Vector3 origin = transform.position + (Vector3.up * 0.2f);
        Debug.DrawRay(origin,Vector3.down * maxGroundDistance);
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit,maxGroundDistance,mask,QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.tag != "NotJumpeable" && hit.collider.tag != "Whistle" && hit.collider.tag != "Player" && hit.distance < maxGroundDistance) inGround = true; 
            else inGround = false; 
        }
        else inGround=false;
        animator.SetBool("Ground", inGround);
        if (inGround && !lastGround) RestoreJumps();
        if (inGround) WorldEnd.lastGround=transform.position;
        
    }

    public bool AddFeather(int feather)
    {
        if (allFeathers.Contains(2) && allFeathers.Count>=4) RemoveGold();
        if (allFeathers.Count < 4)
        {
            allFeathers.Enqueue(feather);
            readyFeathers.Enqueue(feather);
            feathersUI.Refresh(readyFeathers.ToArray());
            return true;
        }
        Instantiate(tooMuchFeathers);
        return false;
    }

    private void StartQueues()
    {
        readyFeathers = new Queue<int>();
        if (allFeathers == null) allFeathers = new Queue<int>();
        else foreach (int feather in allFeathers.ToArray()) readyFeathers.Enqueue(feather);

    }   

    private void RemoveGold()
    {
        List<int> feathers = new List<int>(allFeathers.ToArray());
        feathers.Remove(2);
        allFeathers = new Queue<int>(feathers.ToArray());
        if (readyFeathers.Contains(2))
        {
            feathers = new List<int>(readyFeathers.ToArray());
            feathers.Remove(2);
            readyFeathers = new Queue<int>(feathers.ToArray());
        }
        else if (jumpedFeathers.Contains(2)) jumpedFeathers.Remove(2);
        else
        {
            Feather gold=null;
            foreach(Feather f in shotedFeathers) if(f.color==2) gold = f;
            shotedFeathers.Remove(gold);
            Destroy(gold.gameObject);
        }
    }
    
    public static void Reset()
    {
        allFeathers = new Queue<int>();
    }

    public static void FreeModeReset()
    {
        allFeathers = new Queue<int>();
        for(int i = 0;i<4;i++) allFeathers.Enqueue(2);
    }
}



