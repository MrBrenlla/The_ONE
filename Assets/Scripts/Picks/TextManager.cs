using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    
    public Animator[] textAnimators = new Animator[8];
    public static string[] textString = {Texts.t1,Texts.t2,Texts.t3,Texts.t4,Texts.t5,Texts.t6,Texts.t7,Texts.t8 };
   
    
    public static bool[] textActive = { false, false, false, false, false, false, false, false};
    
    private static int min = 8;
    private static int max = -1;
     

    /*
    public static bool[] textActive = { true, true, true, true, true, true, true, true };

    private static int min = 0;
    private static int max = 7;
    */

    private int actual;


    public Button nextB;
    public Button backB;

    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI num;

    private PlayerController playerController;


    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerController.textsMenu = this.gameObject;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Next();
        if (Input.GetKeyDown(KeyCode.A)) Back();

        if (actual <= min) backB.interactable = false;
        else backB.interactable = true;

        if (actual >= max) nextB.interactable = false;
        else nextB.interactable = true;
    }

    private void OnEnable()
    {
        playerController.Stop();
        actual = min;
        if(actual>-1)textMeshPro.text = textString[actual];
        else textMeshPro.text = "Error 404";
        if(actual > -1) num.text=(actual+1)+"/8";
        else num.text = "NULL/8";
        for(int i = 0; i < 8; i++) textAnimators[i].gameObject.SetActive(textActive[i]);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnDisable()
    {
        playerController.Go();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Next()
    {
        if (actual >= max) return;
        textAnimators[actual].SetTrigger("Out");
        actual++;
        while (!textActive[actual]) actual++;
        textMeshPro.text = textString[actual];
        num.text = (actual + 1) + "/8";
    }

    public void Back()
    {
        if (actual <= min) return;
        actual--;
        while (!textActive[actual]) actual--;
        textMeshPro.text = textString[actual];
        num.text = (actual + 1) + "/8";
        textAnimators[actual].SetTrigger("In");
    }


    public static void Found(int i)
    {
        textActive[i] = true;
        if(min>i) min = i;
        if(max<i) max = i;
    }

    public static bool HasAny()
    {
        return max > -1;
    }

    public static void Reset()
    {
        for (int i = 0; i < 8; i++) textActive[i] = false;
        min = 8;
        max = -1;
    }
    
}
