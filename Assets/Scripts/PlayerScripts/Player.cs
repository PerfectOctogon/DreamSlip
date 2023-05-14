using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float sanity = 100;
    private int sanityDropRate = 3;
    private int higherSanityDropRate = 7;
    [SerializeField]
    Collider playerCollider;
    [SerializeField]
    Collider lampTrigger;
    [SerializeField]
    Collider tableLampTrigger;
    [SerializeField]
    Collider computerTrigger;
    [SerializeField]
    Collider bedTrigger;
    float timer = 0;
    [SerializeField]
    TableLamp tl;
    [SerializeField]
    Lamp l;
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    GameObject computerCamera;
    [SerializeField]
    GameObject bedCamera;
    [SerializeField]
    DoorMonster dm;
    /*[SerializeField]
    ComputerController cc;*/
    [SerializeField]
    ConsciousnessUIController cuc;

    private int currentStage = 1;
    private bool isDead = false;
    private bool eKeyDown = false;
    public bool isHiding = false;
    public bool isPlayingMusic = false;
    private bool canInteractWithTableLamp = false;
    private bool canInteractWithComputer = false;
    private bool canInteractWithLamp = false;
    private bool canInteractWithBed = false;
    private bool isUsingComputer = false;
    private bool canChangeConsciousness = true;

    public bool canReceiveInput = true;

    public int consciousnessLevel = 2;

    public KeyCode interactKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (sanity <= 0) {
            isDead = true;
            //Epic insanity cutscene
        }

        timer += Time.deltaTime;
        if (timer >= 0.1f && sanity > 0) {
            if (!tl.IsOn()) {
                sanity = Mathf.Lerp(sanity, sanity - sanityDropRate, 0.1f);
            }
            if (dm.inRoom) {
                sanity = Mathf.Lerp(sanity, sanity - sanityDropRate, 0.1f);
            }
            if (l.TurnOn && sanity < 100) {
                sanity = Mathf.Lerp(sanity, sanity + 5, 0.1f);
            }
            if (consciousnessLevel <= 1)
            {
                sanity = Mathf.Lerp(sanity, sanity - 1, 0.1f);
            }
            else if (consciousnessLevel > 2 && sanity < 100) {
                sanity = Mathf.Lerp(sanity, sanity + 1, 0.1f);
            }
            timer = 0;
        }

        /*if (!tl.IsOn()) {
            timer += Time.deltaTime;
            if (timer >= 0.1f && sanity > 0)
            {
                sanity = Mathf.Lerp(sanity, sanity - sanityDropRate, 0.1f);
                timer = 0;
            }
        }

        if (l.TurnOn) {
            timer += Time.deltaTime;
            if (timer >= 0.01f && sanity <= 100)
            {
                sanity = Mathf.Lerp(sanity, sanity + 1, 0.1f);
                timer = 0;
            }
        }*/


        if (canReceiveInput)
        {
            CheckInteraction();
        }
    }

    public float GetSanity() {
        return sanity;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other == tableLampTrigger) {
            canInteractWithTableLamp = true;
        }

        if (other == computerTrigger) {
            canInteractWithComputer = true;
        }

        if (other == lampTrigger) {
            canInteractWithLamp = true;
        }

        if (other == bedTrigger) {
            canInteractWithBed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == tableLampTrigger)
        {
            canInteractWithTableLamp = false;
        }

        if (other == computerTrigger)
        {
            canInteractWithComputer = false;
        }

        if (other == lampTrigger)
        {
            canInteractWithLamp = false;
        }

        if (other == bedTrigger)
        {
            canInteractWithBed = false;
        }
    }

    private void CheckInteraction() {
        if (canInteractWithTableLamp && Input.GetKeyDown(interactKey)) {
            tl.ToggleLamp();
        }

        if (canInteractWithLamp && Input.GetKeyDown(interactKey)) {
            l.ToggleLamp();
        }

        if (canInteractWithComputer && Input.GetKeyDown(interactKey) && !isUsingComputer) {
            mainCamera.SetActive(false);
            computerCamera.SetActive(true);
            isUsingComputer = true;
            this.gameObject.GetComponent<PlayerMovement>().moveSpeed = 0;
        }

        if (canInteractWithBed && Input.GetKeyDown(interactKey) && !isHiding) {
            mainCamera.SetActive(false);
            bedCamera.SetActive(true);
            bedCamera.GetComponent<Animator>().SetTrigger("UnderBed");
            this.gameObject.GetComponent<PlayerMovement>().moveSpeed = 0;
            isHiding = true;
        }

        if (isUsingComputer && Input.GetKeyDown(KeyCode.Space))
        {
            computerCamera.GetComponent<Animator>().SetBool("OffComputer", true);
            isUsingComputer = false;
        }

        else if (isHiding && Input.GetKeyDown(KeyCode.Space)) {
            bedCamera.GetComponent<Animator>().SetTrigger("OutUnderBed");
            isHiding = false;
        }

        if (!isUsingComputer && Input.GetKeyDown(KeyCode.Mouse0) && canChangeConsciousness && consciousnessLevel >= 2)
        {
            consciousnessLevel -= 1;
            canChangeConsciousness = false;
            Invoke("RegainConsciousnessControl", 10f);
            cuc.CheckChangeConsciousText(consciousnessLevel);
        }

        else if (!isUsingComputer && Input.GetKeyDown(KeyCode.Mouse1) && canChangeConsciousness && consciousnessLevel <= 2) {
            consciousnessLevel += 1;
            canChangeConsciousness = false;
            Invoke("RegainConsciousnessControl", 10f);
            cuc.CheckChangeConsciousText(consciousnessLevel);
        }
    }

    public void Die() { 
        
    }

    private void RegainConsciousnessControl() {
        canChangeConsciousness = true;
    }
}
