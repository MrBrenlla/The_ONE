using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EasyReadTextMeshPro : MonoBehaviour
{
    private TextMeshProUGUI t;

    public Color normal;
    public Color easy;

    private void Start()
    {
        t=GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if(EasyRead.easyReadEnabled)
        {
            t.color=easy;
        }
        else
        {
            t.color = normal;
        }
    }
}
