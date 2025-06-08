using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;


    [Button("Play", EButtonEnableMode.Always)]
    public void PlaySound(int clip) {
        if (clip ==  1) {
            source.PlayOneShot(clip1);
        } else if (clip == 2) {
            source.PlayOneShot(clip2);
        } else {
            source.PlayOneShot(clip3);
        }
        
    }

    [Button("Stop", EButtonEnableMode.Always)]
    public void StopSound() {
        source.Stop();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (source.clip == null) return;
        Debug.Log(source.clip.name);
    }
}
