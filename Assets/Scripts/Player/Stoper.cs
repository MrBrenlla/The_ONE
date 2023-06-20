using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoper : MonoBehaviour
{
    public PlayerController controller;

    public void PlayerGo()
    {
        controller.Go(true);
    }

    public void PlayerStop()
    {
        controller.Stop(true);
    }
}
