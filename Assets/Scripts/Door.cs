using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip doorKnocking;
    [SerializeField]
    AudioClip doorOpening;
    [SerializeField]
    AudioClip doorClosing;
    [SerializeField]
    Animator doorAnimator;

    public void Knock() {
        audioSource.clip = doorKnocking;
        audioSource.Play();
    }

    public void Open() {
        audioSource.clip = doorOpening;
        audioSource.Play();
        doorAnimator.SetTrigger("OpenDoor");
    }

    public void Close() {
        audioSource.clip = doorClosing;
        audioSource.Play();
        doorAnimator.SetTrigger("CloseDoor");
    }
}
