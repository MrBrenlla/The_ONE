using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasyRead : MonoBehaviour
{
    static public bool easyReadEnabled { private set; get; }

    private void Start()
    {
        GetComponent<Toggle>().isOn = easyReadEnabled;
    }

    public void Toggle()
    {
        easyReadEnabled = GetComponent<Toggle>().isOn;
    }
}
