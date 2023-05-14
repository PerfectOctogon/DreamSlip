using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnClickPlay : MonoBehaviour
{

    public void LoadGame() {
        SceneManager.LoadScene("MainGame");
    }

    public void StartNewGame() {
        PlayerPrefs.SetInt("CutsceneOne", 0);
        PlayerPrefs.SetInt("CutsceneTwo", 0);
        SceneManager.LoadScene("MainGame");
    }
}
