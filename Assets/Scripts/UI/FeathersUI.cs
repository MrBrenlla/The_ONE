using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeathersUI : MonoBehaviour
{
    public Image img0, img1, img2, img3;
    public Sprite white, black, gold, none;

    Image[] images;
    Sprite[] dictionary;

    private void Start()
    {
        images = new Image[] { img0, img1, img2, img3 };
        dictionary = new Sprite[] { white, black, gold};
    }

    public void Refresh(int[] state)
    {
        for(int i = 0; i < state.Length; i++)
        {
            images[i].sprite = dictionary[state[i]];
        }
        for(int i = state.Length; i < 4; i++)
        {
            images[i].sprite = none;
        }
    }
}
