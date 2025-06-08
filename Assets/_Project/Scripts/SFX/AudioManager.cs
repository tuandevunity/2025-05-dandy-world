using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioManager Instance;

    [SerializeField] public AudioClip shootSound;
    [SerializeField] public AudioClip explotionSound;
    [SerializeField] public AudioClip reloadBullet;
    [SerializeField] public AudioClip clickUISound;
    [SerializeField] public AudioClip clickButton;
    [SerializeField] public AudioClip windySound;
    [SerializeField] public AudioClip fireSound;
    [SerializeField] public AudioClip dieSound;
    [SerializeField] public AudioClip openDoorSound;
    [SerializeField] public AudioClip valveSound;
    [SerializeField] public AudioClip ballSound;
    [SerializeField] public AudioClip checkpointSound;
    [SerializeField] GameObject audioSourcePrefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void SpawnAndPlay(Transform pos, AudioClip audioClip, float timeDestroy = 1.5f, bool isDestroy = true) {
        GameObject audio = Instantiate(audioSourcePrefab, pos.position, Quaternion.identity);
        audio.transform.parent = pos;
        audio.GetComponent<AudioSource>().PlayOneShot(audioClip);
        if (!isDestroy) {
            audio.GetComponent<AudioSource>().loop = true;
            return;
        }
        Destroy(audio, timeDestroy);
    }

}
