using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public string level;

    public Respawner respawner;

    public static void SceneLoad(string sceneName)
    {
        Loading.level = sceneName;
        SceneManager.LoadScene("Loading");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (respawner != null) respawner.Add();
            SceneLoad(level);
        }
    }
}
