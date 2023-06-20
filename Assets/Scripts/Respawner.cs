using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawner : MonoBehaviour
{
    private (Vector3,int) id;
    static List<(Vector3,int)> spawns = new List<(Vector3, int)>();

    void Start()
    {
        id = (transform.position, SceneManager.GetActiveScene().buildIndex);
        if (spawns.Contains(id))
        {
            spawns.Remove(id);
            GameObject.Find("Player").transform.position = transform.position;
        }
    }

    public void Add()
    {
        spawns.Add(id);
    }
}
