using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyReadSymbol : MonoBehaviour
{
    private Image i;

    public Color normal;
    public Color easy;

    private void Start()
    {
        i = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if (EasyRead.easyReadEnabled)
        {
            i.color = easy;
        }
        else
        {
            i.color = normal;
        }
    }
}
