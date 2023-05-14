using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthfill;

    [SerializeField]
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        healthfill = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthfill.fillAmount = player.GetSanity() / 100;
    }
}
