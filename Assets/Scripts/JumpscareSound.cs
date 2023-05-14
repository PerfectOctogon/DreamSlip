using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpscareSound : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip jumpscareSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayJumpscareAudio() {
        audioSource.clip = jumpscareSound;
        audioSource.Play();
    }
}
