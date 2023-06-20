using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPick : Pick
{

    public int textId;

    public override void Activate()
    {
        TextManager.Found(textId);
    }
}
