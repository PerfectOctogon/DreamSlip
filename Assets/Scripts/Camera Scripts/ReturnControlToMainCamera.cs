using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnControlToMainCamera : MonoBehaviour
{
    [SerializeField]
    GameObject mainCamera;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject blackScreen;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip thudSound;

    public void ReturnControlToCamera() {
        mainCamera.SetActive(true);
        this.gameObject.SetActive(false);
        player.GetComponent<PlayerMovement>().moveSpeed = 3;
        blackScreen.SetActive(false);
    }

    public void DramatizeReturnControl() {
        audioSource.clip = thudSound;
        audioSource.Play();
        blackScreen.SetActive(true);
        Invoke("ReturnControlToCamera", 5);
    }
}
