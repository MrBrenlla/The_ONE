using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
   public static string level;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(this.MakeTheLoad(level));
    }

    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
