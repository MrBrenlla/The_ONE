using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detected : MonoBehaviour
{

    public Material baseMat;
    public Material invertedMat;

    public float maxPow=5;
    public float minPow=0;
    public float time=1;
    private float pow=0;

    private bool grow=false;

    private Renderer r;

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<Renderer>();
        r.material=Instantiate(baseMat);
        r.material.SetFloat("_Power", minPow);

    }



    // Update is called once per frame
    void Update()
    {
        if(pow > minPow)
        {
            float mult=Time.deltaTime/time;
            float dif = maxPow-minPow;
            pow -= dif*mult;
            if(pow < minPow ) pow = minPow;
            r.material.SetFloat("_Power", pow);
        }
        if (pow == minPow && grow)
        {
            grow = false;
            Destroy(r.material);
            r.material = Instantiate(baseMat);
            pow = maxPow;
            r.material.SetFloat("_Power", pow);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        grow = true;
        Destroy(r.material);
        r.material = Instantiate(invertedMat);
        pow = maxPow;
        r.material.SetFloat("_Power", pow);
    }
}
