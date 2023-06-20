using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] bool ForceFreeMode = false;

    public GameObject freeMode;
    private void Start()
    {
        if(PlayerPrefs.GetInt("Completed")==1 || ForceFreeMode)
        {
            freeMode.SetActive(true);
        }
        else
        {
            freeMode.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        Pick.Reset();
        FeatherManager.Reset();
        TextManager.Reset();
        JustOnce.Reset();
        LoadScene.SceneLoad("Raices");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void FreeMode()
    {
        Pick.Reset();
        FeatherManager.FreeModeReset();
        TextManager.Reset();
        JustOnce.Reset();
        LoadScene.SceneLoad("Raices");
    }
}
