using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPick : Pick
{

    public int textId;
    [SerializeField] private GameObject dialog;

    public override void Activate()
    {
        int cont = 0;
        TextManager.Found(textId);
        foreach (bool b in TextManager.textActive) if (b) cont++;
        if (cont ==1 ) Instantiate(dialog).GetComponent<Stoper>().controller=player.GetComponent<PlayerController>();
        print("textos: "+cont);
    }



}
