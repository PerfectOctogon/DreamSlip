using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField]
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = 1 - player.GetSanity() / 100;
        audioSource.pitch = 2 - player.GetSanity() / 100;
    }
}
