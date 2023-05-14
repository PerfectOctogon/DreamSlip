using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsciousnessUIController : MonoBehaviour
{
    GameObject lowText;
    GameObject highText;
    GameObject midText;

    private void Start()
    {
        lowText = gameObject.GetComponent<RectTransform>().Find("LowText").gameObject;
        midText = gameObject.GetComponent<RectTransform>().Find("MidText").gameObject;
        highText = gameObject.GetComponent<RectTransform>().Find("HighText").gameObject;
        CheckChangeConsciousText(2);
    }

    public void CheckChangeConsciousText(int conscious) {

        switch (conscious) {
            case 1:
                lowText.SetActive(true);
                highText.SetActive(false);
                midText.SetActive(false);
                break;
            case 2:
                lowText.SetActive(false);
                highText.SetActive(false);
                midText.SetActive(true);
                break;
            case 3:
                lowText.SetActive(false);
                highText.SetActive(true);
                midText.SetActive(false);
                break;
        }
    }
}
