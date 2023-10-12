using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudio : MonoBehaviour
{
    public AudioSource Source;

    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SourceClip), 3f);
    }

    void SourceClip()
    {
        Source.PlayOneShot(clip, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
