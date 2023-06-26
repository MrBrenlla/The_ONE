using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86;

public class PauseMenu : MonoBehaviour
{
    
    private PlayerController playerController;

    public Slider music;
    public Slider efects;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.pauseMenu = this.gameObject;
    }


    private void OnEnable()
    {
        float aux;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("MusicVol", out aux);
        music.value = aux;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("EfectsVol", out aux);
        efects.value = aux;

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

    public void EnterAjustes()
    {
        GetComponent<Animator>().SetBool("Ajustes", true);
    }

    public void ExitAjustes()
    {
        GetComponent<Animator>().SetBool("Ajustes", false);
    }

    public void MusicVolume()
    {
        print("music volumen " + music.value);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicVol", music.value);
    }

    public void EfectsVolumen()
    {
        print("efects volumen " + efects.value);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("EfectsVol", efects.value);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/Beep1", transform.position);
    }
}
