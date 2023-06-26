using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] bool ForceFreeMode = false;

    public Slider music;
    public Slider efects;

    private bool firstChange = true;

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
        float aux;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("MusicVol",out aux);
        music.value = aux;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("EfectsVol", out aux );
        efects.value = aux;
    }
    public void StartGame()
    {
        Pick.Reset();
        FeatherManager.Reset();
        TextManager.Reset();
        JustOnce.Reset();
        Respawner.Reset();
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
        Respawner.Reset();
        LoadScene.SceneLoad("Raices");
    }

    public void EnterAjustes()
    {
        GetComponent<Animator>().SetBool("Ajustes",true);
    }

    public void ExitAjustes()
    {
        GetComponent<Animator>().SetBool("Ajustes", false);
    }

    public void MusicVolume()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("MusicVol", music.value);
    }

    public void EfectsVolumen()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("EfectsVol",efects.value);
        if(!firstChange) FMODUnity.RuntimeManager.PlayOneShot("event:/Efects/Beep1", transform.position);
        else firstChange = false;
    }
}
