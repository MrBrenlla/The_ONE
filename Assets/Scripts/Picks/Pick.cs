using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Pick : MonoBehaviour
{

    protected GameObject player;
    protected GameObject other;
    public bool dontRestore = true;
    static private List<(Vector3,int)> picks = new List<(Vector3, int)>();

    private void Start()
    {
        if (picks.Contains((transform.position, SceneManager.GetActiveScene().buildIndex))) Destroy(this.gameObject);
        StartExtension();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Activate();
            if (dontRestore) picks.Add((transform.position, SceneManager.GetActiveScene().buildIndex));
            Destroy(this.gameObject);
        }
        else
        {
            this.other = other.gameObject;
            OtherCollision();
        }

    }

    public abstract void Activate();

    public static void Reset()
    {
        picks.Clear();
    }

    public virtual void OtherCollision()
    {

    }

    public virtual void StartExtension()
    {

    }

}
