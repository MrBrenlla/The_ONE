using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EasyReadNormalText : MonoBehaviour
{
    private Text t;

    public Color normal;
    public Color easy;

    private void Start()
    {
        t = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (EasyRead.easyReadEnabled)
        {
            t.color = easy;
        }
        else
        {
            t.color = normal;
        }
    }
}
