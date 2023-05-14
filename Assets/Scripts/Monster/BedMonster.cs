using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedMonster : MonoBehaviour
{
    private bool isSleeping = true;
    private float maxSleepingTime = 100f;
    private float sleepingTime = 100f;
    private float normalUnsleepRate = 1f;
    float timer = 0f;
    float scratchTimer = 0f;
    private float acceleratedUnsleepRate = 7f;
    [SerializeField]
    Lamp l;
    [SerializeField]
    Player player;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip scratchOne;
    [SerializeField]
    AudioClip scratchTwo;
    [SerializeField]
    GameObject blackScreen;
    [SerializeField]
    GameObject jumpscareFace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(sleepingTime <= 0) { 
            isSleeping = false;
            sleepingTime = 0;
            scratchTimer += Time.deltaTime;
            if (scratchTimer >= 15) {
                PlayScratchSound();
                scratchTimer = 0;
            }
        }

        if (l.TurnOn)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1)
            {
                sleepingTime -= acceleratedUnsleepRate;
                timer = 0;
            }
        }

        else {
            timer += Time.deltaTime;
            if (timer >= 0.1)
            {
                sleepingTime -= normalUnsleepRate;
                timer = 0;
            }
        }

        if (player.isPlayingMusic) {
            timer += Time.deltaTime;
            if (timer >= 0.1) {
                sleepingTime += acceleratedUnsleepRate;
            }
            if (sleepingTime >= 0.5 * maxSleepingTime) {
                isSleeping = true;
            }
        }

        if (player.isHiding && !isSleeping) {
            Jumpscare();
        }
    }

    public void Jumpscare() {
        blackScreen.SetActive(true);
        jumpscareFace.SetActive(true);
        Invoke("LoadCurrentScene", 1f);
    }

    void LoadCurrentScene() {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    void PlayScratchSound() {
        audioSource.clip = scratchTwo;
        audioSource.Play();
    }
}
