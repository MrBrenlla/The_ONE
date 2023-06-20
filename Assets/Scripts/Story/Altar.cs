using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    public GameObject dialogo;

    int state = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (FeatherManager.allFeathers.Count < 4) state = 0;
            else if (FeatherManager.allFeathers.Contains(2)) state =1;
            else state=2;
            dialogo.SetActive(true);
        }
    }

    public int GetState()
    {
        return state;
    }

    public void ActivarFinal()
    {
        LoadScene.SceneLoad("Finale");
    }

    public void FreeMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
