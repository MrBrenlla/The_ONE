using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{

    public int speed;
    public int color;
    public float maxRange;

    bool returning=false;
    bool fly = true;

    Vector3 dir;

    public FeatherManager manager;


    // Start is called before the first frame update
    void Start()
    {
        CalculateDir();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (returning)
        {
            dir = (manager.featherSpawn.position - transform.position).normalized;
        }
        if(fly || returning) transform.Translate(dir*speed*Time.deltaTime,Space.World);
        if ((transform.position - manager.featherSpawn.transform.position).magnitude > maxRange) Return();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && !returning)
        {
            fly = false;
        }
        else if (returning)
        {
            manager.Restore(this);
            Destroy(gameObject);
        }
    }

    public void Return()
    {
        returning=true;
        transform.LookAt(manager.featherSpawn);
    }

    void CalculateDir()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            dir = (hit.point - transform.position).normalized;
            transform.LookAt(hit.point);
        }
        else
        {
            dir = Camera.main.transform.forward;
            transform.rotation = Camera.main.transform.rotation;
        }

    }

}
