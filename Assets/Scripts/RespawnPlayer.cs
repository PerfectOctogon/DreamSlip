using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField]
    GameObject cutsceneCamera;
    [SerializeField]
    GameObject blackScreen;

    // Start is called before the first frame update

    public void ResetPlayer() {
        cutsceneCamera.SetActive(true);
        blackScreen.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
