using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustOnce : MonoBehaviour
{
    private static List<(Vector3, int, string)> objects = new List<(Vector3, int,string)>();

    private void Awake()
    {
        if (objects.Contains((transform.position, UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, gameObject.name))) Destroy(gameObject);
        else objects.Add((transform.position, UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex, gameObject.name));
    }

    public static void Reset()
    {
        objects.Clear();
    }
}
