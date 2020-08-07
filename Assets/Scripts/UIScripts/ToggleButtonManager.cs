using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonManager : MonoBehaviour
{
    public string prefName;
    void Start() {
        Toggle toggle = GetComponent<Toggle>();

        int on = PlayerPrefs.GetInt(prefName, 1);

        toggle.isOn = on == 0;
    }
}
