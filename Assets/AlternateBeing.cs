using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlternateBeing : MonoBehaviour
{
    public float fadePercent = 0;
    [SerializeField]
    Player p;
    float timer = 0;
    float aliveTimer = 5;
    private bool isSpawned = false;
    public bool isActive = false;
    [SerializeField]
    DoorMonster dm;
    [SerializeField]
    Button SpawnerButton;
    [SerializeField]
    Text fadePercentText;
    [SerializeField]
    GameObject beingSilhouette;
    [SerializeField]
    StageManager sm;
    public enum State { 
        NotExist,
        LowHealth,
        OptimalHealth,
        Deceased
    }
    State currentState = State.NotExist;

    // Update is called once per frame
    void Update()
    {
        if (dm.inRoom && isSpawned && !isActive) {
            isActive = true;
            currentState = State.OptimalHealth;
            SpawnerButton.enabled = false;
            beingSilhouette.SetActive(true);
            sm.StartStageTwoTimer();
        }

        if (fadePercent < 100 && isActive)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                if (p.consciousnessLevel > 2)
                {
                    fadePercent += 0.1f;
                }

                else if (p.consciousnessLevel == 2)
                {
                    fadePercent += 0.05f;
                }

                timer = 0;
            }
        }
        else {
            currentState = State.Deceased;
        }

        fadePercentText.text = fadePercent.ToString() + "%";
    }

    public void Spawn() {
        isSpawned = true;
        Invoke("Despawn", aliveTimer); 
    }

    public void Despawn() {
        fadePercent = 0;
        isSpawned = false;
    }

}
