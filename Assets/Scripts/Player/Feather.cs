using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{

    public float speed;
    public int color;
    public float maxRange;

    bool returning=false;
    bool fly = true;

    Vector3 dir;

    public FeatherManager manager;
    public GameObject platformCollider;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        CalculateDir();
        platformCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (returning)
        {
            dir = (manager.featherSpawn.position - transform.position).normalized;
            transform.LookAt(manager.featherSpawn);
        }
        if(fly || returning) rb.velocity=dir*speed;
        if ((transform.position - manager.featherSpawn.transform.position).magnitude > maxRange) Return();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player" && !returning)
        {
            fly = false;
            Platform();
        }
        else if (returning && other.tag == "Player")
        {
            manager.Restore(this);
            Destroy(gameObject);
        }
    }

    void Platform()
    {
        transform.localScale = Vector3.one*10;
        platformCollider.SetActive(true);
        rb.isKinematic = true;
    }

    public void Return()
    {
        if (returning) return;
        speed = speed * 1.5f;
        transform.localScale = Vector3.one;
        platformCollider.SetActive(false);
        returning =true;
        rb.isKinematic = false;
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
