using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public int currentStage = 0;
    float timer = 0;
    [SerializeField]
    GameObject bedMonster;
    [SerializeField]
    GameObject doorMonster;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    GameObject chatter;
    [SerializeField]
    Button dreamslipStep1;
    [SerializeField]
    Button dreamslipStep2;
    [SerializeField]
    InputField authenticatorText;
    [SerializeField]
    Text authenticationText;
    [SerializeField]
    Button dreamslipStep3;
    [SerializeField]
    Button dreamslipStep4;

    public bool isAuthenticated = false;

    int activeCode = 0;
    //string[] chat = { "You were careless. Now they're after you... Leave them no evidence if they investigate...", "Have you hid it yet? We can't let them see it. Cover up your tracks.", "The family is angry, I've sent you some solitary locations. Dump it.", "I've been reading about 'dream slips'. It looks like it has potential :). Have you, uhh- " };

    // Start is called before the first frame update
    void Start()
    {
        switch (currentStage) {
            case 1:
                InitiateStepOne();
                return;
            case 2:
                InitiateStepTwo();
                return;
            case 3:
                InitiateStepThree();
                return;
            default:
                InitiateStepOne();
                return;
        }
    }

    // Update is called once per frame
    void Update()
    {


    }


    public static string ToString(byte[] input)
    {
        if (input == null || input.Length == 0)
        {
            throw new System.ArgumentNullException("input");
        }

        int charCount = (int)System.Math.Ceiling(input.Length / 5d) * 8;
        char[] returnArray = new char[charCount];

        byte nextChar = 0, bitsRemaining = 5;
        int arrayIndex = 0;

        foreach (byte b in input)
        {
            nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
            returnArray[arrayIndex++] = ValueToChar(nextChar);

            if (bitsRemaining < 4)
            {
                nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                bitsRemaining += 5;
            }

            bitsRemaining -= 3;
            nextChar = (byte)((b << bitsRemaining) & 31);
        }

        //if we didn't end with a full char
        if (arrayIndex != charCount)
        {
            returnArray[arrayIndex++] = ValueToChar(nextChar);
            while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
        }

        return new string(returnArray);
    }

    private static char ValueToChar(byte b)
    {
        if (b < 26)
        {
            return (char)(b + 65);
        }

        if (b < 32)
        {
            return (char)(b + 24);
        }

        throw new System.ArgumentException("Byte is not a value Base32 value.", "b");
    }

    //STEP 1
    public void InitiateStepOne() {
        currentStage = 1;
        activeCode = Random.Range(10000, 100000);
        chatter.GetComponent<Text>().text = "AWAITING TRANSMISSION...";
        dreamslipStep1.interactable = true;
    }

    public void StepOneAuthenticator() {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(activeCode.ToString());
        string codeString = System.Convert.ToBase64String(plainTextBytes);
        chatter.GetComponent<Text>().text = "Authenticate yourself to continue. CODE : " + codeString;
    }

    public void AuthenticationCheck() {
        switch (currentStage) {
            case 1:
                if (authenticatorText.text.Equals(activeCode.ToString()))
                {
                    //isAuthenticated = true;
                    InitiateStepTwo();
                    authenticationText.text = "VERIFICATION SUCCESS";
                }
                else {
                    authenticationText.text = "CANNOT VERIFY";
                }
                return;
            case 2:
                if (authenticatorText.text.Equals(activeCode.ToString()))
                {
                    //isAuthenticated = true;
                    InitiateStepThree();
                    authenticationText.text = "VERIFICATION SUCCESS";
                }
                else
                {
                    authenticationText.text = "CANNOT VERIFY";
                }
                return;
            default:
                print("Not possible");
                return;
        }
    }

    //STEP 2
    public void InitiateStepTwo() {
        InitiateStepOne();
        currentStage = 2;
        if (PlayerPrefs.GetInt("CutsceneTwo", 0) == 1) {
            EnableDoorMonster();
        }
        activeCode = Random.Range(10000, 100000);
        dreamslipStep2.interactable = true;
    }

    public void EnableDoorMonster() {
        doorMonster.SetActive(true);
        chatter.GetComponent<Text>().text = "Entity #Active399321 detected in the area.";
    }

    public void StartStageTwoTimer() {
        chatter.GetComponent<Text>().text = "Try to keep your consciousness level low to ensure minimum fading. Await further instructions.";
        Invoke("StepTwoAuthenticator", 100);
    }

    public void StepTwoAuthenticator() {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(activeCode.ToString());
        string codeString = System.Convert.ToBase64String(plainTextBytes);
        chatter.GetComponent<Text>().text = "Authenticate yourself to continue. CODE : " + codeString;
    }

    //STEP THREE
    public void InitiateStepThree() {
        InitiateStepOne();
        InitiateStepTwo();
        currentStage = 3;
        doorMonster.SetActive(false);
        activeCode = Random.Range(10000, 100000);
        dreamslipStep3.interactable = true;
        chatter.GetComponent<Text>().text = "PLEASE HOLD. WE ARE FACING SOME ISSUES.";
        if (PlayerPrefs.GetInt("CutsceneThree", 0) == 1)
        {
            Invoke("EnableBedMonster", 1f);
            Invoke("ShowBedMonsterImmunity", 1f);
        }
    }

    public void EnableBedMonster() {
        bedMonster.SetActive(true);
        chatter.GetComponent<Text>().text = "Entity #Dormant31 detected in the area. PLEASE HOLD WHILE WE RESEARCH IMMUNITY.";
        
    }

    void ShowBedMonsterImmunity() {
        chatter.GetComponent<Text>().text = "#Dormant31 is weak to music.";
    }

    public void FaceIssues() {
        chatter.GetComponent<Text>().text = "WE ARE FACING TECHNICAL ISSUES. TAKE THE TIME TO GET ACCLIMATED.";
    }

    void InitiateStepFour() {
        dreamslipStep4.interactable = true;
        currentStage = 4;
        chatter.GetComponent<Text>().text = "WE HAVE FIXED THE PROBLEM. CARRY OUT STEP 4.";
    }

    public void DisableBothMonsters() {
        doorMonster.SetActive(false);
        bedMonster.SetActive(true);
    }

    public void EnableBothMonsters() {
        doorMonster.SetActive(true);
        bedMonster.SetActive(true);
    }
}
