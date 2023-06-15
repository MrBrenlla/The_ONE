using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    
    static List<Vector3> spawns = new List<Vector3>();

    void Start()
    {
        if (spawns.Contains(transform.position))
        {
            spawns.Remove(transform.position);
            GameObject.Find("Player").transform.position = transform.position;
        }
    }

    public void Add()
    {
        spawns.Add(transform.position);
    }
}
