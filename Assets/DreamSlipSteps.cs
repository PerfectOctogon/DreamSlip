using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class DreamSlipSteps : MonoBehaviour
{
    [SerializeField]
    Button StepOneButton;
    [SerializeField]
    Button StepTwoButton;
    [SerializeField]
    Button StepThreeButton;
    [SerializeField]
    Button StepFourButton;
    [SerializeField]
    Button StepFiveButton;
    [SerializeField]
    Button StepSixButton;
    [SerializeField]
    Button StepSevenButton;

    [SerializeField]
    GameObject StepOneInfo;
    [SerializeField]
    GameObject StepTwoInfo;
    [SerializeField]
    GameObject StepThreeInfo;
    [SerializeField]
    GameObject StepFourInfo;
    [SerializeField]
    GameObject StepFiveInfo;
    [SerializeField]
    GameObject StepSixInfo;
    [SerializeField]
    GameObject StepSevenInfo;

    [SerializeField]
    VideoPlayer vp;
    [SerializeField]
    GameObject cutsceneScreen;
    [SerializeField]
    VideoClip openingCutscene;
    [SerializeField]
    VideoClip secondCutscene;
    [SerializeField]
    VideoClip thirdCutscene;

    [SerializeField]
    Player player;

    [SerializeField]
    StageManager stageManager;

    bool playedCurrentCutscene = false;

    // Start is called before the first frame update
    void Start()
    {
        StepOneButton.onClick.AddListener(ShowStepOneInfo);
        StepTwoButton.onClick.AddListener(ShowStepTwoInfo);
        StepThreeButton.onClick.AddListener(ShowStepThreeInfo);
        StepFourButton.onClick.AddListener(ShowStepFourInfo);
        StepFiveButton.onClick.AddListener(ShowStepFiveInfo);
        StepSixButton.onClick.AddListener(ShowStepSixInfo);
        StepSevenButton.onClick.AddListener(ShowStepSevenInfo);
    }

    void CloseAllSteps() {
        StepOneInfo.SetActive(false);
        StepTwoInfo.SetActive(false);
        StepThreeInfo.SetActive(false);
        StepFourInfo.SetActive(false);
        StepFiveInfo.SetActive(false);
        StepSixInfo.SetActive(false);
        StepSevenInfo.SetActive(false);

    }

    void ShowStepOneInfo() {
        PlayCutsceneOne();
        CloseAllSteps();
        StepOneInfo.SetActive(true);
        
    }

    void ShowStepTwoInfo()
    {
        CloseAllSteps();
        PlayCutsceneTwo();
        StepTwoInfo.SetActive(true);
    }
    void ShowStepThreeInfo()
    {
        CloseAllSteps();
        PlayCutsceneThree();
        StepThreeInfo.SetActive(true);
    }
    void ShowStepFourInfo()
    {
        CloseAllSteps();
        StepFourInfo.SetActive(true);
    }
    void ShowStepFiveInfo()
    {
        CloseAllSteps();
        StepFiveInfo.SetActive(true);
    }
    void ShowStepSixInfo()
    {
        CloseAllSteps();
        StepSixInfo.SetActive(true);
    }
    void ShowStepSevenInfo()
    {
        CloseAllSteps();
        StepSevenInfo.SetActive(true);
    }

    void PlayCutsceneOne() {
        int cutsceneAlreadyplayed = PlayerPrefs.GetInt("CutsceneOne", 0);
        if (cutsceneAlreadyplayed == 0)
        {
            vp.clip = openingCutscene;
            player.canReceiveInput = false;
            cutsceneScreen.SetActive(true);
            vp.Play();
            Invoke("EndCutscene", 54);
            PlayerPrefs.SetInt("CutsceneOne", 1);
            stageManager.StepOneAuthenticator();
        }
    }

    void PlayCutsceneTwo() {
        int cutsceneAlreadyplayed = PlayerPrefs.GetInt("CutsceneTwo", 0);
        if (cutsceneAlreadyplayed == 0)
        {
            vp.clip = secondCutscene;
            player.canReceiveInput = false;
            cutsceneScreen.SetActive(true);
            vp.Play();
            Invoke("EndCutscene", 29);
            PlayerPrefs.SetInt("CutsceneTwo", 1);
            stageManager.EnableDoorMonster();
        }
    }

    void PlayCutsceneThree() {
        int cutsceneAlreadyplayed = PlayerPrefs.GetInt("CutsceneThree", 0);
        if (cutsceneAlreadyplayed == 0)
        {
            vp.clip = thirdCutscene;
            player.canReceiveInput = false;
            cutsceneScreen.SetActive(true);
            vp.Play();
            Invoke("EndCutscene", 31);
            PlayerPrefs.SetInt("CutsceneThree", 1);
            stageManager.EnableDoorMonster();
            stageManager.EnableBedMonster();
        }
    }

    void EndCutscene() {
        player.canReceiveInput = true;
        cutsceneScreen.SetActive(false);
        vp.Stop();
    }
}
