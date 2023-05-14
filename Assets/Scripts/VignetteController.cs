using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteController : MonoBehaviour
{
    private Vignette vignette;
    private PostProcessVolume ppv;
    [SerializeField]
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        ppv = this.GetComponent<PostProcessVolume>();
        vignette = ppv.profile.GetSetting<Vignette>();
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity.value = 1 - player.GetSanity() / 100;
    }
}
