using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableLamp : MonoBehaviour
{

    [SerializeField]
    GameObject lampOn;
    [SerializeField]
    GameObject lampOff;
    [SerializeField]
    GameObject light;
    [SerializeField]
    AudioSource audioSource;

    public bool turnedOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnedOn)
        {
            lampOn.SetActive(true);
            lampOff.SetActive(false);
            light.GetComponent<Light>().intensity = 2;
        }
        else {
            lampOn.SetActive(false);
            lampOff.SetActive(true);
            light.GetComponent<Light>().intensity = 0;
        }
    }

    public bool IsOn() {
        return turnedOn;
    }

    public void ToggleLamp() {
        audioSource.Play();
        turnedOn = !turnedOn;
    }
}
