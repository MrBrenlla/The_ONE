using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    
    private PlayerController playerController;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.pauseMenu = this.gameObject;
    }


    private void OnEnable()
    {
        playerController.Stop();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        playerController.Go();
        if (playerController.IsGoing())
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        Time.timeScale = 1;
    }

    public void Continue()
    {
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        LoadScene.SceneLoad("Inicio");
    }
}
