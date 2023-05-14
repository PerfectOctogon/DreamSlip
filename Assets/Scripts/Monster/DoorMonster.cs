using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorMonster : MonoBehaviour
{

    private float minWaitTime = 30;
    private float maxWaitTime = 100;
    private float waitTime = 45;
    private float timer = 0;
    private int knockWaitTime = 5;
    [HideInInspector]
    private int inRoomWaitTime = 5;
    public bool inRoom;
    private bool isKnocking;
    private bool processStarted = false;
    private float suspicionRate = 1;

    [SerializeField]
    Player p;
    [SerializeField]
    Lamp l;
    [SerializeField]
    TableLamp tl;
    [SerializeField]
    Door d;
    [SerializeField]
    GameObject blackScreen;
    [SerializeField]
    GameObject jumpscareFace;



    // Update is called once per frame
    void Update()
    {
        

        if (!processStarted)
        {

            if (waitTime <= 0) {
                isKnocking = true;
            }

            timer += Time.deltaTime;
            if (timer > 1) {
                waitTime -= suspicionRate;
                if (tl.IsOn()) {
                    waitTime -= suspicionRate;
                }
                if (p.isPlayingMusic) {
                    waitTime -= suspicionRate;
                }
                if (l.TurnOn) {
                    waitTime -= suspicionRate;
                }
                if (p.consciousnessLevel > 2 && waitTime < maxWaitTime) {
                    waitTime += suspicionRate;
                }
                timer = 0;
            }
            CheckKnocking();
        }

        //print(waitTime);

        if (!p.isHiding && inRoom)
        {
            Jumpscare();
        }
        else if (p.isHiding && inRoom && l.TurnOn) {
            int tolerance = Random.Range(0, 10);
            int chance = Random.Range(0, 4);
            if (tolerance < chance) {
                Jumpscare();
            }
        }

    }

    private void WaitTime() {
        //Debug.Log("Process started!");
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        processStarted = false;
    }

    private void CheckKnocking() {
        if (isKnocking) {
            isKnocking = false;
            Invoke("CheckGoIntoRoom", knockWaitTime);
            //Debug.Log("Knocked!");
            d.Knock();
            processStarted = true;
        }
    }

    private void CheckGoIntoRoom() {
        int suspicionLevel = 0;
        if (p.isPlayingMusic) { suspicionLevel += 4; }
        if (tl.IsOn()) { suspicionLevel += 2; }
        if (l.TurnOn) { suspicionLevel += 3; }

        int tolerance = Random.Range(1, 4);

        if (suspicionLevel > tolerance)
        {
            //Open the door and give the user some reaction time to hide
            d.Open();
            Invoke("EnterRoom", inRoomWaitTime);
        }
        else {
            WaitTime();
        }
    }

    private void EnterRoom() {
        inRoom = true;
        int exitTime = Random.Range(4, 9);
        Invoke("ExitRoom", exitTime);
    }

    private void ExitRoom() {
        d.Close();
        inRoom = false;
        WaitTime();
    }

    void LoadCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
    public void Jumpscare() {
        blackScreen.SetActive(true);
        jumpscareFace.SetActive(true);
        Invoke("LoadCurrentScene", 1);
    }

    public float GetMaxWaitTime() {
        return maxWaitTime;
    }

    public float GetWaitTime()
    {
        return waitTime;
    }

}
